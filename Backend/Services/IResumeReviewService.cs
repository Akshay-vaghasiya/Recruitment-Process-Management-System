using Backend.Models;

namespace Backend.Services
{
    public interface IResumeReviewService
    {
        Task<ResumeReview> AddResumeReview(int userId, ResumeReview resumeReview);
        Task<List<ResumeReview>> GetResumeReviews();
        Task<ResumeReview> UpdateResumeReview(int userId, int resumeReviewId, ResumeReview resumeReview);
        Task DeleteResumeReview(int id);
    }
}
