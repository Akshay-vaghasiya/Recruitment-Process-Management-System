using Backend.Dtos;
using Backend.Models;
using Backend.Services;
using Backend.Services.impl;
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
        [Authorize(Roles = "ADMIN,RECRUITER,HR")]
        public async Task<IActionResult> AddCandidate([FromForm] CandidateDto candidateDto)
        {
            try
            {
                if (candidateDto == null)
                {
                    return BadRequest("please fill details");
                }

                //if (candidateDto.Resume == null || candidateDto.Resume.Length == 0)
                //{
                //    return BadRequest("please upload valid resume");
                //}

                var result = await _service.AddCandidate(candidateDto);

                return StatusCode(201, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN,RECRUITER,HR")]
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
        [Authorize(Roles = "ADMIN,CANDIDATE,RECRUITER,HR")]
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
        [Authorize(Roles = "ADMIN,RECRUITER,HR")]
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

        [HttpGet("getCandidateSkill/{candidateId}")]
        [Authorize(Roles = "CANDIDATE")]
        public async Task<IActionResult> GetCandidateSkill(int candidateId)
        {
            try
            {
                var result = await _service.GetCandidateSkillByCandidate(candidateId);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("addCandidateSkill/{candidateId}/{skillId}/{yearOfExp}")]
        [Authorize(Roles = "ADMIN,CANDIDATE")]
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
        [Authorize(Roles = "ADMIN,CANDIDATE")]
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

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var object1 = await _service.AuthenticateCandidate(loginDto);
                if (object1 == null) return Unauthorized("Invalid credentials");
                return Ok(object1);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("upload")]
        [Authorize(Roles = "ADMIN,RECRUITER,HR")]
        public async Task<IActionResult> ExcelUploadCandidates([FromForm] DocumentDto documentDto)
        {
            if (documentDto.document == null || documentDto.document.Length == 0)
            {
                return BadRequest("File is empty.");
            }

            try
            {
                await _service.BulkAddCandidate(documentDto.document);
                return Ok("candidates imported successfully!");
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
