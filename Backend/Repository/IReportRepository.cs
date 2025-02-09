using Backend.Models;

namespace Backend.Repository
{
    public interface IReportRepository
    {
        Task<Report> AddReport(Report report);
        Task<Report?> GetReportById(int reportId);
        Task<List<Report>> GetReportsAsync();
        Task<Report> UpdateReport(Report report);
        Task DeleteReport(int reportId);
    }
}
