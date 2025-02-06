using System.Collections.ObjectModel;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RecruitmentProcessManagementSystemContext _context;

        public RoleRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<Role> GetRoleByName(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
        }

        public async Task<Role> addRole(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();

            return role;
        }

        public async Task<Role?> getRolerByName(string name)
        {
            Role role = await _context.Roles.Where(r => r.Name == name).FirstOrDefaultAsync();
            return role;
        }

        public async Task<Role?> getRoleById(int id)
        {
            Role role = await _context.Roles.Where(r => r.PkRoleId == id).Include(r => r.UserRoles).FirstOrDefaultAsync();
            return role;
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task DeleteRole(Role role)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }

    }
}
