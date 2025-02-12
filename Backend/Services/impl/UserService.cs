using Backend.Dtos;
using Backend.Models;
using Backend.Repository;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Text;

namespace Backend.Services.impl
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;     
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public UserService(IUserRepository userRepository, IConfiguration configuration, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<User> RegisterUser(RegisterDto registerDto)
        {
            var existingUser = await _userRepository.GetUserByEmail(registerDto.Email);
            if (existingUser != null) throw new Exception("user already exist!!");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            var user = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                Phone = registerDto.Phone,
                Password = hashedPassword,
                JoiningDate = registerDto.JoiningDate,
                CreatedAt = DateTime.Now
            };

            var user1 = await _userRepository.AddUser(user);

            foreach (string role in registerDto.Roles)
            {
                Role role1 = await _roleRepository.GetRoleByName(role);

                if (role1 == null) {
                    throw new Exception("role not registerd");
                }

                UserRole role2 = new UserRole();

                role2.FkUser = user1;
                role2.FkRole = role1;

                await _userRoleRepository.AddUserRole(role2);
            }

            return user1;
        }

        public async Task<Object> AuthenticateUser(LoginDto loginDto)
        {
            var user1 = await _userRepository.GetUserByEmail(loginDto.Email);
            if (user1 == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user1.Password))
                throw new Exception("Please enter valid credential");
            Console.Write(user1.UserRoles.Count());
            return new { token = await GenerateJwtToken(user1), user = user1 };
        }

        public async Task<User> UpdateUser(int id, RegisterDto registerDto)
        {

            Console.Write(registerDto);
            var existingUser = await _userRepository.GetUserById(id);
            if (existingUser == null)
            {
                throw new Exception("User does not exist!");
            }

            if (!string.IsNullOrEmpty(registerDto.Password))
            {
                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            }

            if (!string.IsNullOrEmpty(registerDto.Email))
            {
                existingUser.Email = registerDto.Email;
            }

            if (!string.IsNullOrEmpty(registerDto.FullName))
            {
                existingUser.FullName = registerDto.FullName;
            }

            if (!string.IsNullOrEmpty(registerDto.Phone))
            {
                existingUser.Phone = registerDto.Phone;
            }

            if (registerDto.JoiningDate != default)
            {
                existingUser.JoiningDate = registerDto.JoiningDate;
            }

    
            if (registerDto.Roles != null && registerDto.Roles.Any())
            {
                foreach (var userRole in existingUser.UserRoles)
                {
                    bool roleExists = registerDto.Roles.Any(r => r.Equals(userRole?.FkRole?.Name));

                    if(!roleExists)
                    {
                        await _userRoleRepository.deleteUserRole(userRole);
                    }
                }

                foreach (var roleName in registerDto.Roles)
                {
                    Role? role = await _roleRepository.GetRoleByName(roleName);
                    if (role == null)
                    {
                        throw new Exception($"Role '{roleName}' does not exist.");
                    }

                    bool roleExists = existingUser.UserRoles.Any(ur => ur.FkRoleId == role.PkRoleId);
                    if (!roleExists)
                    {
                        UserRole newUserRole = new UserRole
                        {
                            FkRoleId = role.PkRoleId,
                            FkUserId = existingUser.PkUserId
                        };
                        await _userRoleRepository.AddUserRole(newUserRole);
                    }
                }

            }

            var updatedUser = await _userRepository.UpdateUser(existingUser);
 
            return updatedUser;
        }

        public async Task DeleteUser(int id)
        {
            User? user = await _userRepository.GetUserById(id);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var userRoles = await _userRoleRepository.GetUserRolesByUser(user);

            foreach (UserRole userRole in userRoles)
            {
                await _userRoleRepository.deleteUserRole(userRole);
            }

            await _userRepository.DeleteUser(user);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _userRepository.GetUsersAsync();
        }


        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, user.Email),
            };

            foreach (var userRole in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.FkRole.Name));    
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
