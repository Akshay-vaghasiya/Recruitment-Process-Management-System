using Backend.Models;

namespace Backend.Services
{
    public interface IJobSkillService
    {
        Task<List<JobSkill>> GetAllJobSkillsAsync();

        Task<JobSkill?> GetJobSkillByIdAsync(int id);

        Task<JobSkill?> GetJobSkillByJobAndSkill(int jobid, int skillid);
        Task<JobSkill> AddJobSkillAsync(JobSkill jobSkill);
        Task<JobSkill> UpdateJobSkillAsync(JobSkill jobSkill);
        Task DeleteJobSkillAsync(int id);
    }
}
