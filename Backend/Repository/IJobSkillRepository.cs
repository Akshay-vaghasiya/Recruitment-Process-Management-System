using Backend.Models;

namespace Backend.Repository
{
    public interface IJobSkillRepository
    {
        Task<List<JobSkill>> GetAllJobSkillsAsync();

        Task<JobSkill?> GetJobSkillByIdAsync(int id);

        Task<JobSkill> AddJobSkillAsync(JobSkill jobSkill);

        Task<JobSkill?> GetJobSkillByJobAndSkill(int jobid, int skillid);

        Task<JobSkill> UpdateJobSkillAsync(JobSkill jobSkill);

        Task DeleteJobSkillAsync(int id);
    }
}
