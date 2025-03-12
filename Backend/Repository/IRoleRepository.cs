using Backend.Models;

namespace Backend.Repository
{
    public interface IRoleRepository
    {
        Task<Role?> GetRoleByName(string? name);

        Task<Role?> addRole(Role role);
        Task<List<Role>> GetRolesAsync();

        Task DeleteRole(Role? role);

        Task<Role?> getRoleById(int id);
    }
}
