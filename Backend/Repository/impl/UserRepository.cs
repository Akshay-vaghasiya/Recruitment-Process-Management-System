using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.impl
{
    public class UserRepository : IUserRepository
    {
        private readonly RecruitmentProcessManagementSystemContext  _context;

        public UserRepository(RecruitmentProcessManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.FkRole).FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserById(int? id)
        {
            return await _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.FkRole).FirstOrDefaultAsync(u => u.PkUserId == id);
        }

        public async Task<User> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(); 
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var users = await _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.FkRole).ToListAsync();
            return users;
        }
    }
}
