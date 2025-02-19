using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class CollegeRepository : ICollegeRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public CollegeRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<College> AddCollege(College college)
        {
            await _context.Colleges.AddAsync(college);
            await _context.SaveChangesAsync();

            return college;
        }

        public async Task<College> UpdateCollege(College college)
        {
            _context.Colleges.Update(college);
            await _context.SaveChangesAsync();

            return college;
        }

        public async Task<List<College>> GetCollegesAsync()
        {
            return await _context.Colleges.ToListAsync();
        }

        public async Task<College?> GetCollegeById(int id)
        {
            return await _context.Colleges.FirstOrDefaultAsync(c => c.PkCollegeId == id);
        }

        public async Task<College?> GetCollegeByName(string name)
        {
            return await _context.Colleges.FirstOrDefaultAsync(c => c.CollegeName.Equals(name));
        }

        public async Task DeleteCollege(int id)
        {
            College? college = await _context.Colleges.FindAsync(id);
            if(college != null)
            {
                _context.Colleges.Remove(college);
                await _context.SaveChangesAsync();
            }
        }
    }
}
