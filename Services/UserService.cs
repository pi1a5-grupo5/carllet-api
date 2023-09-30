using Domain.Entities;
using Domain.Interfaces;
using Infra.Data;

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

        public async Task<User> DeleteUser(Guid id)
        {
            var user = _dbContext.User.Where(u => u.Id == id).FirstOrDefault();

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

        public async Task<User> GetUser(Guid id)
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
            var userExist = _dbContext.User.FirstOrDefault(u => u.Email == user.Email);

            if (userExist != null)
            {
                return null;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var setUser = _dbContext.User.Add(user);

            if (setUser == null)
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

            if (!correctPassword)
            {
                return null;
            }

            user.Password = "";

            return user;
        }

        public async Task<User> Update(User user)
        {
            // Create an instance of the entity to be updated
            var userToUpdate = _dbContext.User.Find(user.Id);
            if (userToUpdate == null)
            {
                return null;
            }

            // Copy the non-null properties from the incoming entity to the one in the db
            _dbContext.Entry(userToUpdate).CurrentValues.SetValues(user);
            await _dbContext.SaveChangesAsync();

            userToUpdate.Password = "";

            return userToUpdate;
        }
    }
}

