using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public DocumentRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<List<Document>> GetAllDocumentsAsync()
        {
            return await _context.Documents
                .Include(d => d.FkDocumentType)
                .Include(d => d.FkStatus).ToListAsync();
        }

        public async Task<Document?> GetDocumentByIdAsync(int id)
        {
            return await _context.Documents.Include(d => d.FkStatus)
                .Include(d => d.FkDocumentType)
                .Include(d => d.FkCandidate)
            .FirstOrDefaultAsync(d => d.PkDocumentId == id);
        }

        public async Task<List<Document>> GetDocumentsByCandidateId(int? candidateId)
        {
            return await _context.Documents.Include(d => d.FkStatus)
                .Include(d => d.FkDocumentType)
                .Include(d => d.FkCandidate)
                .Where(d => d.FkCandidateId == candidateId)
                .ToListAsync();
        }

        public async Task<Document?> GetDocumentByCandidateAndType(int candidateId, int documentTypeId)
        {
            return await _context.Documents.FirstOrDefaultAsync(d => d.FkCandidateId == candidateId && d.FkDocumentTypeId == documentTypeId);
        }

        public async Task<Document> AddDocumentAsync(Document Document)
        {
            _context.Documents.Add(Document);
            await _context.SaveChangesAsync();

            return Document;
        }

        public async Task<Document> UpdateDocumentAsync(Document Document)
        {
            _context.Documents.Update(Document);
            await _context.SaveChangesAsync();

            return Document;
        }

        public async Task DeleteDocumentAsync(int id)
        {
            var Document = await _context.Documents.FindAsync(id);
            if (Document != null)
            {
                _context.Documents.Remove(Document);
                await _context.SaveChangesAsync();
            }
        }

    }
}
