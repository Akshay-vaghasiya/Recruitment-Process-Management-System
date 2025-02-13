using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class InterviewPanelController : ControllerBase
    {
        private readonly IInterviewPanelService _service;

        public InterviewPanelController(IInterviewPanelService service)
        {
            _service = service;
        }

        [HttpPost("{userId}/{interviewId}")]
        public async Task<IActionResult> AddInterviewPanel(int userId, int interviewId)
        {
            try
            {
                var result = await _service.AddInterviewPanel(userId, interviewId);
                return StatusCode(201, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{interviewId}")]
        public async Task<IActionResult> GetInterviewPanalByInterview(int interviewId)
        {
            try
            {
                return Ok(await _service.GetPanelByInterview(interviewId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetInterviewPanels()
        {
            try
            {
                return Ok(await _service.GetInterviewPanelsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{interviewPanelId}")]
        public async Task<IActionResult> DeleteInterviewPanel(int interviewPanelId)
        {
            try
            {
                await _service.DeleteInterviewPanel(interviewPanelId);
                return Ok("sucessfully panel deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
