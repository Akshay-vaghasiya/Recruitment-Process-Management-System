using Backend.Models;

namespace Backend.Repository
{
    public interface ICandidateNotificationRepository
    {
        Task<CandidateNotification> AddCandidateNotification(CandidateNotification CandidateNotification);
        Task<List<CandidateNotification>> GetCandidateNotificationAsync();
        Task<CandidateNotification?> GetCandidateNotificationById(int? id);
        Task<CandidateNotification> UpdateCandidateNotification(CandidateNotification CandidateNotification);
        Task DeleteCandidateNotification(int id);
        Task<List<CandidateNotification>> GetCandidateNotificationByCandidate(int candidateId);
    }
}
