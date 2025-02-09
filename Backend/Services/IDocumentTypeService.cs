using Backend.Models;

namespace Backend.Services
{
    public interface IDocumentTypeService
    {
        Task<DocumentType> AddDocumentType(DocumentType documentType);

        Task<List<DocumentType>> GetDocumentTypes();

        Task<DocumentType> UpdateDocumentType(int id, DocumentType documentType);

        Task DeleteDocumentType(int id);
    }
}
