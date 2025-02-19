using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN,RECRUITER")]
    public class InterviewStatusController : ControllerBase
    {
        private readonly IInterviewStatusService _service;

        public InterviewStatusController(IInterviewStatusService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllInterviewStatuses()
        {
            try
            {
                return Ok(await _service.GetAllInterviewStatusesAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInterviewStatusById(int id)
        {
            try
            {
                var InterviewStatus = await _service.GetInterviewStatusByIdAsync(id);
                if (InterviewStatus == null)
                    return NotFound("Interview status not found.");
                return Ok(InterviewStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddInterviewStatus([FromBody] InterviewStatus InterviewStatus)
        {
            try
            {
                if (InterviewStatus.Name.Equals("") || InterviewStatus == null)
                {
                    return BadRequest("Interviewstatus should not be empty!");
                }
                await _service.AddInterviewStatusAsync(InterviewStatus);
                return CreatedAtAction(nameof(GetInterviewStatusById), new { id = InterviewStatus.PkInterviewStatusId }, InterviewStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInterviewStatus(int id, [FromBody] InterviewStatus InterviewStatus)
        {
            try
            {
                if (InterviewStatus.Name.Equals("") || InterviewStatus == null)
                {
                    return BadRequest("Interviewstatus should not be empty!");
                }

                return Ok(await _service.UpdateInterviewStatusAsync(id, InterviewStatus));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterviewStatus(int id)
        {
            try
            {
                await _service.DeleteInterviewStatusAsync(id);
                return Ok("Sucessfully deleted!!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
