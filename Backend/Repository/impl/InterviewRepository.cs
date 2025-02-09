using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class InterviewRepository : IInterviewRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public InterviewRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<Interview> AddInterview(Interview interview)
        {
            await _context.Interviews.AddAsync(interview);
            await _context.SaveChangesAsync();

            return interview;
        }


        public async Task<Interview?> GetInterviewByCandidateAndPositionAndRound(int? candidateId, int? positionId, int? roundId)
        {
            return await _context.Interviews
                .FirstOrDefaultAsync(i => i.FkJobPositionId == positionId &&
                                        i.FkCandidateId == candidateId && 
                                        i.FkInterviewRoundId == roundId);
        }

        public async Task<Interview?> GetInterviewById(int? id)
        {
            return await _context.Interviews.FirstOrDefaultAsync(i => i.PkInterviewId == id);
        }

        public async Task<List<Interview>> GetInterviewsAsync()
        {
            return await _context.Interviews
                .Include(i => i.FkInterviewRound)
                .Include(i => i.FkStatus)
                .Include(i => i.FkJobPosition).ToListAsync();
        }

        public async Task<Interview> UpdateInterview(Interview interview)
        {
            _context.Interviews.Update(interview);
            await _context.SaveChangesAsync();
            return interview;
        }

        public async Task DeleteInterview(int id)
        {
            Interview? interview = await _context.Interviews.FindAsync(id);
            if (interview != null)
            {
                _context.Interviews.Remove(interview);
                await _context.SaveChangesAsync();
            }
        }
    }
}
