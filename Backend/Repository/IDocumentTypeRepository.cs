using Backend.Models;

namespace Backend.Repository
{
    public interface IDocumentTypeRepository
    {
        Task<DocumentType> AddDocumentType(DocumentType documentType);

        Task<List<DocumentType>> GetDocumentTypesAsync();

        Task<DocumentType?> GetDocumentTypeById(int id);

        Task<DocumentType> UpdateDocumentType(DocumentType documentType);

        Task DeleteDocumentType(DocumentType documentType);

        Task<DocumentType?> GetDocumentTypeByName(string? name);
    }
}
