using Backend.Models;

namespace Backend.Services
{
    public interface IInterviewFeedbackService
    {
        Task<InterviewFeedback> AddInterviewFeedback(InterviewFeedback interviewFeedback);
        Task<List<InterviewFeedback>> GetInterviewFeedbacksAsync();
        Task<InterviewFeedback> UpdateInterviewFeedback(int? id, InterviewFeedback interviewFeedback);
        Task DeleteInterviewFeedback(int id);
        Task<InterviewFeedback?> GetInterviewFeedbackByUserAndInterview(int userId, int interviewId);
    }
}
