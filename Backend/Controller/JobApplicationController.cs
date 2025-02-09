﻿using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class JobApplicationController : ControllerBase
    {
        private readonly IJobApplicationService _service;

        public JobApplicationController(IJobApplicationService jobApplicationService)
        {
            _service = jobApplicationService;
        }

        [HttpPost("{candidateId}/{jobPositionId}")]
        public async Task<IActionResult> AddJobApplication(int candidateId, int jobPositionId)
        {
            try
            {
                var result = await _service.AddJobApplication(jobPositionId, candidateId);
                return StatusCode(201, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetJobApplications()
        {
            try
            {
                return Ok(await _service.GetJobApplicationsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{jobApplicationId}/{applicationStatusId}")]
        public async Task<IActionResult> UpdateJobApplicationStatus(int applicationStatusId, int jobApplicationId)
        {
            try
            {
                return Ok(await _service.UpdateJobApplicationStatus(jobApplicationId, applicationStatusId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{JobApplicationId}")]
        public async Task<IActionResult> DeleteJobApplication(int JobApplicationId)
        {
            try
            {
                await _service.DeleteJobApplication(JobApplicationId);
                return Ok("job application deleted!!");
            }catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
