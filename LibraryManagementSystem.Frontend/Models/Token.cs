using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System;
using System.Security.Claims;

namespace LibraryManagementSystem.Frontend.Models
{
    public class Token
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string? TokenValue { get; private set; }
        public DateTime Expiration { get; set; }

        public Token() { }

        public void GenerateToken(string secretKey, string issuer, string audience, User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            TokenValue = tokenHandler.WriteToken(token);
            Expiration = token.ValidTo;
        }

    }
}
