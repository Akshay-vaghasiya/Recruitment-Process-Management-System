using Backend.Dtos;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _service;

        public DocumentController(IDocumentService documentService)
        {
            _service = documentService;
        }

        [HttpPost("{candidateId}/{documentTypeId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AddDocument(int candidateId, int documentTypeId, [FromForm] DocumentDto documentDto)
        {
            try
            {
                if (documentDto.document == null || documentDto.document.Length == 0)
                {
                    return BadRequest("please upload valid document");
                }

                var result = await _service.AddDocument(candidateId, documentTypeId, documentDto.document);
                return StatusCode(201, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{documentId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateDocument(int documentId, [FromForm] DocumentDto documentDto)
        {
            try
            {
                if (documentDto.document == null || documentDto.document.Length == 0)
                {
                    return BadRequest("please upload valid document");
                }

                var result = await _service.UpdateDocument(documentId, documentDto.document);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{documentId}/{documentStatusId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateDocumentStatus(int documentId, int documentStatusId)
        {
            try
            {
                var result = await _service.UpdateDocumentStatus(documentId, documentStatusId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetDocuments()
        {
            try
            {
                return Ok(await _service.GetDocumentsAsync());
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{documentId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteDocument(int documentId)
        {
            try
            {
                await _service.DeleteDocument(documentId);
                return Ok("document deleted sucessfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
