using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public JobApplicationRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<JobApplication> AddJobApplication(JobApplication jobApplication)
        {
            await _context.JobApplications.AddAsync(jobApplication);
            await _context.SaveChangesAsync();

            return jobApplication;
        }

        public async Task<JobApplication?> GetJobApplicationByJobAndCandidate(int? jobId, int? candidateId)
        {
            return await _context.JobApplications
                .Include(ja => ja.FkCandidate)
                .Include(ja => ja.FkJobPosition)
                .Include(ja => ja.FkStatus)
                .FirstOrDefaultAsync(ja =>  ja.FkCandidateId == candidateId && ja.FkJobPositionId == jobId);
        }

        public async Task<List<JobApplication>> GetApplicationsAsync()
        {
            return await _context.JobApplications
                .Include(ja => ja.FkCandidate)
                .Include(ja => ja.FkJobPosition)
                .Include(ja => ja.FkStatus)
                .ToListAsync();
        }

        public async Task<JobApplication?> GetJobApplicationById(int id)
        {
            return await _context.JobApplications.FirstOrDefaultAsync(ja => ja.PkJobApplicationId == id);
        }

        public async Task<JobApplication> UpdateJobAppliction(JobApplication jobApplication)
        {
            _context.JobApplications.Update(jobApplication);
            await _context.SaveChangesAsync();

            return jobApplication;
        }

        public async Task DeleteJobAppliction(int id)
        {
            var jobApplication = await _context.JobApplications.FindAsync(id);
            if (jobApplication != null)
            {
                _context.JobApplications.Remove(jobApplication);
                await _context.SaveChangesAsync();
            }
        }

    }
}
