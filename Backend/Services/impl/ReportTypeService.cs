using Backend.Models;
using Backend.Repository;

namespace Backend.Services.impl
{
    public class ReportTypeService : IReportTypeService
    {
        private readonly IReportTypeRepository _repository;

        public ReportTypeService(IReportTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ReportType> AddReportType(ReportType ReportType)
        {
            ReportType? ReportType1 = await _repository.GetReportTypeByName(ReportType.Name);
            if (ReportType1 != null) throw new Exception("Report type already exists");

            return await _repository.AddReportType(ReportType);
        }

        public async Task<List<ReportType>> GetReportTypes()
        {
            return await _repository.GetReportTypesAsync();
        }

        public async Task<ReportType> UpdateReportType(int id, ReportType ReportType)
        {
            ReportType? ReportType1 = await _repository.GetReportTypeById(id);
            if (ReportType1 == null) throw new Exception("Report type not exists.");

            ReportType? ReportType2 = await _repository.GetReportTypeByName(ReportType.Name);
            if (ReportType2 != null) throw new Exception("Report type already exits.");

            ReportType1.Name = ReportType.Name;
            return await _repository.UpdateReportType(ReportType1);
        }

        public async Task DeleteReportType(int id)
        {
            ReportType? ReportType1 = await _repository.GetReportTypeById(id);
            if (ReportType1 == null) throw new Exception("Report type not exists.");

            if (ReportType1.Reports.Count() > 0)
            {
                throw new Exception("You can't delete assigned Report type.");
            }

            await _repository.DeleteReportType(ReportType1);
        }
    }
}
