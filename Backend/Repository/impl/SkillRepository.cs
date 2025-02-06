using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Backend.Repository.impl
{
    public class SkillRepository : ISkillRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public SkillRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<List<Skill>> GetAllSkillsAsync()
        {
            return await _context.Skills.Include(s => s.JobSkills).ToListAsync();
        }

        public async Task<Skill?> GetSkillByIdAsync(int id)
        {
            return await _context.Skills.Include(s => s.JobSkills)
            .FirstOrDefaultAsync(s => s.PkSkillId == id);
        }

        public async Task<Skill?> GetSkillByNameAsync(string name)
        {
            return await _context.Skills.Include(s => s.JobSkills)
            .FirstOrDefaultAsync(s => s.Name == name);
        }

        public async Task<Skill> AddSkillAsync(Skill skill)
        {
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return skill;
        }

        public async Task<Skill> UpdateSkillAsync(Skill skill)
        {
            _context.Skills.Update(skill);
            await _context.SaveChangesAsync();

            return skill;
        }

        public async Task DeleteSkillAsync(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill != null)
            {
                _context.Skills.Remove(skill);
                await _context.SaveChangesAsync();
            }
        }
    }
}
