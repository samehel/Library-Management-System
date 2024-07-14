using LibraryManagementSystem.Backend.Models;

namespace LibraryManagementSystem.Backend.Services
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);
        Task<User> GetUserByIDAsync(int userID);
        Task<List<User>> GetAllUsersAsync();
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(int userID, User updatedUser);
        Task<bool> DeleteUserAsync(int userID);
    }

    public interface ITokenService
    {
        Task<Token> CreateTokenAsync(User user);
    }
}
