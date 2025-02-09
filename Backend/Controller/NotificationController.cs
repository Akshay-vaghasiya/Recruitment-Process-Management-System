using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationController(INotificationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddNotification(Notification notification)
        {
            try
            {
                if (notification == null)
                {
                    return BadRequest("notification must have not be null");
                }

                if (notification.Message == null || notification.Message.Equals(""))
                {
                    return BadRequest("notification message should not be null");
                }

                return StatusCode(201, await _service.AddNotification(notification));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            try
            {
                return Ok(await _service.GetNotificationsAsync());

            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{notificationId}")]
        public async Task<IActionResult> UpdateNotification(int notificationId, [FromBody] Notification notification)
        {
            try
            {
                return Ok(await _service.UpdateNotification(notificationId, notification));
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{notificationId}")] 
        public async Task<IActionResult> DeleteNotification(int notificationId)
        {
            try
            {
                await _service.DeleteNotification(notificationId);
                return Ok("sucessfully deleted notification");

            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
