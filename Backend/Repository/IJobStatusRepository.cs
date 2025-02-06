using Backend.Models;

namespace Backend.Repository
{
    public interface IJobStatusRepository
    {
        Task<List<JobStatus>> GetAllJobStatusesAsync();

        Task<JobStatus?> GetJobStatusByIdAsync(int? id);

        Task<JobStatus?> GetJobStatusByNameAsync(string name);

        Task<JobStatus> AddJobStatusAsync(JobStatus jobStatus);

        Task<JobStatus> UpdateJobStatusAsync(JobStatus jobStatus);

        Task DeleteJobStatusAsync(int id);
    }
}
