﻿using Backend.Dtos;
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

        public async Task<string> AuthenticateUser(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByEmail(loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                return null;
            Console.Write(user.UserRoles.Count());
            return await GenerateJwtToken(user);
        }

        public async Task<UserDto> UpdateUser(int id, RegisterDto registerDto)
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

            Console.Write("Hello");

            if (registerDto.Roles != null && registerDto.Roles.Any())
            {
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
                            FkUser = existingUser,
                            FkRole = role
                        };
                        await _userRoleRepository.AddUserRole(newUserRole);
                    }
                }
            }

            var updatedUser = await _userRepository.UpdateUser(existingUser);

            var userDto = new UserDto
            {
                PkUserId = updatedUser.PkUserId,
                FullName = updatedUser.FullName,
                Email = updatedUser.Email,
                Phone = updatedUser.Phone,
                JoiningDate = updatedUser.JoiningDate
            };

            return userDto;
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

            Console.Write(user.UserRoles.Count);

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
