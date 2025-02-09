using Backend.Models;

namespace Backend.Services
{
    public interface INotificationService
    {
        Task<Notification> AddNotification(Notification notification);
        Task<List<Notification>> GetNotificationsAsync();
        Task<Notification> UpdateNotification(int id, Notification notification);
        Task DeleteNotification(int id);
    }
}
