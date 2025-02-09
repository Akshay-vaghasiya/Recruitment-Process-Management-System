using Backend.Models;
using Backend.Repository;

namespace Backend.Services.impl
{
    public class DocumentStatusService : IDocumentStatusService
    {
        private readonly IDocumentStatusRepository _repository;

        public DocumentStatusService(IDocumentStatusRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DocumentStatus>> GetAllDocumentStatusesAsync()
        {
            return await _repository.GetAllDocumentStatusesAsync();
        }

        public async Task<DocumentStatus?> GetDocumentStatusByIdAsync(int id)
        {
            return await _repository.GetDocumentStatusByIdAsync(id);
        }

        public async Task<DocumentStatus> AddDocumentStatusAsync(DocumentStatus DocumentStatus)
        {
            DocumentStatus? DocumentStatus1 = await _repository.GetDocumentStatusByNameAsync(DocumentStatus.Name);
            if (DocumentStatus1 != null) throw new Exception("Documentstatus already exist!");

            return await _repository.AddDocumentStatusAsync(DocumentStatus);
        }

        public async Task<DocumentStatus> UpdateDocumentStatusAsync(int id, DocumentStatus DocumentStatus)
        {
            DocumentStatus? DocumentStatus1 = await _repository.GetDocumentStatusByIdAsync(id);
            if (DocumentStatus1 == null) throw new Exception("status with given id is not exist!");

            DocumentStatus? DocumentStatus2 = await _repository.GetDocumentStatusByNameAsync(DocumentStatus.Name);
            if (DocumentStatus2 != null) throw new Exception("status already exist!");

            DocumentStatus1.Name = DocumentStatus.Name;

            return await _repository.UpdateDocumentStatusAsync(DocumentStatus1);
        }

        public async Task DeleteDocumentStatusAsync(int id)
        {
            DocumentStatus? DocumentStatus = await _repository.GetDocumentStatusByIdAsync(id);
            if (DocumentStatus == null) throw new Exception("Document status not found by given id");

            if (DocumentStatus.Documents.Count() > 0)
            {
                throw new Exception("This Document status is assign to many Documents.");
            }

            await _repository.DeleteDocumentStatusAsync(id);
        }
    }
}
