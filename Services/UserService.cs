using Domain.Entities;
using Domain.Interfaces;
using Infra.Data;
using BCrypt.Net;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly CarlletDbContext _dbContext;
        private IAuthService _authService;
        public UserService(CarlletDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<User> DeleteUser(int id)
        {
            var user = _dbContext.User.Find(id);

            if (user == null)
            {
                return null;
            }

            _dbContext.User.Remove(user);
            _dbContext.SaveChanges();

            user.Password = "";

            return user;
            
        }

        public async Task<List<User>> GetUserList()
        {
            var users = _dbContext.User.ToList();


            if (users.Count == 0)
            {
                return null;
            }

            return users;
        }

        public async Task<User> GetUser(int id)
        {
            var user = _dbContext.User.Find(id);

            if (user == null)
            {
                return null;
            }

            return user;
        }


        public async Task<User> Register(User user)
        { 
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var setUser = _dbContext.User.Add(user);
            


            if(setUser == null)
            {
                return null;
            }

            _dbContext.SaveChanges();

            user.AccessToken = await _authService.GenerateAccessToken(user);
            user.RefreshToken = await _authService.GenerateRefreshToken(user);

            user.Password = null;

            return user;
        }
        public async Task<User> Login(string email, string password)
        {
            var user = _dbContext.User.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return null;
            }

            bool correctPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if(!correctPassword)
            {
                return null;
            }

            user.Password = "";

            return user;
        }
    }
}
