using Backend.Dtos;
using Backend.Models;

namespace Backend.Services
{
    public interface ICandidateService
    {
        Task<Candidate> AddCandidate(CandidateDto candidateDto);

        Task<List<Candidate>> GetCandidatesAsync();

        Task<Candidate?> UpdateCandidate(int id, CandidateDto candidateDto);

        Task<CandidateSkill> AddCandidateSkill(int candidateId, int skillId, int yearOfExp);

        Task DeleteCandidateSkill(int candidateSkillId);
        Task DeleteCandidate(int candidateId);
        Task<Object> AuthenticateCandidate(LoginDto loginDto);
        Task BulkAddCandidate(IFormFile file);
    }
}
