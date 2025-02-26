using Microsoft.EntityFrameworkCore;
using UserAuth.Database;
using UserAuth.Dto;
using UserAuth.Entities;
using UserAuth.Helpers;

namespace UserAuth.Services
{
    public class UserService
    {
        private readonly DatabaseContext _dbContext;

        public UserService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _dbContext.Users.Include(r => r.Reviews).ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int Id_user)
        {
            return await _dbContext.Users.FindAsync(Id_user);
        }

        public async Task<string> CreateUserAsync(UserDTO userDTO)
        {
            if (string.IsNullOrWhiteSpace(userDTO.Username) ||
                string.IsNullOrWhiteSpace(userDTO.Email) ||
                string.IsNullOrWhiteSpace(userDTO.Password))
            {
                return "Fill all fields!";
            }

            var existingUser = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Username == userDTO.Username || x.Email == userDTO.Email);

            if (existingUser != null)
            {
                return "User already exists";
            }

            var hashedPassword = HelperFunc.PasswordHash(userDTO.Password);

            var user = new User
            {
                Username = userDTO.Username,
                Email = userDTO.Email,
                Password = hashedPassword
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return "User created successfully";
        }

        public async Task<string> UpdateUserAsync(int Id_user, User user)
        {
            var existingUser = await _dbContext.Users.FindAsync(Id_user);
            if (existingUser == null)
            {
                return "User not found";
            }

            if (string.IsNullOrWhiteSpace(user.Username) ||
                string.IsNullOrWhiteSpace(user.Email) ||
                string.IsNullOrWhiteSpace(user.Password))
            {
                return "Fill all fields!";
            }

            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;

            await _dbContext.SaveChangesAsync();

            return "User updated successfully";
        }

        public async Task<string> DeleteUserAsync(int Id_user)
        {
            var existingUser = await _dbContext.Users.FindAsync(Id_user);
            if (existingUser == null)
            {
                return "User not found";
            }

            _dbContext.Users.Remove(existingUser);
            await _dbContext.SaveChangesAsync();

            return "User deleted successfully";
        }
    }
}
