using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Backend.Repository.impl
{
    public class JobStatusRepository : IJobStatusRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public JobStatusRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<List<JobStatus>> GetAllJobStatusesAsync()
        {
            return await _context.JobStatuses.Include(js => js.JobPositions).ToListAsync();
        }

        public async Task<JobStatus?> GetJobStatusByIdAsync(int? id)
        {
            return await _context.JobStatuses.Include(js => js.JobPositions)
            .FirstOrDefaultAsync(js => js.PkJobStatusId == id);
        }

        public async Task<JobStatus?> GetJobStatusByNameAsync(string? name)
        {
            return await _context.JobStatuses.Include(js => js.JobPositions)
            .FirstOrDefaultAsync(js => js.Name == name);
        }

        public async Task<JobStatus?> AddJobStatusAsync(JobStatus? jobStatus)
        {
            if (jobStatus != null) {
                _context.JobStatuses.Add(jobStatus);
                await _context.SaveChangesAsync();
            }

            return jobStatus;
        }

        public async Task<JobStatus> UpdateJobStatusAsync(JobStatus jobStatus)
        {
            _context.JobStatuses.Update(jobStatus);
            await _context.SaveChangesAsync();

            return jobStatus;
        }

        public async Task DeleteJobStatusAsync(int id)
        {
            var jobStatus = await _context.JobStatuses.FindAsync(id);
            if (jobStatus != null)
            {
                _context.JobStatuses.Remove(jobStatus);
                await _context.SaveChangesAsync();
            }
        }
    }
}
