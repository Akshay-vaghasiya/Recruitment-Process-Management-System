using Backend.Models;

namespace Backend.Repository
{
    public interface IInterviewRepository
    {
        Task<Interview> AddInterview(Interview interview);
        Task<Interview?> GetInterviewById(int? id);
        Task<List<Interview>> GetInterviewsAsync();
        Task<Interview> UpdateInterview(Interview interview);
        Task DeleteInterview(int id);
        Task<Interview?> GetInterviewByCandidateAndPositionAndRound(int? candidateId, int? positionId, int? roundId);
        Task<List<Interview>> GetInterviewByCandidateAndPosistion(int? candidateId, int? positionId);
    }
}
