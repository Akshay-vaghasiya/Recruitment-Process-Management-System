using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]

    public class CandidateNotificationController : ControllerBase
    {
        private readonly ICandidateNotificationService _service;

        public CandidateNotificationController(ICandidateNotificationService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
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
        [Authorize(Roles = "CANDIDATE,ADMIN")]
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

        [HttpGet("{candidateId}")]
        [Authorize(Roles = "CANDIDATE,ADMIN")]
        public async Task<IActionResult> GetCandidateNotificationsByCandidate(int candidateId)
        {
            try
            {
                return Ok(await _service.GetCandidateNotificationByCandidate(candidateId));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{CandidateNotificationId}")]
        [Authorize(Roles = "CANDIDATE,ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
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
