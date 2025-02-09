using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class ReportTypeRepository : IReportTypeRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public ReportTypeRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<ReportType> AddReportType(ReportType ReportType)
        {
            await _context.ReportTypes.AddAsync(ReportType);
            await _context.SaveChangesAsync();

            return ReportType;
        }

        public async Task<ReportType?> GetReportTypeByName(string? name)
        {
            return await _context.ReportTypes.FirstOrDefaultAsync(dt => dt.Name == name);
        }

        public async Task<List<ReportType>> GetReportTypesAsync()
        {
            return await _context.ReportTypes.ToListAsync();
        }

        public async Task<ReportType?> GetReportTypeById(int? id)
        {
            return await _context.ReportTypes.Include(dt => dt.Reports).FirstOrDefaultAsync(td => td.PkReportTypeId == id);
        }

        public async Task<ReportType> UpdateReportType(ReportType ReportType)
        {
            _context.ReportTypes.Update(ReportType);
            await _context.SaveChangesAsync();

            return ReportType;
        }

        public async Task DeleteReportType(ReportType ReportType)
        {
            _context.ReportTypes.Remove(ReportType);
            await _context.SaveChangesAsync();
        }

    }
}
