using Backend.Dtos;
using Backend.Models;
using Backend.Repository;
using static System.Net.Mime.MediaTypeNames;

namespace Backend.Services.impl
{
    public class InterviewService : IInterviewService
    {
        private readonly IInterviewRepository _repository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IJobPositionRepository _jobPositionRepository;
        private readonly IInterviewStatusRepository _interviewStatusRepository;
        private readonly IInterviewRoundRepository _interviewRoundRepository;
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IApplicationStatusRepository _applicationStatusRepository;
        private readonly ICandidateNotificationRepository _candidateNotificationRepository;
        private readonly INotificationRepository _notificationRepository;

        public InterviewService(IInterviewRepository repository, ICandidateRepository candidateRepository, IJobPositionRepository jobPositionRepository, IInterviewStatusRepository interviewStatusRepository, IInterviewRoundRepository interviewRoundRepository, IJobApplicationRepository jobApplicationRepository, IApplicationStatusRepository applicationStatusRepository, ICandidateNotificationRepository candidateNotificationRepository, INotificationRepository notificationRepository)
        {
            _repository = repository;
            _candidateRepository = candidateRepository;
            _jobPositionRepository = jobPositionRepository;
            _interviewStatusRepository = interviewStatusRepository;
            _interviewRoundRepository = interviewRoundRepository;
            _jobApplicationRepository = jobApplicationRepository;
            _applicationStatusRepository = applicationStatusRepository;
            _candidateNotificationRepository = candidateNotificationRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task<Interview> AddInterview(InterviewDto interviewDto) 
        {
            Candidate? candidate = await _candidateRepository.GetCandidateById(interviewDto?.FkCandidateId);
            if (candidate == null) throw new Exception("candidate not exist with given id");

            JobPosition? jobPosition = await _jobPositionRepository.GetJobPositionByIdAsync(interviewDto?.FkJobPositionId);
            if (jobPosition == null) throw new Exception("job positoin not exist with given id");

            InterviewStatus? interviewStatus = await _interviewStatusRepository.GetInterviewStatusByIdAsync(interviewDto?.FkStatusId);
            if (interviewStatus == null) throw new Exception("interview status not exist with given id");

            InterviewRound? interviewRound = await _interviewRoundRepository.GetInterviewRoundByIdAsync(interviewDto?.FkInterviewRoundId);
            if (interviewRound == null) throw new Exception("interview round not exist with given id");

            Interview? interview1 = await _repository.GetInterviewByCandidateAndPositionAndRound(interviewDto?.FkCandidateId, interviewDto?.FkJobPositionId, interviewDto?.FkInterviewRoundId);
            if (interview1 != null) throw new Exception("This interview round already exist for candidate for job position");

            JobApplication? jobApplication = await _jobApplicationRepository.GetJobApplicationByJobAndCandidate(interviewDto?.FkJobPositionId, interviewDto?.FkCandidateId);

            List<Interview> interviews = await _repository.GetInterviewByCandidateAndPosistion(interviewDto.FkCandidateId, interviewDto.FkJobPositionId);
            foreach (var i in interviews)
            {
                if(i.RoundNumber == interviewDto.RoundNumber)
                {
                    throw new Exception("please change interview round number");
                }
            }

            if (jobApplication == null) throw new Exception("Job application not exist");


            if (jobApplication?.FkStatus?.Name == "APPLIED" || jobApplication?.FkStatus?.Name == "SCREENED")
            {
                throw new Exception("This candidate not shortlisted so you can not schedule interview round.");
            }

            ApplicationStatus? applicationStatus = await _applicationStatusRepository.GetApplicationStatusByNameAsync("INTERVIEW SCHEDULED");
            if (applicationStatus == null) throw new Exception("Application status not found for change");

            if (applicationStatus != null)
            jobApplication.FkStatus = applicationStatus;

            await _jobApplicationRepository.UpdateJobAppliction(jobApplication);

            Interview interview = new Interview();

            interview.FkInterviewRoundId = interviewDto?.FkInterviewRoundId;
            interview.FkCandidateId = interviewDto?.FkCandidateId;
            interview.FkJobPositionId = interviewDto?.FkJobPositionId;
            interview.FkStatusId = interviewDto?.FkStatusId;
            interview.RoundNumber = interviewDto?.RoundNumber;
            interview.ScheduledTime = interviewDto?.ScheduledTime;
            interview.CreatedAt = DateTime.UtcNow;

            var interview2 = await _repository.AddInterview(interview);

            CandidateNotification candidateNotification = new CandidateNotification();
            candidateNotification.FkCandidateId = interview2.FkCandidateId;
            candidateNotification.Message = $"{interviewRound.Name} interview round will be schedule at {interview2.ScheduledTime?.ToString()} for {jobPosition.Title} job position.";
            candidateNotification.IsRead = false;

            await _candidateNotificationRepository.AddCandidateNotification(candidateNotification);

            return interview2;
        }

        public async Task<List<Interview>> GetInterviewsAsync()
        {
            return await _repository.GetInterviewsAsync();
        }

        public async Task<List<Interview>> GetInterviewsByCandidateAndPosition(int candidateId, int positionId)
        {
            return await _repository.GetInterviewByCandidateAndPosistion(candidateId, positionId);
        }

        public async Task DeleteInterview(int interviewId)
        {
            Interview? interview = await _repository.GetInterviewById(interviewId);
            if (interview == null) throw new Exception("Interview not exist in system");

            interview.FkCandidate = null;
            interview.FkInterviewRound = null;
            interview.FkJobPosition = null;
            interview.FkStatus = null;

            await _repository.UpdateInterview(interview);

            await _repository.DeleteInterview(interviewId);

            CandidateNotification candidateNotification = new CandidateNotification();
            candidateNotification.FkCandidateId = interview.FkCandidateId;
            candidateNotification.Message = $"{interview.FkInterviewRound.Name} interview round was deleted by admin for {interview?.FkJobPosition?.Title} job position";
            candidateNotification.IsRead = false;

            await _candidateNotificationRepository.AddCandidateNotification(candidateNotification);

        }

        public async Task<Interview> UpdateInterview(int interviewId, InterviewDto interviewDto)
        {
            Interview? interview = await _repository.GetInterviewById(interviewId);
            if (interview == null) throw new Exception("Interview not exist in system");

            InterviewStatus? interviewStatus = await _interviewStatusRepository.GetInterviewStatusByIdAsync(interviewDto?.FkStatusId);
            if (interviewStatus == null) throw new Exception("Interview status not exist in system");

            interview.RoundNumber = interviewDto?.RoundNumber ?? interview.RoundNumber;
            interview.ScheduledTime = interviewDto?.ScheduledTime ?? interview.ScheduledTime;
            interview.FkStatusId = interviewDto?.FkStatusId ?? interviewDto?.FkStatusId;

            var interview1 = await _repository.UpdateInterview(interview);

            CandidateNotification candidateNotification = new CandidateNotification();
            candidateNotification.FkCandidateId = interview1.FkCandidateId;
            candidateNotification.Message = $"{interview.FkInterviewRound.Name} interview round is updated and schedule at {interview1.ScheduledTime} for {interview1?.FkJobPosition?.Title} job position.";
            candidateNotification.IsRead = false;
            await _candidateNotificationRepository.AddCandidateNotification(candidateNotification);

            if (interviewStatus.Name == "CANCELLED")
            {
                foreach(var panel in interview.InterviewPanels)
                {
                    Notification notification = new Notification();
                    notification.FkUserId = panel.FkInterviewerId;
                    notification.Message = $"{interview.FkInterviewRound.Name} interview round of {interview.FkJobPosition.Title} job position for {interview.FkCandidate.Email} is cancelled";
                    notification.IsRead = false;
                    await _notificationRepository.AddNotification(notification);
                }
            }

            return interview1;
        }
    }
}
