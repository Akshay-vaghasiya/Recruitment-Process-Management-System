using Backend.Models;
using Backend.Repository;

namespace Backend.Services.impl
{
    public class ApplicationStatusService : IApplicationStatusService
    {
        private readonly IApplicationStatusRepository _repository;

        public ApplicationStatusService(IApplicationStatusRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ApplicationStatus>> GetAllApplicationStatusesAsync()
        {
            return await _repository.GetAllApplicationStatusesAsync();
        }

        public async Task<ApplicationStatus?> GetApplicationStatusByIdAsync(int id)
        {
            return await _repository.GetApplicationStatusByIdAsync(id);
        }

        public async Task<ApplicationStatus> AddApplicationStatusAsync(ApplicationStatus ApplicationStatus)
        {
            ApplicationStatus? ApplicationStatus1 = await _repository.GetApplicationStatusByNameAsync(ApplicationStatus.Name);
            if (ApplicationStatus1 != null) throw new Exception("Applicationstatus already exist!");

            return await _repository.AddApplicationStatusAsync(ApplicationStatus);
        }

        public async Task<ApplicationStatus> UpdateApplicationStatusAsync(int id, ApplicationStatus ApplicationStatus)
        {
            ApplicationStatus? ApplicationStatus1 = await _repository.GetApplicationStatusByIdAsync(id);
            if (ApplicationStatus1 == null) throw new Exception("status with given id is not exist!");

            ApplicationStatus? ApplicationStatus2 = await _repository.GetApplicationStatusByNameAsync(ApplicationStatus.Name);
            if (ApplicationStatus2 != null) throw new Exception("status already exist!");

            ApplicationStatus1.Name = ApplicationStatus.Name;

            return await _repository.UpdateApplicationStatusAsync(ApplicationStatus1);
        }

        public async Task DeleteApplicationStatusAsync(int id)
        {
            ApplicationStatus? ApplicationStatus = await _repository.GetApplicationStatusByIdAsync(id);
            if (ApplicationStatus == null) throw new Exception("Application status not found by given id");

            if (ApplicationStatus.JobApplications.Count() > 0)
            {
                throw new Exception("This Application status is assign to many Applications.");
            }

            await _repository.DeleteApplicationStatusAsync(id);
        }
    }
}
