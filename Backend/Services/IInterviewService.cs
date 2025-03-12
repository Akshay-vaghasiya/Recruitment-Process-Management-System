using Backend.Dtos;
using Backend.Models;

namespace Backend.Services
{
    public interface IInterviewService
    {
        Task<Interview> AddInterview(InterviewDto interviewDto);
        Task<List<Interview>> GetInterviewsAsync();
        Task DeleteInterview(int? interviewId);
        Task<List<Interview>> GetInterviewsByCandidateAndPosition(int candidateId, int positionId);
        Task<Interview?> UpdateInterview(int interviewId, InterviewDto interviewDto);
    }
}
