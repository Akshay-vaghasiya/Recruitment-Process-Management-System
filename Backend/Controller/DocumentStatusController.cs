using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class DocumentStatusController : ControllerBase
    {
        private readonly IDocumentStatusService _service;

        public DocumentStatusController(IDocumentStatusService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllDocumentStatuses()
        {
            try
            {
                return Ok(await _service.GetAllDocumentStatusesAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentStatusById(int id)
        {
            try
            {
                var DocumentStatus = await _service.GetDocumentStatusByIdAsync(id);
                if (DocumentStatus == null)
                    return NotFound("Document status not found.");
                return Ok(DocumentStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDocumentStatus([FromBody] DocumentStatus DocumentStatus)
        {
            try
            {
                if (DocumentStatus.Name.Equals("") || DocumentStatus == null)
                {
                    return BadRequest("Documentstatus should not be empty!");
                }
                await _service.AddDocumentStatusAsync(DocumentStatus);
                return CreatedAtAction(nameof(GetDocumentStatusById), new { id = DocumentStatus.PkDocumentStatusId }, DocumentStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocumentStatus(int id, [FromBody] DocumentStatus DocumentStatus)
        {
            try
            {
                if (DocumentStatus.Name.Equals("") || DocumentStatus == null)
                {
                    return BadRequest("Documentstatus should not be empty!");
                }
            
                return Ok(await _service.UpdateDocumentStatusAsync(id, DocumentStatus));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentStatus(int id)
        {
            try
            {
                await _service.DeleteDocumentStatusAsync(id);
                return Ok("Sucessfully deleted!!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
