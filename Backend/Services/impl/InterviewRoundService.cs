using Backend.Models;
using Backend.Repository;

namespace Backend.Services.impl
{
    public class InterviewRoundService : IInterviewRoundService
    {
        private readonly IInterviewRoundRepository _repository;

        public InterviewRoundService(IInterviewRoundRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InterviewRound>> GetAllInterviewRoundesAsync()
        {
            return await _repository.GetAllInterviewRoundesAsync();
        }

        public async Task<InterviewRound?> GetInterviewRoundByIdAsync(int id)
        {
            return await _repository.GetInterviewRoundByIdAsync(id);
        }

        public async Task<InterviewRound> AddInterviewRoundAsync(InterviewRound InterviewRound)
        {
            InterviewRound? InterviewRound1 = await _repository.GetInterviewRoundByNameAsync(InterviewRound.Name);
            if (InterviewRound1 != null) throw new Exception("InterviewRound already exist!");

            return await _repository.AddInterviewRoundAsync(InterviewRound);
        }

        public async Task<InterviewRound> UpdateInterviewRoundAsync(int id, InterviewRound InterviewRound)
        {
            InterviewRound? InterviewRound1 = await _repository.GetInterviewRoundByIdAsync(id);
            if (InterviewRound1 == null) throw new Exception("Round with given id is not exist!");

            InterviewRound? InterviewRound2 = await _repository.GetInterviewRoundByNameAsync(InterviewRound.Name);
            if (InterviewRound2 != null) throw new Exception("Round already exist!");

            InterviewRound1.Name = InterviewRound.Name;

            return await _repository.UpdateInterviewRoundAsync(InterviewRound1);
        }

        public async Task DeleteInterviewRoundAsync(int id)
        {
            InterviewRound? InterviewRound = await _repository.GetInterviewRoundByIdAsync(id);
            if (InterviewRound == null) throw new Exception("Interview Round not found by given id");

            if (InterviewRound.Interviews.Count() > 0)
            {
                throw new Exception("This Interview Round is assign to many Interviews.");
            }

            await _repository.DeleteInterviewRoundAsync(id);
        }
    }
}
