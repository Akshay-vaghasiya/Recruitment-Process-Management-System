using Backend.Models;
using Backend.Repository;
using Backend.Repository.impl;

namespace Backend.Services.impl
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _repository;

        public SkillService(ISkillRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Skill>> GetAllSkillsAsync()
        {
            return await _repository.GetAllSkillsAsync();
        }

        public async Task<Skill?> GetSkillByIdAsync(int id)
        {
            return await _repository.GetSkillByIdAsync(id);
        }

        public async Task<Skill> AddSkillAsync(Skill skill)
        {
            Skill? skill1 = await _repository.GetSkillByNameAsync(skill.Name);
            if (skill1 != null) throw new Exception("skill already exist!!");

            return await _repository.AddSkillAsync(skill);
        }

        public async Task<Skill> UpdateSkillAsync(int id,Skill skill)
        {
            Skill? skill1 = await _repository.GetSkillByIdAsync(id);
            if (skill1 == null) throw new Exception("skill with given id is not exist!");

            Skill? skill2 = await _repository.GetSkillByNameAsync(skill.Name);
            if (skill2 != null) throw new Exception("skill already exist!");

            skill1.Name = skill.Name;

            return await _repository.UpdateSkillAsync(skill1);
        }

        public async Task DeleteSkillAsync(int id)
        {
            Skill? skill1 = await _repository.GetSkillByIdAsync(id);
            if (skill1 == null) throw new Exception("skill with given id is not exist!");

            await _repository.DeleteSkillAsync(id);
        }
    }

}
