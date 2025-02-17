using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public NotificationRepository(RecruitmentProcessManagementSystemContext context) { 
            _context = context;
        }

        public async Task<Notification> AddNotification(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();

            return notification;
        }

        public async Task<List<Notification>> GetNotificationAsync()
        {
            return await _context.Notifications.ToListAsync();
        }

        public async Task<Notification?> GetNotificationById (int? id)
        {
            return await _context.Notifications.FirstOrDefaultAsync(n => n.PkNotificationId == id);
        }

        public async Task<List<Notification>> GetNotificationByUser(int userId)
        {
            return await _context.Notifications.Where(n => n.FkUserId == userId).ToListAsync();
        }

        public async Task<Notification> UpdateNotification(Notification notification)
        {
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();

            return notification;
        }

        public async Task DeleteNotification(int id)
        {
            Notification? notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }
        }
    }
}
