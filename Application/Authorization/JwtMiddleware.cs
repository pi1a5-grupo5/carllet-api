using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Common;

namespace Domain.Authorization
{

    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, AppSettings appSettings)
        {
            _next = next;
            _appSettings = appSettings;
        }

        public async Task Invoke(HttpContext context, DataContext dataContext, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var usetId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                // attach account to context on successful jwt validation
                context.Items["Account"] = await dataContext.Accounts.FindAsync(userId.Value);
            }

            await _next(context);
        }
    }
}
