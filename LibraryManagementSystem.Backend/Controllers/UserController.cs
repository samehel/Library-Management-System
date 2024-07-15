using LibraryManagementSystem.Backend.Models;
using LibraryManagementSystem.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagementSystem.Backend.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuditService _auditService;

        public UserController(IUserService userService, IAuditService auditService)
        {
            this._userService = userService;
            this._auditService = auditService;
        }

        [HttpGet("{userID}")]
        public async Task<ActionResult<User>> GetUserByID(int userID)
        {
            User? user = await this._userService.GetUserByIDAsync(userID);

            if(user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> GetAllUsers()
        {
            List<User> users = await this._userService.GetAllUsersAsync();

            var invokedUserID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            User? invokedUser = await this._userService.GetUserByIDAsync(int.Parse(invokedUserID!));
            await this._auditService.CreateAuditAsync(new Audit
            {
                UserID = invokedUser!.ID,
                ActionType = Enums.ActionType.GET_ALL_USERS,
                Details = $"Admin {invokedUser.Username} has requested to view the list of exisitng members.",
                isDeleted = false
            });

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
        [Authorize(Roles = "Admin, Member")]
        public async Task<ActionResult<User>> UpdateUser(int userID, User updatedUser)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            Console.WriteLine(userRole);
            if (string.IsNullOrEmpty(userRole) || (!userRole.Equals("Admin") && !userRole.Equals("Member")))
                return Forbid();

            User? user = await this._userService.UpdateUserAsync(userID, updatedUser);

            if(user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpDelete("${userID}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> DeleteUser(int userID)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userRole) || !userRole.Equals("Admin"))
                return Forbid();

            bool isDeleted = await this._userService.DeleteUserAsync(userID);

            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}
