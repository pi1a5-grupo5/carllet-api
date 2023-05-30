using Domain.Entities;
using Domain.Interfaces;
using Infra.Data;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

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

            if(userExist != null) 
            {
                return null;
            }

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

        public async Task<User> Update(User user)
        {
            var updateUser = _dbContext.User.Where(u => u.Id == user.Id).FirstOrDefault();

            if (updateUser != null)
            {
                // Atualize apenas os campos não nulos do novo objeto
                if (!string.IsNullOrEmpty(user.Name))
                {
                    updateUser.Name = user.Name;
                }

                if (user.Cnh != null)
                {
                    updateUser.Cnh = user.Cnh;
                }

                if (!string.IsNullOrEmpty(user.Email))
                {
                    updateUser.Email = user.Email;
                }
                    
                if (!string.IsNullOrEmpty(user.Password))
                {
                    updateUser.Password = user.Password;
                }

                if (user.Cellphone != null)
                {
                    updateUser.Cellphone = user.Cellphone;
                }
                    
                if (user.DeviceId != null)
                {
                    updateUser.DeviceId = user.DeviceId;
                }

                if (user.RefreshToken != null)
                {
                    updateUser.RefreshToken = user.RefreshToken;
                }

                if (user.RefreshTokenExpiration != null)
                {
                    updateUser.RefreshTokenExpiration = user.RefreshTokenExpiration;
                }

                if (user.AccessToken != null)
                {
                    updateUser.AccessToken = user.AccessToken;
                }

                if (user.AccessTokenExpiration != null)
                {
                    updateUser.AccessTokenExpiration = user.AccessTokenExpiration;
                }

                // Salve as alterações no banco de dados
                _dbContext.SaveChanges();
                return updateUser;
            }

            return null;


        }
    }
}

