using System.Linq;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public UserRoleRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<UserRole> AddUserRole(UserRole userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();

            return userRole;
        }

        public async Task deleteUserRole(UserRole userRole)
        {
            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserRole>> GetUserRolesByUser(User user)
        {
            var userRoles = await _context.UserRoles.Where(ur => ur.FkUserId == user.PkUserId).ToListAsync();
            return userRoles;
        }

    }
}
