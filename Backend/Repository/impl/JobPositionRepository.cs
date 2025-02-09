using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class JobPositionRepository : IJobPositionRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public JobPositionRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<List<JobPosition>> GetAllJobPositionsAsync()
        {
            return await _context.JobPositions
            .Include(j => j.FkStatus)
            .Include(j => j.JobSkills)
            .ThenInclude(js => js.FkSkill)
            .Include(j => j.ResumeReviews)
            .ToListAsync();
        }

        public async Task<JobPosition?> GetJobPositionByIdAsync(int? id)
        {
            return await _context.JobPositions
            .Include(j => j.FkStatus)
            .Include(j => j.JobSkills)
            .ThenInclude(js => js.FkSkill)
            .Include(j => j.ResumeReviews)
            .FirstOrDefaultAsync(j => j.PkJobPositionId == id);
        }

        public async Task<JobPosition> AddJobPositionAsync(JobPosition jobPosition)
        {
            _context.JobPositions.Add(jobPosition);
            await _context.SaveChangesAsync();
            return jobPosition;
        }

        public async Task<JobPosition> UpdateJobPositionAsync(JobPosition jobPosition)
        {
            _context.JobPositions.Update(jobPosition);
            await _context.SaveChangesAsync();
            return jobPosition;
        }

        public async Task DeleteJobPositionAsync(int id)
        {
            var jobPosition = await _context.JobPositions.FindAsync(id);
            if (jobPosition != null)
            {
                _context.JobPositions.Remove(jobPosition);
                await _context.SaveChangesAsync();
            }
        }
    }

}
