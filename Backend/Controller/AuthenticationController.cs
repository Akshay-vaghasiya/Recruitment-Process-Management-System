﻿using Backend.Dtos;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                var result = await _userService.RegisterUser(registerDto);
                return Ok(result);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var object1 = await _userService.AuthenticateUser(loginDto);
                if (object1 == null) return Unauthorized("Invalid credentials");
                return Ok(object1);
            }
            catch (Exception ex) { 
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("updateUser/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> updateUser(int id, [FromBody] RegisterDto registerDto)
        {
            try
            {
                var result = await _userService.UpdateUser(id, registerDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteUser/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> deleteUser(int id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return Ok("sucessfully deleted user");
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAllUsers")]
        [Authorize(Roles = "ADMIN,RECRUITER,HR,REVIEWER,INTERVIEWER")]
        public async Task<IActionResult> getAllUsers()
        {
            try
            {
                var result = await _userService.GetUsersAsync();
                return Ok(result);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
