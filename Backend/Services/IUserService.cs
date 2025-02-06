using Backend.Dtos;
using Backend.Models;

namespace Backend.Services
{
    public interface IUserService
    {
        Task<User> RegisterUser(RegisterDto registerDto);

        Task<string> AuthenticateUser(LoginDto loginDto);

        //Task<User> UpdateUser(int id, RegisterDto registerDto);

        Task DeleteUser(int id);

        Task<List<User>> GetUsersAsync();
    }
}
