using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class InterviewPanelRepository : IInterviewPanelRepository
    {

        private readonly RecruitmentProcessManagementSystemContext _context;

        public InterviewPanelRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<InterviewPanel> AddInterviewPanel(InterviewPanel interviewPanel)
        {
            await _context.InterviewPanels.AddAsync(interviewPanel);
            await _context.SaveChangesAsync();

            return interviewPanel;
        }

        public async Task<InterviewPanel?> GetInterviewPanelByUserAndInterview(int userId, int interviewId)
        {
            return await _context.InterviewPanels.FirstOrDefaultAsync(ip => ip.FkInterviewId == interviewId && ip.FkInterviewerId == userId);
        }


        public async Task<List<InterviewPanel>> GetInterviewPanelsAsync()
        {
            return await _context.InterviewPanels
                .Include(ip => ip.FkInterviewer)
                .ToListAsync();
        }

        public async Task<List<InterviewPanel>> GetInterviewPanelByInterview(int interviewId)
        {
            return await _context.InterviewPanels
                .Include(ip => ip.FkInterviewer)
                .Include(ip => ip.FkInterview)
                .ThenInclude(i => i.FkInterviewRound)
                .Where(ip => ip.FkInterviewId == interviewId).ToListAsync();
        }

        public async Task DeleteInterviewPanel(InterviewPanel interviewPanel)
        {
            _context.InterviewPanels.Remove(interviewPanel);
            await _context.SaveChangesAsync();
        }

        public async Task<InterviewPanel?> GetInterviewPanelById(int interviewPanelId)
        {
            return _context.InterviewPanels.FirstOrDefault(ip => ip.PkInterviewPanelId == interviewPanelId);
        }
    }
}
