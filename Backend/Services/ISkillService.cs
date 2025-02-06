using Backend.Models;

namespace Backend.Services
{
    public interface ISkillService
    {
        Task<List<Skill>> GetAllSkillsAsync();
        Task<Skill?> GetSkillByIdAsync(int id);
        Task<Skill> AddSkillAsync(Skill skill);
        Task<Skill> UpdateSkillAsync(int id,Skill skill);
        Task DeleteSkillAsync(int id);
    }
}
