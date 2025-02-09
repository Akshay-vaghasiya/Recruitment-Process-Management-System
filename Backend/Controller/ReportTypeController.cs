using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportTypeController : ControllerBase
    {
        private readonly IReportTypeService _service;

        public ReportTypeController(IReportTypeService ReportTypeService)
        {
            _service = ReportTypeService;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetReportTypes()
        {
            try
            {
                return Ok(await _service.GetReportTypes());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AddReportType(ReportType ReportType)
        {
            try
            {
                return StatusCode(201, await _service.AddReportType(ReportType));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateDocumetType(int id, ReportType ReportType)
        {
            try
            {
                return StatusCode(200, await _service.UpdateReportType(id, ReportType));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteReportType(int id)
        {
            try
            {
                await _service.DeleteReportType(id);
                return Ok("sucessfully deleted Report type!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
