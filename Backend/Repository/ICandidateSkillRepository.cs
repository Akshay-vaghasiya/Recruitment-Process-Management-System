using Backend.Models;

namespace Backend.Repository
{
    public interface ICandidateSkillRepository
    {
        Task<CandidateSkill> AddCandidateSkill(CandidateSkill candidateSkill);

        Task DeleteCandidateSkill(CandidateSkill candidateSkill);

        Task<CandidateSkill?> GetCandidateSkillByCandidateAndSkill(int candidateId, int skillId);

        Task<CandidateSkill> UpdateCandidateSkill(CandidateSkill candidateSkill);

        Task<CandidateSkill?> GetCandidateSkillAsyncById(int candidateSkillId);
    }
}
