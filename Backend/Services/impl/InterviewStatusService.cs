using Backend.Models;
using Backend.Repository;

namespace Backend.Services.impl
{
    public class InterviewStatusService : IInterviewStatusService
    {
        private readonly IInterviewStatusRepository _repository;

        public InterviewStatusService(IInterviewStatusRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InterviewStatus>> GetAllInterviewStatusesAsync()
        {
            return await _repository.GetAllInterviewStatusesAsync();
        }

        public async Task<InterviewStatus?> GetInterviewStatusByIdAsync(int id)
        {
            return await _repository.GetInterviewStatusByIdAsync(id);
        }

        public async Task<InterviewStatus> AddInterviewStatusAsync(InterviewStatus InterviewStatus)
        {
            InterviewStatus? InterviewStatus1 = await _repository.GetInterviewStatusByNameAsync(InterviewStatus.Name);
            if (InterviewStatus1 != null) throw new Exception("Interviewstatus already exist!");

            return await _repository.AddInterviewStatusAsync(InterviewStatus);
        }

        public async Task<InterviewStatus> UpdateInterviewStatusAsync(int id, InterviewStatus InterviewStatus)
        {
            InterviewStatus? InterviewStatus1 = await _repository.GetInterviewStatusByIdAsync(id);
            if (InterviewStatus1 == null) throw new Exception("status with given id is not exist!");

            InterviewStatus? InterviewStatus2 = await _repository.GetInterviewStatusByNameAsync(InterviewStatus.Name);
            if (InterviewStatus2 != null) throw new Exception("status already exist!");

            InterviewStatus1.Name = InterviewStatus.Name;

            return await _repository.UpdateInterviewStatusAsync(InterviewStatus1);
        }

        public async Task DeleteInterviewStatusAsync(int id)
        {
            InterviewStatus? InterviewStatus = await _repository.GetInterviewStatusByIdAsync(id);
            if (InterviewStatus == null) throw new Exception("Interview status not found by given id");

            if (InterviewStatus.Interviews.Count() > 0)
            {
                throw new Exception("This Interview status is assign to many Interviews.");
            }

            await _repository.DeleteInterviewStatusAsync(id);
        }
    }
}
