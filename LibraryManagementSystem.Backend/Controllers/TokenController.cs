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

        public TokenController(IUserService userService, ITokenService tokenService)
        {
            this._userService = userService;
            this._tokenService = tokenService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<Token>> Authenticate([FromBody] UserLoginDTO userLogin)
        {
            User? user = await this._userService.AuthenticateAsync(userLogin.Username, userLogin.Password);

            if (user == null)
                return Unauthorized();

            Token token = await this._tokenService.CreateTokenAsync(user);

            return Ok(token);
        }
    }
}
