using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class CampusRecruitmentRepository : ICampusRecruitmentRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public CampusRecruitmentRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<CampusRecruitment> AddCampusRecruitment(CampusRecruitment campusRecruitment)
        {
            await _context.CampusRecruitments.AddAsync(campusRecruitment);
            await _context.SaveChangesAsync();

            return campusRecruitment;
        }

        public async Task<CampusRecruitment> UpdateCampusRecruitment(CampusRecruitment campusRecruitment)
        {
            _context.CampusRecruitments.Update(campusRecruitment);
            await _context.SaveChangesAsync();

            return campusRecruitment;
        }

        public async Task<List<CampusRecruitment>> GetCampusRecruitmentsAsync()
        {
            return await _context.CampusRecruitments
                .Include(cr => cr.FkCollege)
                .ToListAsync();
        }

        public async Task<CampusRecruitment?> GetCampusRecruitmentById(int id)
        {
            return await _context.CampusRecruitments
                .Include(cr => cr.FkCollege)
                .FirstOrDefaultAsync(cr => cr.PkCampusRecruitmentId == id);
        }

        public async Task<CampusRecruitment?> GetCampusRecruitmentByName(string name)
        {
            return await _context.CampusRecruitments
                .Include(cr => cr.FkCollege)
                .FirstOrDefaultAsync(cr => cr.EventName == name);
        }

        public async Task DeleteCampusRecruitment(int id)
        {
            CampusRecruitment? campusRecruitment = await _context.CampusRecruitments.FindAsync(id);

            if (campusRecruitment != null )
            {
                _context.CampusRecruitments.Remove(campusRecruitment);
                await _context.SaveChangesAsync();
            }

        }
    }
}
