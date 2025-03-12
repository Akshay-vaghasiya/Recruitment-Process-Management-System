using Backend.Models;
using Backend.Repository;

namespace Backend.Services.impl
{
    public class InterviewPanelService : IInterviewPanelService
    {
        private readonly IInterviewPanelRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IInterviewRepository _interviewRepository;
        private readonly INotificationRepository _notificationRepository;

        public InterviewPanelService(IInterviewPanelRepository repository, IUserRepository userRepository, IInterviewRepository interviewRepository, INotificationRepository notificationRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _interviewRepository = interviewRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task<InterviewPanel> AddInterviewPanel(int userId, int interviewId)
        {
            User? user = await _userRepository.GetUserById(userId);
            if (user == null) throw new Exception("user not exist with given id");

            Interview? interview = await _interviewRepository.GetInterviewById(interviewId);
            if (interview == null) throw new Exception("interview not exist with given id");

            var round = interview?.FkInterviewRound?.Name;

            int flag = 0;
            foreach (var role in user.UserRoles)
            {
                if(round == "HR")
                {
                    if (role?.FkRole?.Name == "HR")
                    {
                        flag = 1;
                        break;
                    }
                } else
                {
                    if (role?.FkRole?.Name == "INTERVIEWER")
                    {
                        flag = 1;
                        break;
                    }
                }       
            }
            
            if(flag == 0)
            {
                throw new Exception("user not have sufficient role");
            }

            InterviewPanel? interviewPanel = await _repository.GetInterviewPanelByUserAndInterview(userId, interviewId);
            if (interviewPanel != null) throw new Exception("interview panel already exist with given interview id");

            InterviewPanel interviewPanel1 = new InterviewPanel();
            interviewPanel1.FkInterviewId = interviewId;
            interviewPanel1.FkInterviewerId = userId;

            var panel =  await _repository.AddInterviewPanel(interviewPanel1);

            Notification notification = new Notification();
            notification.FkUserId = panel.FkInterviewerId;
            notification.Message = $"you will be assigned to take {interview?.FkInterviewRound?.Name} interview round of {interview?.FkJobPosition?.Title} job position for {interview?.FkCandidate?.Email} and it will be schedule at {interview?.ScheduledTime}";
            notification.IsRead = false;

            await _notificationRepository.AddNotification(notification);

            return panel;
        }

        public async Task<List<InterviewPanel>> GetInterviewPanelsAsync()
        {
            return await _repository.GetInterviewPanelsAsync();
        }

        public async Task DeleteInterviewPanel(int id)
        {
            InterviewPanel? interviewPanel = await _repository.GetInterviewPanelById(id);
            if (interviewPanel == null) throw new Exception("interview panel not exist in system");

            Interview? interview = await _interviewRepository.GetInterviewById(interviewPanel.FkInterviewId);

            await _repository.DeleteInterviewPanel(interviewPanel);

            Notification notification = new Notification();
            notification.FkUserId = interviewPanel.FkInterviewerId;
            notification.Message = $"you will be free your {interview?.FkInterviewRound?.Name} interview round of {interview?.FkJobPosition?.Title} job position for {interview?.FkCandidate?.Email} was deleted by admin";
            notification.IsRead = false;

            await _notificationRepository.AddNotification(notification);
        }

        public async Task<List<InterviewPanel>> GetPanelByInterview(int interviewId)
        {
            Interview? interview = await _interviewRepository.GetInterviewById(interviewId);
            if (interview == null) throw new Exception("interview not exist with given id");

            return await _repository.GetInterviewPanelByInterview(interviewId);

        }
    }
}
