using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class ResumeReviewRepository : IResumeReviewRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public ResumeReviewRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<ResumeReview> AddResumeReview(ResumeReview resumeReview)
        {
            await _context.ResumeReviews.AddAsync(resumeReview);
            await _context.SaveChangesAsync();

            return resumeReview;
        }

        public async Task<ResumeReview?> GetResumeReviewByJobAndCandidate(int? jobId, int? candidateId)
        {
            return await _context.ResumeReviews.FirstOrDefaultAsync(r => r.FkCandidateId == candidateId && r.FkJobPositionId == jobId);
        }


        public async Task<ResumeReview?> GetResumeReviewById(int resumeReviewId)
        {
            return await _context.ResumeReviews.FirstOrDefaultAsync();
        }

        public async Task<List<ResumeReview>> GetResumeReviewsAsync()
        {
            return await _context.ResumeReviews
                .Include(r => r.FkCandidate)
                .Include(r => r.FkJobPosition)
                .ToListAsync();
        }

        public async Task<ResumeReview> UpdateResumeReview(ResumeReview resumeReview)
        {
            _context.ResumeReviews.Update(resumeReview);
            await _context.SaveChangesAsync();

            return resumeReview;
        }

        public async Task DeleteResumeReview(int resumeReviewId)
        {
            var resumeReview = await _context.ResumeReviews.FindAsync(resumeReviewId);
            if (resumeReview != null)
            {
                _context.ResumeReviews.Remove(resumeReview);
                await _context.SaveChangesAsync();
            }
        }
    }
}
