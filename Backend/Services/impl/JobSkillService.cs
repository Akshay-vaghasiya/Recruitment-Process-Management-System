using Backend.Models;
using Backend.Repository;
using Backend.Repository.impl;

namespace Backend.Services.impl
{
    public class JobSkillService : IJobSkillService
    {
        private readonly IJobSkillRepository _repository;

        public JobSkillService(IJobSkillRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<JobSkill>> GetAllJobSkillsAsync()
        {
            return await _repository.GetAllJobSkillsAsync();
        }

        public async Task<JobSkill?> GetJobSkillByIdAsync(int id)
        {
            return await _repository.GetJobSkillByIdAsync(id);
        }

        public async Task<JobSkill?> GetJobSkillByJobAndSkill(int jobid, int skillid)
        {
            return await _repository.GetJobSkillByJobAndSkill(jobid, skillid);
        }

        public async Task<JobSkill> AddJobSkillAsync(JobSkill jobSkill)
        {
            return await _repository.AddJobSkillAsync(jobSkill);
        }

        public async Task<JobSkill> UpdateJobSkillAsync(JobSkill jobSkill)
        {
            return await _repository.UpdateJobSkillAsync(jobSkill);
        }

        public async Task DeleteJobSkillAsync(int id)
        {
            await _repository.DeleteJobSkillAsync(id);
        }
    }
}
