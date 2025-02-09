using Backend.Models;

namespace Backend.Repository
{
    public interface IInterviewPanelRepository
    {
        Task<InterviewPanel> AddInterviewPanel(InterviewPanel interviewPanel);
        Task<List<InterviewPanel>> GetInterviewPanelsAsync();
        Task DeleteInterviewPanel(InterviewPanel interviewPanel);
        Task<InterviewPanel?> GetInterviewPanelById(int interviewPanelId);
        Task<InterviewPanel?> GetInterviewPanelByUserAndInterview(int userId, int interviewId);
    }
}
