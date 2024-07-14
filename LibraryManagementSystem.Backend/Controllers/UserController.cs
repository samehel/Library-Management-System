using LibraryManagementSystem.Backend.Models;
using LibraryManagementSystem.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Backend.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet("{userID}")]
        public async Task<ActionResult<User>> GetUserByID(int userID)
        {
            User user = await this._userService.GetUserByIDAsync(userID);

            if(user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetAllUsers()
        {
            List<User> users = await this._userService.GetAllUsersAsync();

            if(users.Count == 0) 
                return NotFound();

            return Ok(users);  
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User newUser)
        {
            User createdUser = await this._userService.CreateUserAsync(newUser);

            return CreatedAtAction(nameof(GetUserByID), new { userID = createdUser.ID }, createdUser);
        }

        [HttpPut("{userID}")]
        public async Task<ActionResult<User>> UpdateUser(int userID, User updatedUser)
        {
            User user = await this._userService.UpdateUserAsync(userID, updatedUser);

            if(user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpDelete("${userID}")]
        public async Task<ActionResult<User>> DeleteUser(int userID)
        {
            bool isDeleted = await this._userService.DeleteUserAsync(userID);

            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}
