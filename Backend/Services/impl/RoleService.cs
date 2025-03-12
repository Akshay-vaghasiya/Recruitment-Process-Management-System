using Backend.Models;
using Backend.Repository;

namespace Backend.Services.impl
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role?> addRole(Role role)
        {
            var role1 = await _roleRepository.GetRoleByName(role.Name);
            if (role1 != null) throw new Exception("role already exist!!");

            return await _roleRepository.addRole(role);
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _roleRepository.GetRolesAsync();
        }

        public async Task DeleteRole(int id)
        {
            var role = await _roleRepository.getRoleById(id);

            if(role?.UserRoles != null && role.UserRoles.Count() > 0)
            {
                throw new Exception("Existing user have this role so you can not delete it");
            } else {
                await _roleRepository.DeleteRole(role);
            }
        }

    }
}
