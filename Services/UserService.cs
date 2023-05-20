using AutoMapper;
using Common;
using Domain.Authorization;
using Domain.Entities;
using BCrypt.Net; 
using Domain.Interfaces;
using Domain.Models.Users;
using Infra.Data;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserService(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == model.Email);

            // validate
            if (user == null || !user.IsVerified || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                throw new AppException("Email or password is incorrect");

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = _jwtUtils.GenerateJwtToken(user);
            var refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            user.RefreshTokens.Add(refreshToken);

            // remove old refresh tokens from user
            removeOldRefreshTokens(user);

            // save changes to db
            _context.Update(user);
            _context.SaveChanges();

            var response = _mapper.Map<AuthenticateResponse>(user);
            response.JwtToken = jwtToken;
            response.RefreshToken = refreshToken.Token;
            return response;
        }

        public AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            var user = getUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (refreshToken.IsRevoked)
            {
                // revoke all descendant tokens in case this token has been compromised
                revokeDescendantRefreshTokens(refreshToken, user, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
                _context.Update(user);
                _context.SaveChanges();
            }

            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");

            // replace old refresh token with a new one (rotate token)
            var newRefreshToken = rotateRefreshToken(refreshToken, ipAddress);
            user.RefreshTokens.Add(newRefreshToken);


            // remove old refresh tokens from user
            removeOldRefreshTokens(user);

            // save changes to db
            _context.Update(user);
            _context.SaveChanges();

            // generate new jwt
            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            // return data in authenticate response object
            var response = _mapper.Map<AuthenticateResponse>(user);
            response.JwtToken = jwtToken;
            response.RefreshToken = newRefreshToken.Token;
            return response;
        }

        public void RevokeToken(string token, string ipAddress)
        {
            var user = getUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");

            // revoke token and save
            revokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
            _context.Update(user);
            _context.SaveChanges();
        }

        public void Register(RegisterRequest model, string origin)
        {
            // validate
            if (_context.Users.Any(x => x.Email == model.Email))
            {
                // send already registered error in email to prevent user enumeration
                //sendAlreadyRegisteredEmail(model.Email, origin);
                return;
            }

            // map model to new user object
            var user = _mapper.Map<User>(model);

            // first registered user is an admin
            var isFirstAccount = _context.Users.Count() == 0;
            user.Role = isFirstAccount ? Role.Admin : Role.User;
            user.Created = DateTime.UtcNow;
            user.VerificationToken = generateVerificationToken();

            // hash password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // save user
            _context.Users.Add(user);
            _context.SaveChanges();

            // send email
            //sendVerificationEmail(user, origin);
        }

        public void VerifyEmail(string token)
        {
            var user = _context.Users.SingleOrDefault(x => x.VerificationToken == token);

            if (user == null)
                throw new AppException("Verification failed");

            user.Verified = DateTime.UtcNow;
            user.VerificationToken = null;

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == model.Email);

            // always return ok response to prevent email enumeration
            if (user == null) return;

            // create reset token that expires after 1 day
            user.ResetToken = generateResetToken();
            user.ResetTokenExpires = DateTime.UtcNow.AddDays(1);

            _context.Users.Update(user);
            _context.SaveChanges();

            // send email
            //sendPasswordResetEmail(user, origin);
        }

        public void ValidateResetToken(ValidateResetTokenRequest model)
        {
            getUserByResetToken(model.Token);
        }

        public void ResetPassword(ResetPasswordRequest model)
        {
            var user = getUserByResetToken(model.Token);

            // update password and remove reset token
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            user.PasswordReset = DateTime.UtcNow;
            user.ResetToken = null;
            user.ResetTokenExpires = null;

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public IEnumerable<UserResponse> GetAll()
        {
            var users = _context.Users;
            return _mapper.Map<IList<UserResponse>>(users);
        }

        public UserResponse GetById(int id)
        {
            var user = getUser(id);
            return _mapper.Map<UserResponse>(user);
        }

        public UserResponse Create(CreateRequest model)
        {
            // validate
            if (_context.Users.Any(x => x.Email == model.Email))
                throw new AppException($"Email '{model.Email}' is already registered");

            // map model to new user object
            var user = _mapper.Map<User>(model);
            user.Created = DateTime.UtcNow;
            user.Verified = DateTime.UtcNow;

            // hash password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // save user
            _context.Users.Add(user);
            _context.SaveChanges();

            return _mapper.Map<UserResponse>(user);
        }

        public UserResponse Update(int id, UpdateRequest model)
        {
            var user = getUser(id);

            // validate
            if (user.Email != model.Email && _context.Users.Any(x => x.Email == model.Email))
                throw new AppException($"Email '{model.Email}' is already registered");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // copy model to user and save
            _mapper.Map(model, user);
            user.Updated = DateTime.UtcNow;
            _context.Users.Update(user);
            _context.SaveChanges();

            return _mapper.Map<UserResponse>(user);
        }

        public void Delete(int id)
        {
            var user = getUser(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        // helper methods

        private User getUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        private User getUserByRefreshToken(string token)
        {
            var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user == null) throw new AppException("Invalid token");
            return user;
        }

        private User getUserByResetToken(string token)
        {
            var user = _context.Users.SingleOrDefault(x =>
                x.ResetToken == token && x.ResetTokenExpires > DateTime.UtcNow);
            if (user == null) throw new AppException("Invalid token");
            return user;
        }

        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("user_id", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string generateResetToken()
        {
            // token is a cryptographically strong random sequence of values
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            // ensure token is unique by checking against db
            var tokenIsUnique = !_context.Users.Any(x => x.ResetToken == token);
            if (!tokenIsUnique)
                return generateResetToken();

            return token;
        }

        private string generateVerificationToken()
        {
            // token is a cryptographically strong random sequence of values
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            // ensure token is unique by checking against db
            var tokenIsUnique = !_context.Users.Any(x => x.VerificationToken == token);
            if (!tokenIsUnique)
                return generateVerificationToken();

            return token;
        }

        private RefreshToken rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            revokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
        }

        private void removeOldRefreshTokens(User user)
        {
            user.RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        private void revokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string ipAddress, string reason)
        {
            // recursively traverse the refresh token chain and ensure all descendants are revoked
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                    revokeRefreshToken(childToken, ipAddress, reason);
                else
                    revokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
            }
        }

        private void revokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }

        //private void sendVerificationEmail(User user, string origin)
        //{
        //    string message;
        //    if (!string.IsNullOrEmpty(origin))
        //    {
        //        // origin exists if request sent from browser single page app (e.g. Angular or React)
        //        // so send link to verify via single page app
        //        var verifyUrl = $"{origin}/user/verify-email?token={user.VerificationToken}";
        //        message = $@"<p>Please click the below link to verify your email address:</p>
        //                    <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";
        //    }
        //    else
        //    {
        //        // origin missing if request sent directly to api (e.g. from Postman)
        //        // so send instructions to verify directly with api
        //        message = $@"<p>Please use the below token to verify your email address with the <code>/users/verify-email</code> api route:</p>
        //                    <p><code>{user.VerificationToken}</code></p>";
        //    }

        //    _emailService.Send(
        //        to: user.Email,
        //        subject: "Sign-up Verification API - Verify Email",
        //        html: $@"<h4>Verify Email</h4>
        //                <p>Thanks for registering!</p>
        //                {message}"
        //    );
        //}

        //private void sendAlreadyRegisteredEmail(string email, string origin)
        //{
        //    string message;
        //    if (!string.IsNullOrEmpty(origin))
        //        message = $@"<p>If you don't know your password please visit the <a href=""{origin}/user/forgot-password"">forgot password</a> page.</p>";
        //    else
        //        message = "<p>If you don't know your password you can reset it via the <code>/users/forgot-password</code> api route.</p>";

        //    _emailService.Send(
        //        to: email,
        //    subject: "Sign-up Verification API - Email Already Registered",
        //        html: $@"<h4>Email Already Registered</h4>
        //                <p>Your email <strong>{email}</strong> is already registered.</p>
        //                {message}"
        //    );
        //}

        //private void sendPasswordResetEmail(User user, string origin)
        //{
        //    string message;
        //    if (!string.IsNullOrEmpty(origin))
        //    {
        //        var resetUrl = $"{origin}/user/reset-password?token={user.ResetToken}";
        //        message = $@"<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
        //                    <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
        //    }
        //    else
        //    {
        //        message = $@"<p>Please use the below token to reset your password with the <code>/users/reset-password</code> api route:</p>
        //                    <p><code>{user.ResetToken}</code></p>";
        //    }

        //    _emailService.Send(
        //        to: user.Email,
        //        subject: "Sign-up Verification API - Reset Password",
        //        html: $@"<h4>Reset Password Email</h4>
        //                {message}"
        //    );
        //}
    }


}

