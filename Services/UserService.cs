using Domain.Entities;
using Domain.Interfaces;
using Infra.Data;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly CarlletDbContext _dbContext;
        public UserService(CarlletDbContext dbContext)
        {
            _dbContext = dbContext;
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
            var setUser = _dbContext.User.Add(user);
            
            if(setUser == null)
            {
                return null;
            }

            _dbContext.SaveChanges();

            user.Password = null;

            return user;
        }
    }
}
