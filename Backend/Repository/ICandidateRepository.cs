using Backend.Models;

namespace Backend.Repository
{
    public interface ICandidateRepository
    {
        Task<Candidate> AddCandidate(Candidate candidate);

        Task<List<Candidate>> GetCandidatesAsync();
        Task<Candidate?> GetCandidateByEmail(string? email);

        Task<Candidate?> UpdateCandidate(Candidate candidate);

        Task<Candidate?> GetCandidateById(int? id);
        Task DeleteCandidate(int id);
    }
}
