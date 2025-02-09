using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class CandidateSkillRepository 
        : ICandidateSkillRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public CandidateSkillRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<CandidateSkill> AddCandidateSkill(CandidateSkill candidateSkill)
        {
            await _context.CandidateSkills.AddAsync(candidateSkill);
            await _context.SaveChangesAsync();

            return candidateSkill;
        }

        public async Task<CandidateSkill?> GetCandidateSkillAsyncById(int candidateSkillId)
        {
           return await _context.CandidateSkills.Where(cs => cs.PkCandidateSkillId == candidateSkillId).FirstOrDefaultAsync();
        }

        public async Task<CandidateSkill?> GetCandidateSkillByCandidateAndSkill(int candidateId, int skillId)
        {
           return await _context.CandidateSkills.Where(cs => cs.FkSkillId == skillId && cs.FkCandidateId == candidateId).FirstOrDefaultAsync();
        }

        public async Task DeleteCandidateSkill(CandidateSkill candidateSkill)
        {
            _context.CandidateSkills.Remove(candidateSkill);
            await _context.SaveChangesAsync();
        }

        public async Task<CandidateSkill> UpdateCandidateSkill(CandidateSkill candidateSkill)
        {
            _context.CandidateSkills.Update(candidateSkill);
            await _context.SaveChangesAsync();

            return candidateSkill;
        }

    }
}
