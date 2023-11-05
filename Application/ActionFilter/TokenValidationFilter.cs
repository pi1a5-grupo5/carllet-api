using Application.Controllers;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services;
using System.IdentityModel.Tokens.Jwt;

namespace Application.ActionFilter
{
    public class TokenValidationFilter : IActionFilter
    {
        private readonly IAuthService _authService;

        public TokenValidationFilter(IAuthService authService)
        {
            _authService = authService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //var accessToken = context.HttpContext.Request.Headers["Authorization"].ToString();


            //if (string.IsNullOrEmpty(accessToken))
            //{
            //    context.Result = new BadRequestObjectResult("Token inválido ou ausente");
            //    return;
            //}

            //bool isTokenValid = _authService.ValidateToken(accessToken, out JwtSecurityToken jwt);

            //if (!isTokenValid)
            //{
            //    context.Result = new UnauthorizedObjectResult("Token inválido");
            //    return;
            //}
            //var c = context.Controller as HomeController;


        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
