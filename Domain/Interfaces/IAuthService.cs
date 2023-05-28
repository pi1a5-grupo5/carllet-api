
namespace Domain.Interfaces
{
    public interface IAuthService
    {
        Task<bool> AuthGrant();
        Task<bool> AuthRevoke();
    }
}
