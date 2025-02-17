using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class CandidateNotificationRepository : ICandidateNotificationRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public CandidateNotificationRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<CandidateNotification> AddCandidateNotification(CandidateNotification CandidateNotification)
        {
            await _context.CandidateNotifications.AddAsync(CandidateNotification);
            await _context.SaveChangesAsync();

            return CandidateNotification;
        }

        public async Task<List<CandidateNotification>> GetCandidateNotificationAsync()
        {
            return await _context.CandidateNotifications.ToListAsync();
        }

        public async Task<CandidateNotification?> GetCandidateNotificationById(int? id)
        {
            return await _context.CandidateNotifications.FirstOrDefaultAsync(n => n.PkNotificationId == id);
        }

        public async Task<List<CandidateNotification>> GetCandidateNotificationByCandidate(int candidateId)
        {
            return await _context.CandidateNotifications.Where(cn => cn.FkCandidateId == candidateId).ToListAsync();
        }

        public async Task<CandidateNotification> UpdateCandidateNotification(CandidateNotification CandidateNotification)
        {
            _context.CandidateNotifications.Update(CandidateNotification);
            await _context.SaveChangesAsync();

            return CandidateNotification;
        }

        public async Task DeleteCandidateNotification(int id)
        {
            CandidateNotification? CandidateNotification = await _context.CandidateNotifications.FindAsync(id);
            if (CandidateNotification != null)
            {
                _context.CandidateNotifications.Remove(CandidateNotification);
                await _context.SaveChangesAsync();
            }
        }
    }
}
