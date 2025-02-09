using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class InterviewRoundRepository : IInterviewRoundRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public InterviewRoundRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<List<InterviewRound>> GetAllInterviewRoundesAsync()
        {
            return await _context.InterviewRounds.Include(dr => dr.Interviews).ToListAsync();
        }

        public async Task<InterviewRound?> GetInterviewRoundByIdAsync(int? id)
        {
            return await _context.InterviewRounds.Include(dr => dr.Interviews)
            .FirstOrDefaultAsync(dr => dr.PkInterviewRoundId == id);
        }

        public async Task<InterviewRound?> GetInterviewRoundByNameAsync(string? name)
        {
            return await _context.InterviewRounds.Include(dr => dr.Interviews)
            .FirstOrDefaultAsync(dr => dr.Name == name);
        }

        public async Task<InterviewRound> AddInterviewRoundAsync(InterviewRound InterviewRound)
        {
            _context.InterviewRounds.Add(InterviewRound);
            await _context.SaveChangesAsync();

            return InterviewRound;
        }

        public async Task<InterviewRound> UpdateInterviewRoundAsync(InterviewRound InterviewRound)
        {
            _context.InterviewRounds.Update(InterviewRound);
            await _context.SaveChangesAsync();

            return InterviewRound;
        }

        public async Task DeleteInterviewRoundAsync(int id)
        {
            var InterviewRound = await _context.InterviewRounds.FindAsync(id);
            if (InterviewRound != null)
            {
                _context.InterviewRounds.Remove(InterviewRound);
                await _context.SaveChangesAsync();
            }
        }
    }
}
