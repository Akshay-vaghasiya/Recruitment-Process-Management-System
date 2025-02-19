using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class ApplicationStatusController : ControllerBase
    {
        private readonly IApplicationStatusService _service;

        public ApplicationStatusController(IApplicationStatusService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllApplicationStatuses()
        {
            try
            {
                return Ok(await _service.GetAllApplicationStatusesAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationStatusById(int id)
        {
            try
            {
                var ApplicationStatus = await _service.GetApplicationStatusByIdAsync(id);
                if (ApplicationStatus == null)
                    return NotFound("Application status not found.");
                return Ok(ApplicationStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddApplicationStatus([FromBody] ApplicationStatus ApplicationStatus)
        {
            try
            {
                if (ApplicationStatus.Name.Equals("") || ApplicationStatus == null)
                {
                    return BadRequest("Application status should not be empty!");
                }
                await _service.AddApplicationStatusAsync(ApplicationStatus);
                return CreatedAtAction(nameof(GetApplicationStatusById), new { id = ApplicationStatus.PkApplicationStatusId }, ApplicationStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplicationStatus(int id, [FromBody] ApplicationStatus ApplicationStatus)
        {
            try
            {
                if (ApplicationStatus.Name.Equals("") || ApplicationStatus == null)
                {
                    return BadRequest("Application status should not be empty!");
                }
            
                return Ok(await _service.UpdateApplicationStatusAsync(id, ApplicationStatus));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationStatus(int id)
        {
            try
            {
                await _service.DeleteApplicationStatusAsync(id);
                return Ok("Sucessfully deleted!!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
