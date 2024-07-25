using LibraryManagementSystem.Backend.DTOs;
using LibraryManagementSystem.Backend.Models;
using LibraryManagementSystem.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Backend.Controllers
{
    [ApiController]
    [Route("api/tokens")]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IAuditService _auditService;

        public TokenController(IUserService userService, ITokenService tokenService, IAuditService auditService)
        {
            this._userService = userService;
            this._tokenService = tokenService;
            this._auditService = auditService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<Token>> Authenticate([FromBody] UserLoginDTO userLogin)
        {
            User? user = await this._userService.AuthenticateAsync(userLogin.Username!, userLogin.Password!);

            if (user == null)
                return Unauthorized();

            await this._auditService.CreateAuditAsync(new Audit
            {
                UserID = 0,
                ActionType = Enums.ActionType.LOGIN.ToString(),
                Details = $"User \"{user?.Username}\" was logged into the system",
                isDeleted = false

            });

            Token token = await this._tokenService.CreateTokenAsync(user!);

            return Ok(token);
        }
    }
}
