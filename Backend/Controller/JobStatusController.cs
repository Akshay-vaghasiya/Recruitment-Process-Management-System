using Backend.Models;
using Backend.Services;
using Backend.Services.impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="ADMIN")]
    public class JobStatusController : ControllerBase
    {
        private readonly IJobStatusService _service;

        public JobStatusController(IJobStatusService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobStatuses()
        {
            try
            {
                return Ok(await _service.GetAllJobStatusesAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobStatusById(int id)
        {
            try
            {
                var jobStatus = await _service.GetJobStatusByIdAsync(id);
                if (jobStatus == null)
                    return NotFound("Job status not found.");
                return Ok(jobStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddJobStatus([FromBody] JobStatus jobStatus)
        {
            try
            {
                if(jobStatus.Name.Equals("") || jobStatus == null)
                {
                    return BadRequest("jobstatus should not be empty!");
                }
                await _service.AddJobStatusAsync(jobStatus);
                return CreatedAtAction(nameof(GetJobStatusById), new { id = jobStatus.PkJobStatusId }, jobStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobStatus(int id, [FromBody] JobStatus jobStatus)
        {
            try
            {
                if(jobStatus.Name.Equals("") || jobStatus == null)
                {
                    return BadRequest("jobstatus should not be empty!");
                }
                await _service.UpdateJobStatusAsync(id, jobStatus);
                return Ok("Sucessfully updated!!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobStatus(int id)
        {
            try
            {
                await _service.DeleteJobStatusAsync(id);
                return Ok("Sucessfully deleted!!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
