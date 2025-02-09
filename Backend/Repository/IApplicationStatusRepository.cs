using Backend.Models;

namespace Backend.Repository
{
    public interface IApplicationStatusRepository
    {
        Task<List<ApplicationStatus>> GetAllApplicationStatusesAsync();
        Task<ApplicationStatus?> GetApplicationStatusByIdAsync(int? id);
        Task<ApplicationStatus?> GetApplicationStatusByNameAsync(string? name);
        Task<ApplicationStatus> AddApplicationStatusAsync(ApplicationStatus ApplicationStatus);
        Task<ApplicationStatus> UpdateApplicationStatusAsync(ApplicationStatus ApplicationStatus);
        Task DeleteApplicationStatusAsync(int id);
    }
}
