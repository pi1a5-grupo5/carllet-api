using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        Task<User> Register(User user);
        Task<User> GetUser(int id);
        Task<List<User>> GetUserList();
        Task<User> DeleteUser(int id);
        Task<User> Login(string email, string password);
    }
}
