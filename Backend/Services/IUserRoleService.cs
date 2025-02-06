using Backend.Models;

namespace Backend.Services
{
    public interface IUserRoleService
    {
        Task<UserRole> addUserRole(UserRole role);
        Task deleteUserRole(UserRole userRole);
    }
}
