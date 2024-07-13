using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace LibraryManagementSystem.Backend.Models
{
    public class Token
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string TokenValue { get; private set; }

        public Token(string secretKey, string issuer, string audience)  
        { 
            this.TokenValue = GenerateJwtToken(secretKey, issuer, audience);
        }

        private string GenerateJwtToken(string secretKey, string issuer, string audience)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            byte[] key = Encoding.UTF8.GetBytes(secretKey);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, this.UserID.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
