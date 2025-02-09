using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class DocumentStatusRepository : IDocumentStatusRepository
    {

        private readonly RecruitmentProcessManagementSystemContext _context;

        public DocumentStatusRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<List<DocumentStatus>> GetAllDocumentStatusesAsync()
        {
            return await _context.DocumentStatuses.Include(ds => ds.Documents).ToListAsync();
        }

        public async Task<DocumentStatus?> GetDocumentStatusByIdAsync(int? id)
        {
            return await _context.DocumentStatuses.Include(ds => ds.Documents)
            .FirstOrDefaultAsync(js => js.PkDocumentStatusId == id);
        }

        public async Task<DocumentStatus?> GetDocumentStatusByNameAsync(string? name)
        {
            return await _context.DocumentStatuses.Include(ds => ds.Documents)
            .FirstOrDefaultAsync(js => js.Name == name);
        }

        public async Task<DocumentStatus> AddDocumentStatusAsync(DocumentStatus DocumentStatus)
        {
            _context.DocumentStatuses.Add(DocumentStatus);
            await _context.SaveChangesAsync();

            return DocumentStatus;
        }

        public async Task<DocumentStatus> UpdateDocumentStatusAsync(DocumentStatus DocumentStatus)
        {
            _context.DocumentStatuses.Update(DocumentStatus);
            await _context.SaveChangesAsync();

            return DocumentStatus;
        }

        public async Task DeleteDocumentStatusAsync(int id)
        {
            var DocumentStatus = await _context.DocumentStatuses.FindAsync(id);
            if (DocumentStatus != null)
            {
                _context.DocumentStatuses.Remove(DocumentStatus);
                await _context.SaveChangesAsync();
            }
        }
    }
}
