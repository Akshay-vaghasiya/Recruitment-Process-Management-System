using Backend.Dtos;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public CandidateRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<Candidate?> GetCandidateByEmail(string? email)
        {
            var result = await _context.Candidates.Where(c => c.Email == email)
                .Include(c => c.CandidateSkills)
                .ThenInclude(cs => cs.FkSkill)
                .Include(c => c.Documents)
                .ThenInclude(d => d.FkStatus)
                .Include(c => c.Documents)
                .ThenInclude(d => d.FkDocumentType)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<Candidate?> GetCandidateById(int? id)
        {
            var result = await _context.Candidates.Where(c => c.PkCandidateId == id)
                .Include(c => c.CandidateSkills)
                .ThenInclude(cs => cs.FkSkill)
                .Include(c => c.Documents)
                .ThenInclude(d => d.FkStatus)
                .Include(c => c.Documents)
                .ThenInclude(d => d.FkDocumentType)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<Candidate> AddCandidate(Candidate candidate)
        {
            
            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();

            return candidate;
        }

        public async Task<Candidate?> UpdateCandidate(Candidate candidate)
        {
            _context.Candidates.Update(candidate);
            await _context.SaveChangesAsync();

            return candidate;
        }

        public async Task<List<Candidate>> GetCandidatesAsync()
        {
            return await _context.Candidates
                .Include(c => c.CandidateSkills)
                .ThenInclude(cs => cs.FkSkill)
                .Include(c => c.Documents)
                .ThenInclude(d => d.FkStatus)
                .Include(c => c.Documents)
                .ThenInclude(d => d.FkDocumentType)
                .ToListAsync();
        }

        public async Task DeleteCandidate(int id)
        {
            Candidate? candidate = await _context.Candidates.FindAsync(id);
            if(candidate != null)
            {
                _context.Candidates.Remove(candidate);
                await _context.SaveChangesAsync();
            }
        }

        public async Task BulkAddCandidateAsync(List<Candidate> candidates)
        {
            await _context.Candidates.AddRangeAsync(candidates);
            await _context.SaveChangesAsync();
        }

    }
}
