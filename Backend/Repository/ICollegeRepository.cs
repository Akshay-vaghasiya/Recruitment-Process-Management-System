using Backend.Models;

namespace Backend.Repository
{
    public interface ICollegeRepository
    {
        Task<College> AddCollege(College college);
        Task<College> UpdateCollege(College college);
        Task<List<College>> GetCollegesAsync();
        Task<College?> GetCollegeById(int id);
        Task<College?> GetCollegeByName(string name);
        Task DeleteCollege(int id);
    }
}
