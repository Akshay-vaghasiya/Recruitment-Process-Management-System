using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class CandidateNotificationController : ControllerBase
    {
        private readonly ICandidateNotificationService _service;

        public CandidateNotificationController(ICandidateNotificationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddCandidateNotification(CandidateNotification CandidateNotification)
        {
            try
            {
                if (CandidateNotification == null)
                {
                    return BadRequest("Candidate notification must have not be null");
                }

                if (CandidateNotification.Message == null || CandidateNotification.Message.Equals(""))
                {
                    return BadRequest("Candidate notification message should not be null");
                }

                return StatusCode(201, await _service.AddCandidateNotification(CandidateNotification));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCandidateNotifications()
        {
            try
            {
                return Ok(await _service.GetCandidateNotificationsAsync());

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{CandidateNotificationId}")]
        public async Task<IActionResult> UpdateCandidateNotification(int CandidateNotificationId, [FromBody] CandidateNotification CandidateNotification)
        {
            try
            {
                return Ok(await _service.UpdateCandidateNotification(CandidateNotificationId, CandidateNotification));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{CandidateNotificationId}")]
        public async Task<IActionResult> DeleteCandidateNotification(int CandidateNotificationId)
        {
            try
            {
                await _service.DeleteCandidateNotification(CandidateNotificationId);
                return Ok("sucessfully deleted CandidateNotification");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
