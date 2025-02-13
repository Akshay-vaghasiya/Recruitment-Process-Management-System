using Backend.Dtos;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class InterviewController : ControllerBase
    {
        private readonly IInterviewService _service;

        public InterviewController(IInterviewService interviewService)
        {
            _service = interviewService;
        }

        [HttpPost]
        public async Task<IActionResult> AddInterview(InterviewDto interviewDto)
        {
            try
            {
                var result = await _service.AddInterview(interviewDto);
                return StatusCode(201, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }   
        }

        [HttpGet]
        public async Task<IActionResult> GetInterviews()
        {
            try
            {
                return Ok(await _service.GetInterviewsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{candidateId}/{positionId}")]
        public async Task<IActionResult> GetInterviewsByCandidateAndPosition(int candidateId, int positionId)
        {
            try
            {
                return Ok(await _service.GetInterviewsByCandidateAndPosition(candidateId, positionId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("updatestatus/{interviewId}/{interviewStatusId}")]
        public async Task<IActionResult> UpdateStatus(int interviewId, int interviewStatusId)
        {
            try
            {
                return StatusCode(200, await _service.UpdateInterviewStatus(interviewId, interviewStatusId));
            } 
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInteview(int id)
        {
            try
            {
                await _service.DeleteInterview(id);
                return Ok("sucessfully delete interview");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
