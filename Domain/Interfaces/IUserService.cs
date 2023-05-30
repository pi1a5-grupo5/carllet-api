using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        Task<User> Register(User user);
        Task<User> GetUser(Guid id);
        Task<List<User>> GetUserList();
        Task<User> DeleteUser(Guid id);
        Task<User> Login(string email, string password);
        Task<User> Update(User user);
    }
}
