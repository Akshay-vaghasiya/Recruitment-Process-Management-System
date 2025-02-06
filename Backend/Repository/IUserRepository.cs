using Backend.Models;

namespace Backend.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmail(string email);
        Task<User> AddUser(User user);

        Task<User> UpdateUser(User user);

        Task<User?> GetUserById(int? id);

        Task DeleteUser(User user);

        Task<List<User>> GetUsersAsync();
    }
}
