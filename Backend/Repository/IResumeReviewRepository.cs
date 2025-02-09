using Backend.Models;

namespace Backend.Repository
{
    public interface IResumeReviewRepository
    {
        Task<ResumeReview> AddResumeReview(ResumeReview resumeReview);
        Task<ResumeReview?> GetResumeReviewById(int resumeReviewId);
        Task<List<ResumeReview>> GetResumeReviewsAsync();
        Task<ResumeReview> UpdateResumeReview(ResumeReview resumeReview);
        Task DeleteResumeReview(int resumeReviewId);

        Task<ResumeReview?> GetResumeReviewByJobAndCandidate(int? jobId, int? candidateId);
    }
}
