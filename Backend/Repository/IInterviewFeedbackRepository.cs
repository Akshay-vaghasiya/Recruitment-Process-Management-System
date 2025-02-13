using Backend.Models;

namespace Backend.Repository
{
    public interface IInterviewFeedbackRepository
    {
        Task<InterviewFeedback> AddInterviewFeedback(InterviewFeedback interviewFeedback);
        Task<InterviewFeedback?> GetInterviewFeedbackById(int? id);
        Task<List<InterviewFeedback>> GetInterviewFeedbacksAsync();
        Task<InterviewFeedback> UpdateInterviewFeedback(InterviewFeedback interviewFeedback);
        Task DeleteInterviewFeedback(int id);
        Task<InterviewFeedback?> GetInterviewFeedbackByUserAndInterview(int? userId, int? interviewId);
    }
}
