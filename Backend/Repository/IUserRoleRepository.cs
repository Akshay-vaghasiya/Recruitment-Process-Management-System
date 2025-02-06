using Backend.Models;

namespace Backend.Repository
{
    public interface IUserRoleRepository
    {
        Task<UserRole> AddUserRole(UserRole userRole);
        Task deleteUserRole(UserRole userRole);

        Task<List<UserRole>> GetUserRolesByUser(User user);
    }
}
