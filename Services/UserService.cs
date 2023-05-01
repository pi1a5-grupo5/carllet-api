using Domain.Entities;
using Domain.Interfaces;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IDbService _dbService;
        public UserService(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<bool> DeleteUser(int id)
        {
            _ = await _dbService.EditData(
                "DELETE * FROM usuarios WHERE Id = @id", new { id });
            return true;
        }

        public async Task<List<User>> GetUserList()
        {
            var userList = await _dbService.GetAll<User>(
                "SELECT * FROM usuarios", new { });
            return userList;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _dbService.GetAsync<User>(
                "SELECT * FROM usuarios WHERE id = @id", id);
            return user;
        }


        public async Task<bool> Register(User user)
        {
            _ = await _dbService.EditData(
                "INSERT INTO usuarios (name, email, password, deviceId) VALUES (@name, @email, @password, @deviceId)", user);
            return true;
        }
    }
}
