using Backend.Models;

namespace Backend.Services
{
    public interface IReportTypeService
    {
        Task<ReportType> AddReportType(ReportType ReportType);
        Task<List<ReportType>> GetReportTypes();
        Task<ReportType> UpdateReportType(int id, ReportType ReportType);
        Task DeleteReportType(int id);

    }
}
