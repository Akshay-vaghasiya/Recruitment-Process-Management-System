using Backend.Dtos;
using Backend.Models;
using Backend.Services;

using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPositionController : ControllerBase
    {
        private readonly IJobPositionService _service;

        public JobPositionController(IJobPositionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobPositions()
        {
            try
            {
                return Ok(await _service.GetAllJobPositionsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobPositionById(int id)
        {
            try
            {
                var jobPosition = await _service.GetJobPositionByIdAsync(id);
                if (jobPosition == null)
                    return NotFound("Job position not found.");
                return Ok(jobPosition);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddJobPosition([FromBody] JobPositionDto jobPositionDto)
        {
            try
            {
                if (jobPositionDto == null)
                    return BadRequest("Invalid job position data.");

                var result = await _service.AddJobPositionAsync(jobPositionDto);
                return CreatedAtAction(nameof(GetJobPositionById), new { id = result.PkJobPositionId }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobPosition(int id, [FromBody] JobPositionDto jobPositionDto)
        {
            try
            {
                if (jobPositionDto == null)
                    return BadRequest("Invalid job position data.");


                return Ok(await _service.UpdateJobPositionAsync(id, jobPositionDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobPosition(int id)
        {
            try
            {
                JobPosition? jobPosition = await _service.GetJobPositionByIdAsync(id);
                if (jobPosition == null) return NotFound("job position not found");

                await _service.DeleteJobPositionAsync(id);
                return Ok("sucessfully deleted!!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("addjobskill/{jobpositionId}/{skillId}/{isRequire}")]
        public async Task<IActionResult> AddJobSkill(int jobpositionId, int skillId, int isRequire)
        {
            try
            {
                if (isRequire != 0 && isRequire != 1)
                {
                    return BadRequest("isRequire either contains 0 or 1 values");
                }
                await _service.AddJobSkill(jobpositionId, skillId, isRequire);
                return Ok("sucessfully skill added");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("deletejobskill/{jobskillId}")]
        public async Task<IActionResult> DeleteJobSkill(int jobskillId)
        {
            try
            {
                await _service.DeleteJobSkill(jobskillId);
                return Ok("sucessfully job skill deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
