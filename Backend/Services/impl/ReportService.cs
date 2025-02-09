using Backend.Models;
using Backend.Repository;

namespace Backend.Services.impl
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IReportTypeRepository _reportTypeRepository;

        public ReportService(IReportRepository repository, IUserRepository userRepository, IReportTypeRepository reportTypeRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _reportTypeRepository = reportTypeRepository;
        }

        public async Task<Report> AddReport(Report report)
        {
            User? user = await _userRepository.GetUserById(report.FkGeneratedBy);
            if (user == null) throw new Exception("user not exist with given id");

            ReportType? reportType = await _reportTypeRepository.GetReportTypeById(report.FkReportTypeId);
            if (reportType == null) throw new Exception("report type not exist with given id");

            report.CreatedAt = DateTime.UtcNow;

            return await _repository.AddReport(report);
        }

        public async Task<List<Report>> GetReportsAsync()
        {
            return await _repository.GetReportsAsync();
        }

        public async Task<Report> UpdateReport(int reportId,Report report)
        {
            Report? report1 = await _repository.GetReportById(reportId);
            if (report1 == null) throw new Exception("report is not exist with given id");

            report1.ReportData = report.ReportData ?? report1.ReportData;
            report1.FkReportTypeId = report.FkReportTypeId ?? report1.FkReportTypeId;

            return await _repository.UpdateReport(report1);
        }

        public async Task DeleteReport(int reportId)
        {
            Report? report1 = await _repository.GetReportById(reportId);
            if (report1 == null) throw new Exception("report is not exist with given id");

            await _repository.DeleteReport(reportId);
        }
    }
}
