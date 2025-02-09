using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class ReportRepository : IReportRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public ReportRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<Report> AddReport(Report report)
        {
            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();

            return report;
        }

        public async Task<Report?> GetReportById(int reportId)
        {
            return await _context.Reports.Include(r => r.FkReportType).FirstOrDefaultAsync();
        }

        public async Task<List<Report>> GetReportsAsync()
        {
            return await _context.Reports.Include(r => r.FkReportType).ToListAsync();
        }

        public async Task<Report> UpdateReport(Report report)
        {
            _context.Reports.Update(report);
            await _context.SaveChangesAsync();

            return report;
        }

        public async Task DeleteReport(int reportId)
        {
            Report? report = await _context.Reports.FindAsync(reportId);
            if (report != null)
            {
                _context.Reports.Remove(report);
                await _context.SaveChangesAsync();
            }
        }
    }
}
