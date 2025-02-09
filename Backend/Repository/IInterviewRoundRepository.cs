using Backend.Models;

namespace Backend.Repository
{
    public interface IInterviewRoundRepository
    {
        Task<List<InterviewRound>> GetAllInterviewRoundesAsync();
        Task<InterviewRound?> GetInterviewRoundByIdAsync(int? id);
        Task<InterviewRound?> GetInterviewRoundByNameAsync(string? name);
        Task<InterviewRound> AddInterviewRoundAsync(InterviewRound InterviewRound);
        Task<InterviewRound> UpdateInterviewRoundAsync(InterviewRound InterviewRound);
        Task DeleteInterviewRoundAsync(int id);
    }
}
