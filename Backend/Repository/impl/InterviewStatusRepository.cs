using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class InterviewStatusRepository : IInterviewStatusRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public InterviewStatusRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<List<InterviewStatus>> GetAllInterviewStatusesAsync()
        {
            return await _context.InterviewStatuses.Include(ds => ds.Interviews).ToListAsync();
        }

        public async Task<InterviewStatus?> GetInterviewStatusByIdAsync(int? id)
        {
            return await _context.InterviewStatuses.Include(ds => ds.Interviews)
            .FirstOrDefaultAsync(js => js.PkInterviewStatusId == id);
        }

        public async Task<InterviewStatus?> GetInterviewStatusByNameAsync(string? name)
        {
            return await _context.InterviewStatuses.Include(ds => ds.Interviews)
            .FirstOrDefaultAsync(js => js.Name == name);
        }

        public async Task<InterviewStatus> AddInterviewStatusAsync(InterviewStatus InterviewStatus)
        {
            _context.InterviewStatuses.Add(InterviewStatus);
            await _context.SaveChangesAsync();

            return InterviewStatus;
        }

        public async Task<InterviewStatus> UpdateInterviewStatusAsync(InterviewStatus InterviewStatus)
        {
            _context.InterviewStatuses.Update(InterviewStatus);
            await _context.SaveChangesAsync();

            return InterviewStatus;
        }

        public async Task DeleteInterviewStatusAsync(int id)
        {
            var InterviewStatus = await _context.InterviewStatuses.FindAsync(id);
            if (InterviewStatus != null)
            {
                _context.InterviewStatuses.Remove(InterviewStatus);
                await _context.SaveChangesAsync();
            }
        }
    }
}
