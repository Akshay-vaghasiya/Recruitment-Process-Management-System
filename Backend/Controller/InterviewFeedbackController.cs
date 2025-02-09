using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class InterviewFeedbackController : ControllerBase
    {
        private readonly IInterviewFeedbackService _service;

        public InterviewFeedbackController (IInterviewFeedbackService service)
        {
            _service = service;
        }

        [HttpPost] 
        public async Task<IActionResult> AddInterviewFeedback(InterviewFeedback interviewFeedback)
        {
            try
            {
                if(interviewFeedback == null || interviewFeedback.Comments == null || interviewFeedback.Comments.Equals(""))
                {
                    return BadRequest("interview feedback must have comment and it would be not null");
                }

                if(interviewFeedback.Rating == null)
                {
                    return BadRequest("interview feedback rating would be require");
                }
               
                return StatusCode(201, await _service.AddInterviewFeedback(interviewFeedback));

            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetInterviewFeedbacks()
        {
            try
            {
                return Ok(await _service.GetInterviewFeedbacksAsync());

            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{interviewFeedbackId}")]
        public async Task<IActionResult> UpdateInterviewFeedback(int interviewFeedbackId, [FromBody] InterviewFeedback interviewFeedback)
        {
            try
            {
                if (interviewFeedback == null || interviewFeedback.Comments == null || interviewFeedback.Comments.Equals(""))
                {
                    return BadRequest("interview feedback must have comment and it would be not null");
                }

                if (interviewFeedback.Rating == null)
                {
                    return BadRequest("interview feedback rating would be require");
                }

                return Ok(await _service.UpdateInterviewFeedback(interviewFeedbackId, interviewFeedback));
            }
            catch (Exception ex) { 
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{interviewFeedbackId}")]
        public async Task<IActionResult> DeleteInterviewFeedback(int interviewFeedbackId)
        {
            try
            {
                await _service.DeleteInterviewFeedback(interviewFeedbackId);
                return Ok("sucessfully feedback deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
