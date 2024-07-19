using LibraryManagementSystem.Frontend.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace LibraryManagementSystem.Frontend.Services
{
    public class TokenService : ServiceBase
    {
        public TokenService() : base() { }

        public async Task<Token> AuthenticateUserAsync(string username, string password)
        {
            var userLogin = new UserLoginDTO
            {
                Username = username,
                Password = password
            };

            var content = new StringContent(JsonConvert.SerializeObject(userLogin), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await this.Client.PostAsync("tokens/authenticate", content);

            if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.InternalServerError)
                return null;

            Token token = await response.Content.ReadAsAsync<Token>();
            return token;
        }


        public User DecodeToken(string tokenValue)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(tokenValue) as JwtSecurityToken;

            if (jsonToken == null)
                return null;

            var userIDClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "nameid");
            var usernameClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "unique_name");
            var emailClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "email");
            var roleClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "role");
            var fullnameClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "Fullname");
            var passwordClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "Password");

            if (userIDClaim == null || usernameClaim == null || emailClaim == null || roleClaim == null || fullnameClaim == null || passwordClaim == null)
                return null;

            return new User
            {
                ID = int.Parse(userIDClaim.Value),
                Username = usernameClaim.Value,
                Email = emailClaim.Value,
                Role = roleClaim.Value,
                Fullname = fullnameClaim.Value,
                Password = passwordClaim.Value,
            };
        }

    }

    public class UserLoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    }
