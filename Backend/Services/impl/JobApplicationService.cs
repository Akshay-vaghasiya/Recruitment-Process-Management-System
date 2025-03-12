using Backend.Models;
using Backend.Repository;

namespace Backend.Services.impl
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _repository;
        private readonly IJobPositionRepository _positionRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IApplicationStatusRepository _applicationStatusRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly ICandidateNotificationRepository _candidateNotificationRepository;

        public JobApplicationService(IJobApplicationRepository repository, IJobPositionRepository positionRepository, ICandidateRepository candidateRepository, IApplicationStatusRepository applicationStatusRepository, INotificationRepository notificationRepository, ICandidateNotificationRepository candidateNotificationRepository)
        {
            _repository = repository;
            _positionRepository = positionRepository;
            _candidateRepository = candidateRepository;
            _applicationStatusRepository = applicationStatusRepository;
            _notificationRepository = notificationRepository;
            _candidateNotificationRepository = candidateNotificationRepository;
        }

        public async Task<JobApplication?> AddJobApplication(int jobPositionId, int candidateId)
        {
            JobPosition? jobPosition = await _positionRepository.GetJobPositionByIdAsync(jobPositionId);
            if (jobPosition == null) throw new Exception("job position not exist with given id");

            Candidate? candidate = await _candidateRepository.GetCandidateById(candidateId);
            if (candidate == null) throw new Exception("candidate not exist with given id");

            JobApplication? jobApplication1 = await _repository.GetJobApplicationByJobAndCandidate(jobPositionId, candidateId);
            if (jobApplication1 != null) throw new Exception("candidate already applied for this job");

            ApplicationStatus? applicationStatus = await _applicationStatusRepository.GetApplicationStatusByNameAsync("APPLIED");


            JobApplication jobApplication = new JobApplication();
            jobApplication.FkStatus = applicationStatus;
            jobApplication.FkCandidate = candidate;
            jobApplication.FkJobPosition = jobPosition;
            jobApplication.CreatedAt = DateTime.UtcNow;

            var application =  await _repository.AddJobApplication(jobApplication);
            if (application != null)
            {
                Notification notification = new Notification();
                notification.FkUserId = jobPosition.FkReviewerId;
                notification.Message = $"You need to screen resume of ${candidate.Email} candidate.";
                notification.IsRead = false;
                await _notificationRepository.AddNotification(notification);

                CandidateNotification candidateNotification = new CandidateNotification();
                candidateNotification.FkCandidateId = jobApplication.FkCandidateId;
                candidateNotification.Message = $"Your application for {jobPosition.Title} job position successfully saved so please check it out.";
                candidateNotification.IsRead = false;

                await _candidateNotificationRepository.AddCandidateNotification(candidateNotification);
                return application;
            }
            return application;
        }

        public async Task<List<JobApplication>> GetJobApplicationsAsync()
        {
            return await _repository.GetApplicationsAsync();
        }

        public async Task<JobApplication?> UpdateJobApplicationStatus(int jobApplicationId, int  jobApplicationStatusId)
        {
            JobApplication? jobApplication = await _repository.GetJobApplicationById(jobApplicationId);
            if (jobApplication == null) throw new Exception("job application not exist with given id");

            ApplicationStatus? applicationStatus = await _applicationStatusRepository.GetApplicationStatusByIdAsync(jobApplicationStatusId);
            if (applicationStatus == null) throw new Exception("application status not exist with given id");

            jobApplication.FkStatus = applicationStatus;

            var application =  await _repository.UpdateJobAppliction(jobApplication);
            if (application != null)
            {
                CandidateNotification candidateNotification = new CandidateNotification();
                candidateNotification.FkCandidateId = jobApplication.FkCandidateId;
                candidateNotification.Message = $"Your application of {jobApplication?.FkJobPosition?.Title} job position status is going to {applicationStatus.Name}";
                candidateNotification.IsRead = false;

                await _candidateNotificationRepository.AddCandidateNotification(candidateNotification);

                return application;
            }

            return null;
        }

        public async Task DeleteJobApplication(int jobApplicationId)
        {
            JobApplication? jobApplication = await _repository.GetJobApplicationById(jobApplicationId);
            if (jobApplication == null) throw new Exception("job appliction not exist with given id");

            CandidateNotification candidateNotification = new CandidateNotification();
            candidateNotification.FkCandidateId = jobApplication.FkCandidateId;
            candidateNotification.Message = $"Your application of {jobApplication?.FkJobPosition?.Title} job position deleted by admin";
            candidateNotification.IsRead = false;

            await _candidateNotificationRepository.AddCandidateNotification(candidateNotification);

            await _repository.DeleteJobAppliction(jobApplicationId);
        }

    }
}
