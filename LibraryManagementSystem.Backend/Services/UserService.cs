using LibraryManagementSystem.Backend.Contexts;
using LibraryManagementSystem.Backend.Models;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Backend.Utils;

namespace LibraryManagementSystem.Backend.Services
{
    public class UserService : IUserService
    {

        private readonly LibraryContext _context;

        public UserService(LibraryContext context) 
        { 
            this._context = context;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await this._context.Users.SingleOrDefaultAsync(user => user.Username == username);

            if (user == null)
                return null;

            if(EncryptionUtil.isEncrypted(user.Password))
            {
                if (EncryptionUtil.Decrypt(user.Password) == password)
                {
                    user.Password = EncryptionUtil.Decrypt(user.Password);
                    return user;
                }
            }

            if (user.Password == password)
                return user;
            
            return null;
        }

        public async Task<User?> GetUserByIDAsync(int userID)
        {
            return await this._context.Users.FindAsync(userID);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await this._context.Users.ToListAsync();
        }

        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            return await this._context.Users.AnyAsync(user => user.Username == username);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            if (await IsUsernameTakenAsync(user.Username!))
                throw new Exception("Username is already taken");

            this._context.Users.Add(user);
            await this._context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateUserAsync(int userID, User updatedUser)
        {
            User? user = await this._context.Users.FindAsync(userID);

            if (user != null)
            {
                if(!string.IsNullOrEmpty(updatedUser.Fullname))
                    user.Fullname = updatedUser.Fullname;

                if(!string.IsNullOrEmpty(updatedUser.Email))
                    user.Email = updatedUser.Email;

                if (!string.IsNullOrEmpty(updatedUser.Role))
                    user.Role = updatedUser.Role;

                if (!string.IsNullOrEmpty(updatedUser.Username))
                    user.Username = updatedUser.Username;

                if (!string.IsNullOrEmpty(updatedUser.Password))
                    user.Password = EncryptionUtil.Encrypt(updatedUser.Password); 

                await this._context.SaveChangesAsync();
            }

            return user;
        }

        public async Task<bool> DeleteUserAsync(int userID)
        {
            User? user = await this._context.Users.FindAsync(userID);
            
            if (user != null)
            {
                this._context.Users.Remove(user);
                await this._context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
