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

            string uploadsFolder = Directory.GetCurrentDirectory().Replace("Backend", "Frontend") + "\\public\\Document\\" + candidate?.Email;

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string fileName = $"{formFile.FileName}";

            string fileExtention = Path.GetExtension(fileName);

            if (fileExtention != ".pdf")
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
            document1.DocumentUrl = "../../public/Document/" + candidate?.Email + "/" + fileName;

            return await _repository.AddDocumentAsync(document1);

        }

        public async Task<List<Document>> GetDocumentsAsync()
        {
            return await _repository.GetAllDocumentsAsync();
        }

        public async Task<List<Document>> GetDocumentByCandidate(int candidateId)
        {
            Candidate? candidate = await _candidateRepository.GetCandidateById(candidateId);
            if (candidate == null) throw new Exception("candidate not exist with given id");

            return await _repository.GetDocumentsByCandidateId(candidateId);

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
                string uploadsFolder = Directory.GetCurrentDirectory().Replace("Backend", "Frontend") + "\\public\\Document\\" + document?.FkCandidate?.Email;

                if (document?.DocumentUrl != null && !document.DocumentUrl.Equals(""))
                {
                    File.Delete(uploadsFolder + "\\" + document?.DocumentUrl?.Substring(document.DocumentUrl.LastIndexOf("/") + 1));
                }

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string fileName = $"{formFile.FileName}";

                string fileExtention = Path.GetExtension(fileName);

                if (fileExtention != ".pdf")
                {
                    throw new Exception("file extention is not valid.");
                }

                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                document.DocumentUrl = "../../public/Document/" + document?.FkCandidate?.Email + "/" + fileName;
            }

            DocumentStatus? documentStatus = await _documentStatusRepository.GetDocumentStatusByNameAsync("PENDING");
            document.FkStatus = documentStatus;
            return await _repository.UpdateDocumentAsync(document);
        }

        public async Task DeleteDocument(int id)
        {
            Document? document = await _repository.GetDocumentByIdAsync(id);
            if (document == null) throw new Exception("document not exist in system");

            List<Document> documents = await _repository.GetDocumentsByCandidateId(document.FkCandidateId);
            if (documents.Count == 1)
            {
                Directory.Delete(Directory.GetCurrentDirectory().Replace("Backend", "Frontend") + "\\public\\Document\\" + document?.FkCandidate?.Email, true);
            }
            else
            {
                string uploadsFolder = Directory.GetCurrentDirectory().Replace("Backend", "Frontend") + "\\public\\Document\\" + document?.FkCandidate?.Email;
                File.Delete(uploadsFolder + "\\" + document?.DocumentUrl?.Substring(document.DocumentUrl.LastIndexOf("/") + 1));
            }
            await _repository.DeleteDocumentAsync(id);
        }
    }
}
