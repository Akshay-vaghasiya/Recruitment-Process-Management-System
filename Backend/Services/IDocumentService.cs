using Backend.Models;

namespace Backend.Services
{
    public interface IDocumentService
    {
        Task<Document> AddDocument(int candidateId, int documentTypeId, IFormFile formFile);

        Task<List<Document>> GetDocumentsAsync();
        Task<Document> UpdateDocument(int documentId, IFormFile formFile);

        Task<Document> UpdateDocumentStatus(int documentId, int statusId);

        Task DeleteDocument(int id);
    }
}
