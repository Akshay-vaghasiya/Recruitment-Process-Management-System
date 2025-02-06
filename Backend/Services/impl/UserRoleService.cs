using Backend.Models;
using Backend.Repository;
using Backend.Repository.impl;

namespace Backend.Services.impl
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }


        public async Task<UserRole> addUserRole(UserRole role)
        {
            return await _userRoleRepository.AddUserRole(role);
        }

        public async Task deleteUserRole(UserRole userRole)
        {
            await _userRoleRepository.deleteUserRole(userRole);
        }
    }
}
