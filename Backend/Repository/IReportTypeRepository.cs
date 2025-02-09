using Backend.Models;

namespace Backend.Repository
{
    public interface IReportTypeRepository
    {
        Task<ReportType> AddReportType(ReportType ReportType);
        Task<ReportType?> GetReportTypeByName(string? name);
        Task<List<ReportType>> GetReportTypesAsync();
        Task<ReportType?> GetReportTypeById(int? id);
        Task<ReportType> UpdateReportType(ReportType ReportType);
        Task DeleteReportType(ReportType ReportType);
    }
}
