using Domain.Entities;
using Domain.Interfaces;
using Infra.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private CarlletDbContext _dbContext;

        public AuthService(IConfiguration configuration, CarlletDbContext dbContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<string> GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWTKey"]);
            var expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["AccessTokenExpirationMinutes"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            var updateUser = _dbContext.User.SingleOrDefault(u => u.Id == user.Id);

            if (updateUser == null)
            {
                return null;
            }

            updateUser.AccessToken = accessToken;
            updateUser.AccessTokenExpiration = expires;
            _dbContext.SaveChanges();

            return updateUser.AccessToken;
        }

        public async Task<string> GenerateRefreshToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWTKey"]);
            var expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["RefreshTokenExpirationMinutes"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = tokenHandler.WriteToken(token);

            var updateUser = _dbContext.User.SingleOrDefault(u => u.Id == user.Id);

            if (updateUser == null)
            {
                return null;
            }

            updateUser.RefreshToken = refreshToken;
            updateUser.RefreshTokenExpiration = expires;
            _dbContext.SaveChanges();

            return updateUser.RefreshToken;
        }

        public async Task<int?> ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                return userId;
            }
            catch
            {
                return null;
            }
        }
    }
}
