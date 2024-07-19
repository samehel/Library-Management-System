using LibraryManagementSystem.Backend.Models;
using LibraryManagementSystem.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

            if (string.IsNullOrEmpty(invokedUser?.Role) || (!invokedUser.Role.Equals("Admin")))
                return Forbid();

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
            try
            {
                User createdUser = await this._userService.CreateUserAsync(newUser);

                await this._auditService.CreateAuditAsync(new Audit
                {
                    UserID = 0,
                    ActionType = Enums.ActionType.REGISTER,
                    Details = $"New Account created with username \"{createdUser.Username}\" and email \"{createdUser.Email}\"",
                    isDeleted = false

                });

                return CreatedAtAction(nameof(GetUserByID), new { userID = createdUser.ID }, createdUser);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{userID}")]
        [Authorize(Roles = "Admin, Member")]
        public async Task<ActionResult<User>> UpdateUser(int userID, User updatedUser)
        {
            var invokedUserID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            User? invokedUser = await this._userService.GetUserByIDAsync(int.Parse(invokedUserID!));

            if (string.IsNullOrEmpty(invokedUser?.Role) || (!invokedUser.Role.Equals("Admin") && !invokedUser.Role.Equals("Member")))
                return Forbid();

            User? user = await this._userService.UpdateUserAsync(userID, updatedUser);

            await this._auditService.CreateAuditAsync(new Audit
            {
                UserID = invokedUser.ID,
                ActionType = Enums.ActionType.UPDATE_USER,
                Details = $"\"{invokedUser.Username}\" updated user info for user with ID \"{updatedUser.ID}\"",
                isDeleted = false
            });

            if(user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpDelete("${userID}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> DeleteUser(int userID)
        {
            var invokedUserID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            User? invokedUser = await this._userService.GetUserByIDAsync(int.Parse(invokedUserID!));

            if (string.IsNullOrEmpty(invokedUser?.Role) || !invokedUser.Role.Equals("Admin"))
                return Forbid();

            bool isDeleted = await this._userService.DeleteUserAsync(userID);

            await this._auditService.CreateAuditAsync(new Audit
            {
                UserID = invokedUser.ID,
                ActionType = Enums.ActionType.DELETE_USER,
                Details = $"Admin \"{invokedUser.Username}\" deleted user with ID \"{userID}\"",
                isDeleted = false
            });

            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}
