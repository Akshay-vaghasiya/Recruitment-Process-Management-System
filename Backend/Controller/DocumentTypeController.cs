using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
    {
        private readonly IDocumentTypeService _service;

        public DocumentTypeController(IDocumentTypeService documentTypeService)
        {
            _service = documentTypeService;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetDocumentTypes()
        {
            try
            {
                return Ok(await _service.GetDocumentTypes());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AddDocumentType(DocumentType documentType)
        {
            try
            {
                return StatusCode(201, await _service.AddDocumentType(documentType));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateDocumetType(int id,  DocumentType documentType)
        {
            try
            {
                return StatusCode(200, await _service.UpdateDocumentType(id, documentType));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteDocumentType(int id)
        {
            try
            {
                await _service.DeleteDocumentType(id);
                return Ok("sucessfully deleted document type!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
