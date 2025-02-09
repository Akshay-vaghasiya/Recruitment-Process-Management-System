using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class ApplicationStatusRepository : IApplicationStatusRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public ApplicationStatusRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<List<ApplicationStatus>> GetAllApplicationStatusesAsync()
        {
            return await _context.ApplicationStatuses.Include(s => s.JobApplications).ToListAsync();
        }

        public async Task<ApplicationStatus?> GetApplicationStatusByIdAsync(int? id)
        {
            return await _context.ApplicationStatuses.Include(s => s.JobApplications)
            .FirstOrDefaultAsync(s => s.PkApplicationStatusId == id);
        }

        public async Task<ApplicationStatus?> GetApplicationStatusByNameAsync(string? name)
        {
            return await _context.ApplicationStatuses.Include(s => s.JobApplications)
            .FirstOrDefaultAsync(s => s.Name == name);
        }

        public async Task<ApplicationStatus> AddApplicationStatusAsync(ApplicationStatus ApplicationStatus)
        {
            _context.ApplicationStatuses.Add(ApplicationStatus);
            await _context.SaveChangesAsync();

            return ApplicationStatus;
        }

        public async Task<ApplicationStatus> UpdateApplicationStatusAsync(ApplicationStatus ApplicationStatus)
        {
            _context.ApplicationStatuses.Update(ApplicationStatus);
            await _context.SaveChangesAsync();

            return ApplicationStatus;
        }

        public async Task DeleteApplicationStatusAsync(int id)
        {
            var ApplicationStatus = await _context.ApplicationStatuses.FindAsync(id);
            if (ApplicationStatus != null)
            {
                _context.ApplicationStatuses.Remove(ApplicationStatus);
                await _context.SaveChangesAsync();
            }
        }
    }
}
