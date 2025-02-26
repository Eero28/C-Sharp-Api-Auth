using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAuth.Database;
using UserAuth.Entities;
using UserAuth.Helpers;
using UserAuth.Dto;

namespace UserAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public UserController(DatabaseContext dbContext) =>
            _dbContext = dbContext;

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            var users = await _dbContext.Users.Include(r => r.Reviews).ToListAsync();
            return Ok(users); 
        }

        [HttpGet("{Id_user}")]
        public async Task<ActionResult> GetUserById(int Id_user)
        {
            var user = await _dbContext.Users.FindAsync(Id_user);

            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserDTO UserDTO)
        {
            if (string.IsNullOrWhiteSpace(UserDTO.Username) ||
                string.IsNullOrWhiteSpace(UserDTO.Email) ||
                string.IsNullOrWhiteSpace(UserDTO.Password))
            {
                return BadRequest("Fill all fields!");
            }

            var existingUser = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Username == UserDTO.Username || x.Email == UserDTO.Email);

            if (existingUser != null)
            {
                return BadRequest("User already exists");
            }

            var hashedPassword = HelperFunc.PasswordHash(UserDTO.Password);

            var user = new User
            {
                Username = UserDTO.Username,
                Email = UserDTO.Email,
                Password = UserDTO.Password
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { Id_user = user.Id_user }, user);
        }


        [HttpPut("{Id_user}")]
        public async Task<ActionResult> UpdateUser(int Id_user, [FromBody] User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username) ||
                string.IsNullOrWhiteSpace(user.Email) ||
                string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("Fill all fields!");
            }

            var existingUser = await _dbContext.Users.FindAsync(Id_user);

            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;

            await _dbContext.SaveChangesAsync();

            return Ok("User updated successfully");
        }

        [HttpDelete("{Id_user}")]
        public async Task<ActionResult> DeleteUser(int Id_user)
        {
            var existingUser = await _dbContext.Users.FindAsync(Id_user);
            if (existingUser is null)
            {
                return NotFound("User not found");
            }

            _dbContext.Users.Remove(existingUser);
            await _dbContext.SaveChangesAsync();

            return Ok("User deleted successfully");
        }
    }
}
