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
    public class InterviewRoundController : ControllerBase
    {
        private readonly IInterviewRoundService _service;

        public InterviewRoundController(IInterviewRoundService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInterviewRoundes()
        {
            try
            {
                return Ok(await _service.GetAllInterviewRoundesAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInterviewRoundById(int id)
        {
            try
            {
                var InterviewRound = await _service.GetInterviewRoundByIdAsync(id);
                if (InterviewRound == null)
                    return NotFound("Interview Round not found.");
                return Ok(InterviewRound);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddInterviewRound([FromBody] InterviewRound InterviewRound)
        {
            try
            {
                if (InterviewRound.Name.Equals("") || InterviewRound == null)
                {
                    return BadRequest("InterviewRound should not be empty!");
                }
                await _service.AddInterviewRoundAsync(InterviewRound);
                return CreatedAtAction(nameof(GetInterviewRoundById), new { id = InterviewRound.PkInterviewRoundId }, InterviewRound);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInterviewRound(int id, [FromBody] InterviewRound InterviewRound)
        {
            try
            {
                if (InterviewRound.Name.Equals("") || InterviewRound == null)
                {
                    return BadRequest("InterviewRound should not be empty!");
                }

                return Ok(await _service.UpdateInterviewRoundAsync(id, InterviewRound));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterviewRound(int id)
        {
            try
            {
                await _service.DeleteInterviewRoundAsync(id);
                return Ok("Sucessfully deleted!!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
