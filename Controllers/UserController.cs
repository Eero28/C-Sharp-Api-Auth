using Microsoft.AspNetCore.Mvc;
using UserAuth.Services;
using UserAuth.Dto;
using UserAuth.Entities;

namespace UserAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("{Id_user}")]
        public async Task<ActionResult> GetUserById(int Id_user)
        {
            var user = await _userService.GetUserByIdAsync(Id_user);

            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserDTO userDTO)
        {
            var result = await _userService.CreateUserAsync(userDTO);
            if (result == "User created successfully")
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{Id_user}")]
        public async Task<ActionResult> UpdateUser(int Id_user, [FromBody] User user)
        {
            var result = await _userService.UpdateUserAsync(Id_user, user);
            if (result == "User updated successfully")
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("{Id_user}")]
        public async Task<ActionResult> DeleteUser(int Id_user)
        {
            var result = await _userService.DeleteUserAsync(Id_user);
            if (result == "User deleted successfully")
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
