using Backend.Models;

namespace Backend.Services
{
    public interface IJobStatusService
    {
        Task<List<JobStatus>> GetAllJobStatusesAsync();
        Task<JobStatus?> GetJobStatusByIdAsync(int id);
        Task<JobStatus?> AddJobStatusAsync(JobStatus jobStatus);
        Task<JobStatus> UpdateJobStatusAsync(int id,JobStatus jobStatus);
        Task DeleteJobStatusAsync(int id);
    }
}
