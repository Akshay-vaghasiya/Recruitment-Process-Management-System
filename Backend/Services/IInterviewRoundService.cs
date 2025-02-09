using Backend.Models;

namespace Backend.Services
{
    public interface IInterviewRoundService
    {
        Task<List<InterviewRound>> GetAllInterviewRoundesAsync();
        Task<InterviewRound?> GetInterviewRoundByIdAsync(int id);
        Task<InterviewRound> AddInterviewRoundAsync(InterviewRound InterviewRound);
        Task<InterviewRound> UpdateInterviewRoundAsync(int id, InterviewRound InterviewRound);
        Task DeleteInterviewRoundAsync(int id);
    }
}
