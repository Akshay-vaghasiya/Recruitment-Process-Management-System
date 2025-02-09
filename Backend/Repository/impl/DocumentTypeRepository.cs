using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public DocumentTypeRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<DocumentType> AddDocumentType(DocumentType documentType)
        {
            await _context.DocumentTypes.AddAsync(documentType);
            await _context.SaveChangesAsync();

            return documentType;
        }

        public async Task<DocumentType?> GetDocumentTypeByName(string? name)
        {
            return await _context.DocumentTypes.FirstOrDefaultAsync(dt => dt.Name == name);
        }

        public async Task<List<DocumentType>> GetDocumentTypesAsync()
        {
            return await _context.DocumentTypes.ToListAsync();
        }

        public async Task<DocumentType?> GetDocumentTypeById(int id)
        {
            return await _context.DocumentTypes.Include(dt => dt.Documents).FirstOrDefaultAsync(td => td.PkDocumentTypeId == id);
        }

        public async Task<DocumentType> UpdateDocumentType(DocumentType documentType)
        {
            _context.DocumentTypes.Update(documentType);
            await _context.SaveChangesAsync();

            return documentType;
        } 

        public async Task DeleteDocumentType(DocumentType documentType)
        {
            _context.DocumentTypes.Remove(documentType);
            await _context.SaveChangesAsync();
        }

    }

}
