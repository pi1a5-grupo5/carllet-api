using Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Domain.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateAccessToken(User user);
        Task<string> GenerateRefreshToken(User user);
        Task<string> GenerateVerificationToken(User user);
        bool ValidateToken(string token, out JwtSecurityToken jwt);
    }
}
