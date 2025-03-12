using Backend.Models;

namespace Backend.Services
{
    public interface IRoleService
    {
        Task<Role?> addRole(Role role);

        Task<List<Role>> GetRolesAsync();

        Task DeleteRole(int id);
    }
}
