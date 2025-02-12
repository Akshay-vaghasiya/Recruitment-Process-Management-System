using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "REVIEWER,ADMIN")]
    public class ResumeReviewController : ControllerBase
    {
        private readonly IResumeReviewService _service;

        public ResumeReviewController(IResumeReviewService service)
        {
            _service = service;
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddResumeReview(int userId, [FromBody] ResumeReview resumeReview)
        {
            try
            {
                var reuslt = await _service.AddResumeReview(userId, resumeReview);
                return StatusCode(201, reuslt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetResumeReviews()
        {
            try
            {
                return Ok(await _service.GetResumeReviews());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{userId}/{resumeReviewId}")]
        public async Task<IActionResult> UpdateResumeReview(int userId, int resumeReviewId, ResumeReview resumeReview)
        {
            try
            {
                return Ok(await _service.UpdateResumeReview(userId, resumeReviewId, resumeReview));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{resumeReviewId}")]
        public async Task<IActionResult> DeleteResumeReview(int resumeReviewId)
        {
            try
            {
                await _service.DeleteResumeReview(resumeReviewId);

                return Ok("sucessfully deleted review");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
