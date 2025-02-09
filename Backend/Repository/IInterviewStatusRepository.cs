using Backend.Models;

namespace Backend.Repository
{
    public interface IInterviewStatusRepository
    {
        Task<List<InterviewStatus>> GetAllInterviewStatusesAsync();
        Task<InterviewStatus?> GetInterviewStatusByIdAsync(int? id);
        Task<InterviewStatus?> GetInterviewStatusByNameAsync(string? name);
        Task<InterviewStatus> AddInterviewStatusAsync(InterviewStatus InterviewStatus);
        Task<InterviewStatus> UpdateInterviewStatusAsync(InterviewStatus InterviewStatus);
        Task DeleteInterviewStatusAsync(int id);
    }
}
