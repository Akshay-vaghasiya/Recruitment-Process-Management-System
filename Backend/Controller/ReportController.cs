using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddReport(Report report)
        {
            try
            {
                if (report == null || report.ReportData == null) 
                {
                    return BadRequest("report data should not be null");
                }

                return StatusCode(201, await _service.AddReport(report));
            }
            catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetReports()
        {
            try
            {
                return Ok(await _service.GetReportsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{reportId}")]
        public async Task<IActionResult> UpdateReport(int reportId, [FromBody] Report report)
        {
            try
            {
                return Ok(await _service.UpdateReport(reportId, report));
            }
            catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{reportId}")]
        public async Task<IActionResult> DeleteReport(int reportId)
        {
            try
            {
                await _service.DeleteReport(reportId);
                return Ok("sucessfully report deleted");
            }
            catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
