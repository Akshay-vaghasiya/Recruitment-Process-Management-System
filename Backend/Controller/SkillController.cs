﻿using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _service;

        public SkillController(ISkillService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN,CANDIDATE,RECRUITER,HR,REVIEWER,INTERVIEWER")]
        public async Task<IActionResult> GetAllSkills()
        {
            try
            {
                return Ok(await _service.GetAllSkillsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AddSkill([FromBody] Skill skill)
        {
            try
            {
                if(skill.Name == null || skill.Name.Equals(""))
                {
                    return BadRequest("skill should not be empty!");
                }

                await _service.AddSkillAsync(skill);
                return CreatedAtAction(nameof(GetAllSkills), skill);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateSkill(int id, [FromBody] Skill skill)
        {
            try
            {
                if (skill.Name == null || skill.Name.Equals(""))
                {
                    return BadRequest("skill should not be empty!");
                }
                
                return Ok(await _service.UpdateSkillAsync(id, skill));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            try
            {
                await _service.DeleteSkillAsync(id);
                return Ok("Sucessfully deleted!!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }

}
