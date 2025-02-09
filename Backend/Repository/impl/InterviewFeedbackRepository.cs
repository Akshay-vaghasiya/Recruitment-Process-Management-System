using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class InterviewFeedbackRepository : IInterviewFeedbackRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public InterviewFeedbackRepository (RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<InterviewFeedback> AddInterviewFeedback(InterviewFeedback interviewFeedback)
        {
            await _context.InterviewFeedbacks.AddAsync(interviewFeedback);
            await _context.SaveChangesAsync();

            return interviewFeedback;
        }

        public async Task<InterviewFeedback?> GetInterviewFeedbackById(int? id)
        {
            return await _context.InterviewFeedbacks.Include(f => f.FkInterview).FirstOrDefaultAsync(f => f.PkInterviewFeedbackId == id);
        }

        public async Task<List<InterviewFeedback>> GetInterviewFeedbacksAsync()
        {
            return await _context.InterviewFeedbacks.Include(f => f.FkInterview).ToListAsync();
        }

        public async Task<InterviewFeedback> UpdateInterviewFeedback(InterviewFeedback interviewFeedback)
        {
            _context.InterviewFeedbacks.Update(interviewFeedback);
            await _context.SaveChangesAsync();

            return interviewFeedback;
        }

        public async Task DeleteInterviewFeedback(int id)
        {
            InterviewFeedback? interviewFeedback = await _context.InterviewFeedbacks.FindAsync(id);
            if (interviewFeedback != null)
            {
                _context.InterviewFeedbacks.Remove(interviewFeedback);
                await _context.SaveChangesAsync();
            }
        }
    }
}
