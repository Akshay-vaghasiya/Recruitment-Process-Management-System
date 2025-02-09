
using Backend.Models;

namespace Backend.Services
{
    public interface IDocumentStatusService
    {
        Task<List<DocumentStatus>> GetAllDocumentStatusesAsync();

        Task<DocumentStatus?> GetDocumentStatusByIdAsync(int id);

        Task<DocumentStatus> AddDocumentStatusAsync(DocumentStatus DocumentStatus);

        Task<DocumentStatus> UpdateDocumentStatusAsync(int id, DocumentStatus DocumentStatus);

        Task DeleteDocumentStatusAsync(int id);
    }
}
