using Backend.Models;

namespace Backend.Services
{
    public interface ICandidateNotificationService
    {
        Task<CandidateNotification> AddCandidateNotification(CandidateNotification CandidateNotification);
        Task<List<CandidateNotification>> GetCandidateNotificationsAsync();
        Task<CandidateNotification> UpdateCandidateNotification(int id, CandidateNotification CandidateNotification);
        Task DeleteCandidateNotification(int id);
    }
}
