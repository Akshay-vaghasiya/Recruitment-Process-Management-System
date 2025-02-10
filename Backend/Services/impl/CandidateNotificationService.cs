using Backend.Models;
using Backend.Repository;

namespace Backend.Services.impl
{
    public class CandidateNotificationService : ICandidateNotificationService
    {
        private readonly ICandidateNotificationRepository _repository;
        private readonly ICandidateRepository _candidateRepository;


        public CandidateNotificationService(ICandidateNotificationRepository repository, ICandidateRepository candidateRepository)
        {
            _repository = repository;
            _candidateRepository = candidateRepository;
        }

        public async Task<CandidateNotification> AddCandidateNotification(CandidateNotification CandidateNotification)
        {
            Candidate? candidate = await _candidateRepository.GetCandidateById(CandidateNotification.FkCandidateId);
            if (candidate == null) throw new Exception("candidate not exist with given id");

            CandidateNotification.IsRead = false;
            CandidateNotification.CreatedAt = DateTime.UtcNow;
            return await _repository.AddCandidateNotification(CandidateNotification);
        }

        public async Task<List<CandidateNotification>> GetCandidateNotificationsAsync()
        {
            return await _repository.GetCandidateNotificationAsync();
        }

        public async Task<CandidateNotification> UpdateCandidateNotification(int id, CandidateNotification CandidateNotification)
        {
            CandidateNotification? CandidateNotification1 = await _repository.GetCandidateNotificationById(id);
            if (CandidateNotification1 == null) throw new Exception("CandidateNotification not exist with given id");

            CandidateNotification1.Message = CandidateNotification.Message ?? CandidateNotification1.Message;
            CandidateNotification1.IsRead = CandidateNotification.IsRead ?? CandidateNotification1.IsRead;

            return await _repository.UpdateCandidateNotification(CandidateNotification1);
        }

        public async Task DeleteCandidateNotification(int id)
        {
            CandidateNotification? CandidateNotification1 = await _repository.GetCandidateNotificationById(id);
            if (CandidateNotification1 == null) throw new Exception("CandidateNotification not exist with given id");

            await _repository.DeleteCandidateNotification(id);
        }
    }
}
