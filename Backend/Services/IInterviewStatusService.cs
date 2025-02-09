using Backend.Models;

namespace Backend.Services
{
    public interface IInterviewStatusService
    {
        Task<List<InterviewStatus>> GetAllInterviewStatusesAsync();
        Task<InterviewStatus?> GetInterviewStatusByIdAsync(int id);
        Task<InterviewStatus> AddInterviewStatusAsync(InterviewStatus InterviewStatus);
        Task<InterviewStatus> UpdateInterviewStatusAsync(int id, InterviewStatus InterviewStatus);
        Task DeleteInterviewStatusAsync(int id);
    }
}
