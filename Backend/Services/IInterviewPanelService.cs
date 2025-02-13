using Backend.Models;

namespace Backend.Services
{
    public interface IInterviewPanelService
    {
        Task<InterviewPanel> AddInterviewPanel(int userId, int interviewId);
        Task<List<InterviewPanel>> GetInterviewPanelsAsync();
        Task DeleteInterviewPanel(int id);
        Task<List<InterviewPanel>> GetPanelByInterview(int interviewId);
    }
}
