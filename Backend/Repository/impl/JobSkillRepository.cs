using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Backend.Repository.impl
{
    public class JobSkillRepository : IJobSkillRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public JobSkillRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<List<JobSkill>> GetAllJobSkillsAsync()
        {
            return await _context.JobSkills
            .Include(js => js.FkJobPosition)
            .Include(js => js.FkSkill)
            .ToListAsync();
        }

        public async Task<JobSkill?> GetJobSkillByIdAsync(int id)
        {
            return await _context.JobSkills
            .Include(js => js.FkJobPosition)
            .Include(js => js.FkSkill)
            .FirstOrDefaultAsync(js => js.PkJobSkillId == id);
        }

        public async Task<JobSkill?> GetJobSkillByJobAndSkill(int jobid, int skillid)
        {
            return await _context.JobSkills.Where(j => j.FkSkillId == skillid && j.FkJobPositionId == jobid).FirstOrDefaultAsync();
        }

        public async Task<JobSkill> AddJobSkillAsync(JobSkill jobSkill)
        {
            _context.JobSkills.Add(jobSkill);
            await _context.SaveChangesAsync();

            return jobSkill;
        }

        public async Task<JobSkill> UpdateJobSkillAsync(JobSkill jobSkill)
        {
            _context.JobSkills.Update(jobSkill);
            await _context.SaveChangesAsync();

            return jobSkill;
        }

        public async Task DeleteJobSkillAsync(int id)
        {
            var jobSkill = await _context.JobSkills.FindAsync(id);
            if (jobSkill != null)
            {
                _context.JobSkills.Remove(jobSkill);
                await _context.SaveChangesAsync();
            }
        }
    }
}
