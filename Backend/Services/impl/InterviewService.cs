using Backend.Dtos;
using Backend.Models;
using Backend.Repository;

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

        public InterviewService(IInterviewRepository repository, ICandidateRepository candidateRepository, IJobPositionRepository jobPositionRepository, IInterviewStatusRepository interviewStatusRepository, IInterviewRoundRepository interviewRoundRepository, IJobApplicationRepository jobApplicationRepository, IApplicationStatusRepository applicationStatusRepository)
        {
            _repository = repository;
            _candidateRepository = candidateRepository;
            _jobPositionRepository = jobPositionRepository;
            _interviewStatusRepository = interviewStatusRepository;
            _interviewRoundRepository = interviewRoundRepository;
            _jobApplicationRepository = jobApplicationRepository;
            _applicationStatusRepository = applicationStatusRepository;
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

            return await _repository.AddInterview(interview);
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

            return await _repository.UpdateInterview(interview);
        }
    }
}
