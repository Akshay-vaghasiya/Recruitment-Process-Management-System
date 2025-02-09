using Backend.Models;
using Backend.Repository;

namespace Backend.Services.impl
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IDocumentTypeRepository _repository;

        public DocumentTypeService(IDocumentTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<DocumentType> AddDocumentType(DocumentType documentType)
        {
            DocumentType? documentType1 = await _repository.GetDocumentTypeByName(documentType.Name);
            if (documentType1 != null) throw new Exception("document type already exists");

            return await _repository.AddDocumentType(documentType);
        } 

        public async Task<List<DocumentType>> GetDocumentTypes()
        {
            return await _repository.GetDocumentTypesAsync();
        }

        public async Task<DocumentType> UpdateDocumentType(int id,DocumentType documentType)
        {
            DocumentType? documentType1 = await _repository.GetDocumentTypeById(id);
            if (documentType1 == null) throw new Exception("document type not exists.");

            DocumentType? documentType2 = await _repository.GetDocumentTypeByName(documentType.Name);
            if (documentType2 != null) throw new Exception("document type already exits.");

            documentType1.Name = documentType.Name;
            return await _repository.UpdateDocumentType(documentType1);
        }

        public async Task DeleteDocumentType(int id)
        {
            DocumentType? documentType1 = await _repository.GetDocumentTypeById(id);
            if (documentType1 == null) throw new Exception("document type not exists.");

            if(documentType1.Documents.Count() > 0)
            {
                throw new Exception("You can't delete assigned document type.");
            }

            await _repository.DeleteDocumentType(documentType1);
        }
    }
}
