
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateAccessToken(User user);
        Task<string> GenerateRefreshToken(User user);
        Task<string> GenerateVerificationToken(User user);
        Guid ValidateToken(string token);
    }
}
