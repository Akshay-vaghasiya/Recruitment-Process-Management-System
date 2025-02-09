using Backend.Models;

namespace Backend.Repository
{
    public interface IDocumentStatusRepository
    {
        Task<List<DocumentStatus>> GetAllDocumentStatusesAsync();

        Task<DocumentStatus?> GetDocumentStatusByIdAsync(int? id);

        Task<DocumentStatus?> GetDocumentStatusByNameAsync(string? name);

        Task<DocumentStatus> AddDocumentStatusAsync(DocumentStatus DocumentStatus);

        Task<DocumentStatus> UpdateDocumentStatusAsync(DocumentStatus DocumentStatus);

        Task DeleteDocumentStatusAsync(int id);
    }
}
