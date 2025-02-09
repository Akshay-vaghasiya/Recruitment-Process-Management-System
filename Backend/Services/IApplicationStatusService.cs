using Backend.Models;

namespace Backend.Services
{
    public interface IApplicationStatusService
    {
        Task<List<ApplicationStatus>> GetAllApplicationStatusesAsync();

        Task<ApplicationStatus?> GetApplicationStatusByIdAsync(int id);
        Task<ApplicationStatus> AddApplicationStatusAsync(ApplicationStatus ApplicationStatus);
        Task<ApplicationStatus> UpdateApplicationStatusAsync(int id, ApplicationStatus ApplicationStatus);
        Task DeleteApplicationStatusAsync(int id);
    }
}
