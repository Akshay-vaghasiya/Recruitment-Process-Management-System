using Backend.Dtos;
using Backend.Models;

namespace Backend.Services
{
    public interface IJobPositionService
    {
        Task<List<JobPosition>> GetAllJobPositionsAsync();

        Task<JobPosition?> GetJobPositionByIdAsync(int id);

        Task<JobPosition> AddJobPositionAsync(JobPositionDto jobPositionDto);

        Task<JobPosition> UpdateJobPositionAsync(int id,JobPositionDto jobPositionDto);

        Task AddJobSkill(int jobid, int skillid, int isRequire);

        Task DeleteJobSkill(int jobskillid);

        Task DeleteJobPositionAsync(int id);
    }
}
