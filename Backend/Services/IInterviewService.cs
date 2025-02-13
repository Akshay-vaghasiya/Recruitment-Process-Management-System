using Backend.Dtos;
using Backend.Models;

namespace Backend.Services
{
    public interface IInterviewService
    {
        Task<Interview> AddInterview(InterviewDto interviewDto);
        Task<List<Interview>> GetInterviewsAsync();
        Task DeleteInterview(int interviewId);

        Task<Interview> UpdateInterviewStatus(int interviewId, int interviewStatusId);
        Task<List<Interview>> GetInterviewsByCandidateAndPosition(int candidateId, int positionId);
    }
}
