using Backend.Models;
using Backend.Repository;

namespace Backend.Services.impl
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly ICandidateRepository _candidateRepository;


        public NotificationService(INotificationRepository repository, IUserRepository userRepository, ICandidateRepository candidateRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _candidateRepository = candidateRepository;
        }

        public async Task<Notification> AddNotification(Notification notification)
        {           
            User? user = await _userRepository.GetUserById(notification.FkUserId);
            if (user == null) throw new Exception("user not exist with given id");
            
            notification.IsRead = false;
            notification.CreatedAt = DateTime.UtcNow;
            return await _repository.AddNotification(notification);
        }

        public async Task<List<Notification>> GetNotificationsAsync()
        {
            return await _repository.GetNotificationAsync();
        }

        public async Task<Notification> UpdateNotification(int id, Notification notification)
        {
            Notification? notification1 = await _repository.GetNotificationById(id);
            if (notification1 == null) throw new Exception("notification not exist with given id");

            notification1.Message = notification.Message ?? notification1.Message;
            notification1.IsRead = notification.IsRead ?? notification1.IsRead;

            return await _repository.UpdateNotification(notification1);
        }

        public async Task DeleteNotification(int id)
        {
            Notification? notification1 = await _repository.GetNotificationById(id);
            if (notification1 == null) throw new Exception("notification not exist with given id");

            await _repository.DeleteNotification(id);
        }
    }
}
