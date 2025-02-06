using Backend.Models;

namespace Backend.Repository
{
    public interface IJobPositionRepository
    {
        Task<List<JobPosition>> GetAllJobPositionsAsync();

        Task<JobPosition?> GetJobPositionByIdAsync(int id);

        Task<JobPosition> AddJobPositionAsync(JobPosition jobPosition);
        Task<JobPosition> UpdateJobPositionAsync(JobPosition jobPosition);

        Task DeleteJobPositionAsync(int id);
    }
}
