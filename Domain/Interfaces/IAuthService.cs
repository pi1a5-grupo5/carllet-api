
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateAccessToken(User user);
        Task<string> GenerateRefreshToken(User user);
        Task<int?> ValidateToken(string token);
    }
}
