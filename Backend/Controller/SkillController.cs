using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _service;

        public SkillController(ISkillService service)
        {
            _service = service;
        }

        [HttpGet]
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
        public async Task<IActionResult> AddSkill([FromBody] Skill skill)
        {
            try
            {
                if(skill == null || skill.Name.Equals(""))
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
        public async Task<IActionResult> UpdateSkill(int id, [FromBody] Skill skill)
        {
            try
            {
                if (skill == null || skill.Name.Equals(""))
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
