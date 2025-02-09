using Backend.Models;

namespace Backend.Repository
{
    public interface INotificationRepository
    {
        Task<Notification> AddNotification(Notification notification);
        Task<List<Notification>> GetNotificationAsync();
        Task<Notification?> GetNotificationById(int? id);
        Task<Notification> UpdateNotification(Notification notification);
        Task DeleteNotification(int id);
    }
}
