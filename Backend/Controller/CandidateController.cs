using Backend.Dtos;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _service;

        public CandidateController(ICandidateService candidateService)
        {
            _service = candidateService;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AddCandidate([FromForm] CandidateDto candidateDto)
        {
            try
            {
                if (candidateDto == null)
                {
                    return BadRequest("please fill details");
                }

                if (candidateDto.Resume == null || candidateDto.Resume.Length == 0)
                {
                    return BadRequest("please upload valid resume");
                }

                var result = await _service.AddCandidate(candidateDto);

                return StatusCode(201, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetCandidates() {

            try
            {
                var result = await _service.GetCandidatesAsync();
                return StatusCode(200, result);
            }
            catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateCandidate(int id, [FromForm] CandidateDto candidateDto)
        {
            try
            {
                var result = await _service.UpdateCandidate(id, candidateDto);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{candidateId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteCandidate(int candidateId)
        {
            try
            {
                await _service.DeleteCandidate(candidateId);
                return Ok("Successfully deleted candidate");
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("addCandidateSkill/{candidateId}/{skillId}/{yearOfExp}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AddCandidateSkill(int candidateId, int skillId, int yearOfExp)
        {
            try
            {
                var result = await _service.AddCandidateSkill(candidateId, skillId, yearOfExp);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("deleteCandidateSkill/{candidateSkillId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteCandidateSkill(int candidateSkillId)
        {
            try
            {
                await _service.DeleteCandidateSkill(candidateSkillId);
                return StatusCode(200, "candidate skill deleted sucessfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
