using Backend.Models;
using Backend.Repository;

namespace Backend.Services.impl
{
    public class InterviewFeedbackService : IInterviewFeedbackService
    {
        private readonly IInterviewFeedbackRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IInterviewRepository _interviewRepository;
        private readonly IApplicationStatusRepository _applicationStatusRepository;
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public InterviewFeedbackService (IInterviewFeedbackRepository repository, IUserRepository userRepository, IInterviewRepository interviewRepository, IApplicationStatusRepository applicationStatusRepository, IJobApplicationRepository jobApplicationRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _interviewRepository = interviewRepository;
            _applicationStatusRepository = applicationStatusRepository;
            _jobApplicationRepository = jobApplicationRepository;
        }

        public async Task<InterviewFeedback> AddInterviewFeedback(InterviewFeedback interviewFeedback)
        {
            User? user = await _userRepository.GetUserById(interviewFeedback.FkInterviewerId);
            if (user == null) throw new Exception("interviewer not found in system with given id");

            Interview? interview = await _interviewRepository.GetInterviewById(interviewFeedback.FkInterviewId);
            if (interview == null) throw new Exception("interview not found with given id");

            ApplicationStatus? applicationStatus = await _applicationStatusRepository.GetApplicationStatusByNameAsync("INTERVIEWED");
            if (applicationStatus == null) throw new Exception("application status not found for change");

            JobApplication? jobApplication = await _jobApplicationRepository.GetJobApplicationByJobAndCandidate(interview.FkJobPositionId, interview.FkCandidateId);
            if (jobApplication == null) throw new Exception("job application not found you can't give feedback");

            jobApplication.FkStatus = applicationStatus;
            await _jobApplicationRepository.UpdateJobAppliction(jobApplication);

            interviewFeedback.CreatedAt = DateTime.UtcNow;

            return await _repository.AddInterviewFeedback(interviewFeedback);
        }

        public async Task<List<InterviewFeedback>> GetInterviewFeedbacksAsync()
        {
            return await _repository.GetInterviewFeedbacksAsync();
        }

        public async Task<InterviewFeedback> UpdateInterviewFeedback(int? id, InterviewFeedback interviewFeedback)
        {
            InterviewFeedback? interviewFeedback1 = await _repository.GetInterviewFeedbackById(id);
            if (interviewFeedback1 == null) throw new Exception("Interview feedback is not exist with given id");

            interviewFeedback1.Rating = interviewFeedback.Rating ?? interviewFeedback1.Rating;
            interviewFeedback1.Comments = interviewFeedback.Comments ?? interviewFeedback1.Comments;

            return await _repository.UpdateInterviewFeedback(interviewFeedback1);
        }

        public async Task<InterviewFeedback?> GetInterviewFeedbackByUserAndInterview(int userId, int interviewId)
        {
            User? user = await _userRepository.GetUserById(userId);
            if (user == null) throw new Exception("interviewer not found in system with given id");

            Interview? interview = await _interviewRepository.GetInterviewById(interviewId);
            if (interview == null) throw new Exception("interview not found with given id");

            return await _repository.GetInterviewFeedbackByUserAndInterview(userId, interviewId);
        }

        public async Task DeleteInterviewFeedback(int id)
        {
            InterviewFeedback? interviewFeedback = await _repository.GetInterviewFeedbackById(id);
            if (interviewFeedback == null) throw new Exception("interview feedback not exist with given id");

            await _repository.DeleteInterviewFeedback(id);
        }
    }
}
