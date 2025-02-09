using Backend.Models;

namespace Backend.Services
{
    public interface IReportService
    {
        Task<Report> AddReport(Report report);
        Task<List<Report>> GetReportsAsync();
        Task<Report> UpdateReport(int reportId, Report report);
        Task DeleteReport(int reportId);
    }
}
