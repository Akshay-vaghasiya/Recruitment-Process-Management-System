using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Http;

namespace Backend.Services.impl
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _repository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IDocumentStatusRepository _documentStatusRepository;

        public DocumentService(IDocumentRepository repository, ICandidateRepository candidateRepository, IDocumentTypeRepository documentTypeRepository, IDocumentStatusRepository documentStatusRepository) { 
            _repository = repository;
            _candidateRepository = candidateRepository;
            _documentTypeRepository = documentTypeRepository;
            _documentStatusRepository = documentStatusRepository;
        }

        public async Task<Document> AddDocument(int candidateId, int documentTypeId, IFormFile formFile)
        {
            Candidate? candidate = await _candidateRepository.GetCandidateById(candidateId);
            if (candidate == null) throw new Exception("candidate not exist with given id");

            DocumentType? documentType = await _documentTypeRepository.GetDocumentTypeById(documentTypeId);
            if (documentType == null) throw new Exception("document type not exist with given id");

            Document? document = await _repository.GetDocumentByCandidateAndType(candidateId, documentTypeId);
            if (document != null) throw new Exception("document already exist please update it");

            DocumentStatus? documentStatus = await _documentStatusRepository.GetDocumentStatusByNameAsync("PENDING");

            string uploadsFolder = Path.Combine("C:", "Document", candidate.Email);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string fileName = $"{formFile.FileName}";

            string fileExtention = Path.GetExtension(fileName);

            if (fileExtention != ".pdf" && fileExtention != ".docx")
            {
                throw new Exception("file extention is not valid.");
            }

            string filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            Document document1 = new Document();

            document1.FkStatus = documentStatus;
            document1.FkCandidateId = candidateId;
            document1.FkDocumentTypeId = documentTypeId;
            document1.DocumentUrl = filePath;

            return await _repository.AddDocumentAsync(document1);

        }

        public async Task<List<Document>> GetDocumentsAsync()
        {
            return await _repository.GetAllDocumentsAsync();
        }

        public async Task<Document> UpdateDocumentStatus(int documentId, int statusId)
        {
            DocumentStatus? documentStatus = await _documentStatusRepository.GetDocumentStatusByIdAsync(statusId);
            if (documentStatus == null) throw new Exception("document status not exist in system");

            Document? document = await _repository.GetDocumentByIdAsync(documentId);
            if (document == null) throw new Exception("document not exist in system");

            document.FkStatus = documentStatus;

            return await _repository.UpdateDocumentAsync(document);
        }

        public async Task<Document> UpdateDocument(int documentId, IFormFile formFile)
        {
            Document? document = await _repository.GetDocumentByIdAsync(documentId);
            if (document == null) throw new Exception("document not exist in system");


            if (formFile != null)
            {
                if (document.DocumentUrl != null || document.DocumentUrl.Equals(""))
                {
                    File.Delete(document.DocumentUrl);
                }

                string uploadsFolder = Path.Combine("C:", "Document", document.FkCandidate.Email);

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string fileName = $"{formFile.FileName}";

                string fileExtention = Path.GetExtension(fileName);

                if (fileExtention != ".pdf" && fileExtention != ".docx")
                {
                    throw new Exception("file extention is not valid.");
                }

                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

            }

            DocumentStatus? documentStatus = await _documentStatusRepository.GetDocumentStatusByNameAsync("PENDING");
            document.FkStatus = documentStatus;
            return await _repository.UpdateDocumentAsync(document);
        }

        public async Task DeleteDocument(int id)
        {
            Document? document = await _repository.GetDocumentByIdAsync(id);
            if (document == null) throw new Exception("document not exist in system");

            await _repository.DeleteDocumentAsync(id);
        }
    }
}
