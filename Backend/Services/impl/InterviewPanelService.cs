using Backend.Models;
using Backend.Repository;

namespace Backend.Services.impl
{
    public class InterviewPanelService : IInterviewPanelService
    {
        private readonly IInterviewPanelRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IInterviewRepository _interviewRepository;

        public InterviewPanelService(IInterviewPanelRepository repository, IUserRepository userRepository, IInterviewRepository interviewRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _interviewRepository = interviewRepository;
        }

        public async Task<InterviewPanel> AddInterviewPanel(int userId, int interviewId)
        {
            User? user = await _userRepository.GetUserById(interviewId);
            if (user == null) throw new Exception("user not exist with given id");

            int flag = 0;
            foreach (var role in user.UserRoles)
            {
                if(role.FkRole.Name == "INTERVIEWER")
                {
                    flag = 1;
                    break;
                }
            }
            
            if(flag == 0)
            {
                throw new Exception("user not have interviewer role");
            }


            Interview? interview = await _interviewRepository.GetInterviewById(interviewId);
            if (interview == null) throw new Exception("interview not exist with given id");

            InterviewPanel? interviewPanel = await _repository.GetInterviewPanelByUserAndInterview(userId, interviewId);
            if (interviewPanel != null) throw new Exception("interview panel already exist with given interview id");

            InterviewPanel interviewPanel1 = new InterviewPanel();
            interviewPanel1.FkInterviewId = interviewId;
            interviewPanel1.FkInterviewerId = userId;
            
            return await _repository.AddInterviewPanel(interviewPanel1);
        }

        public async Task<List<InterviewPanel>> GetInterviewPanelsAsync()
        {
            return await _repository.GetInterviewPanelsAsync();
        }

        public async Task DeleteInterviewPanel(int id)
        {
            InterviewPanel? interviewPanel = await _repository.GetInterviewPanelById(id);
            if (interviewPanel == null) throw new Exception("interview panel not exist in system");

            await _repository.DeleteInterviewPanel(interviewPanel);
        }
    }
}
