using LibraryManagementSystem.Backend.Contexts;
using LibraryManagementSystem.Backend.Models;

namespace LibraryManagementSystem.Backend.Services
{
    public class TokenService : ITokenService
    {
        private readonly LibraryContext _context;
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public TokenService(LibraryContext context, IConfiguration configuration)
        {
            this._context = context;
            this._secretKey = configuration["Jwt:Key"]!;
            this._issuer = configuration["Jwt:Issuer"]!;
            this._audience = configuration["Jwt:Audience"]!;
        }

        public async Task<Token> CreateTokenAsync(User user)
        {
            Token token = new Token();
            token.GenerateToken(this._secretKey, this._issuer, this._audience, user);
            token.UserID = user.ID;

            this._context.Tokens.Add(token);
            await this._context.SaveChangesAsync();

            return token;
        }
    }
}
