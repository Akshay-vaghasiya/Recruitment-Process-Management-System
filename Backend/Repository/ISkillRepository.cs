using Backend.Models;

namespace Backend.Repository
{
    public interface ISkillRepository
    {
        Task<List<Skill>> GetAllSkillsAsync();

        Task<Skill?> GetSkillByIdAsync(int id);

        Task<Skill> AddSkillAsync(Skill skill);

        Task<Skill?> GetSkillByNameAsync(string name);
        Task<Skill> UpdateSkillAsync(Skill skill);
        Task DeleteSkillAsync(int id);

    }
}
