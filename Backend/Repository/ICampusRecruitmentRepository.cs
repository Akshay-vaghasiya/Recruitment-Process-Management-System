using Backend.Models;

namespace Backend.Repository
{
    public interface ICampusRecruitmentRepository
    {
        Task<CampusRecruitment> AddCampusRecruitment(CampusRecruitment campusRecruitment);
        Task<CampusRecruitment> UpdateCampusRecruitment(CampusRecruitment campusRecruitment);
        Task<List<CampusRecruitment>> GetCampusRecruitmentsAsync();
        Task<CampusRecruitment?> GetCampusRecruitmentById(int id);
        Task<CampusRecruitment?> GetCampusRecruitmentByName(string name);
        Task DeleteCampusRecruitment(int id);
    }
}
