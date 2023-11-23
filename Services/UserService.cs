using Domain.Entities;
using Domain.Interfaces;
using Infra.Data;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly CarlletDbContext _dbContext;
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        public UserService(CarlletDbContext dbContext, IAuthService authService, IEmailService emailService)
        {
            _dbContext = dbContext;
            _authService = authService;
            _emailService = emailService;
        }

        public async Task<User> DeleteUser(Guid id)
        {
            var user = _dbContext.Users.Where(u => u.Id == id).FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();

            user.Password = "";

            return user;

        }

        public async Task<List<User>> GetUserList()
        {
            var users = _dbContext.Users.ToList();


            if (users.Count == 0)
            {
                return null;
            }

            return users;
        }

        public async Task<User> GetUser(Guid id)
        {
            var user = _dbContext.Users.Find(id);

            if (user == null)
            {
                return null;
            }

            return user;
        }


        public async Task<User> Register(User user)
        {
            var userExist = _dbContext.Users.FirstOrDefault(u => u.Email == user.Email);

            if (userExist != null)
            {
                return null;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Verified = false;

            var setUser = _dbContext.Users.Add(user);

            if (setUser == null)
            {
                return null;
            }

            _dbContext.SaveChanges();

            user.AccessToken = await _authService.GenerateAccessToken(user);
            user.RefreshToken = await _authService.GenerateRefreshToken(user);
            user.VerificationToken = await _authService.GenerateVerificationToken(user);


            _emailService.SendConfirmationEmail(user);

            user.Password = null;

            return user;
        }

        public async void VerifyEmail(string verificationToken)
        {
            var verifiedUser = _dbContext.Users.SingleOrDefault(vu => vu.VerificationToken == verificationToken);

            if (verifiedUser == null)
            {
                return;
            }

            if(verifiedUser.VerificationTokenExpiration < DateTime.UtcNow)
            {
                return;
            }

            verifiedUser.Verified= true;
            verifiedUser.VerificationToken = null;

            _dbContext.Update(verifiedUser);

            _dbContext.SaveChanges();
        }
        public async Task<User> Login(string email, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);

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
            var userToUpdate = _dbContext.Users.Find(user.Id);
            if (userToUpdate == null)
            {
                return null;
            }

            foreach (var toProp in typeof(User).GetProperties())
            {
                var fromProp = typeof(User).GetProperty(toProp.Name);
                var toValue = fromProp.GetValue(user, null);
                if (toValue != null)
                {
                    toProp.SetValue(userToUpdate, toValue, null);
                }
            }

            // Copy the non-null properties from the incoming entity to the one in the db
            await _dbContext.SaveChangesAsync();

            userToUpdate.Password = "";

            return userToUpdate;
        }

        public void ForgotPassword(string email)
        {
            var userExist = _dbContext.Users.FirstOrDefault(u => u.Email == email);

            if(userExist == null)
            {
                return;
            }

            userExist.ResetPasswordToken = Guid.NewGuid().ToString();
            userExist.ResetPassword = true;
            userExist.ResetPasswordTokenExpiration = DateTime.UtcNow.AddMinutes(15.0);
            _emailService.SendResetPasswordEmail(userExist);

            _dbContext.SaveChanges();
        }

        public async Task<User> ResetPassword(string resetToken, string newPassword,string newPasswordConfirmation )
        {
            var userExist =  _dbContext.Users.FirstOrDefault(u => u.ResetPasswordToken == resetToken);

            if(userExist == null || DateTime.UtcNow > userExist.ResetPasswordTokenExpiration)
            {
                return null;
            }

            if(newPassword != newPasswordConfirmation)
            {
                return null;
            }
            userExist.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            userExist.ResetPassword = false;
            userExist.ResetPasswordToken = "";
            userExist.ResetPasswordTokenExpiration = null;

           _dbContext.Update(userExist);
            _dbContext.SaveChanges();
            return userExist;
        }
    }
}

