using Backend.Models;

namespace Backend.Repository
{
    public interface IDocumentRepository
    {
        Task<List<Document>> GetAllDocumentsAsync();

        Task<Document?> GetDocumentByIdAsync(int id);

        Task<Document> AddDocumentAsync(Document Document);

        Task<Document> UpdateDocumentAsync(Document Document);

        Task DeleteDocumentAsync(int id);

        Task<Document?> GetDocumentByCandidateAndType(int candidateId, int documentTypeId);
        Task<List<Document>> GetDocumentsByCandidateId(int? candidateId);
    }
}
