using Application.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.ActionFilter
{
    public class TokenValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var accessToken = context.HttpContext.Request.Headers["Authorization"].ToString();


            if (string.IsNullOrEmpty(accessToken))
            {
                context.Result = new BadRequestObjectResult("Token inválido ou ausente");
                return;
            }

            bool isTokenValid = ValidateToken(accessToken);
            if (!isTokenValid)
            {
                context.Result = new UnauthorizedObjectResult("Token inválido");
                return;
            }
            var c = context.Controller as HomeController;


        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
        private bool ValidateToken(string accessToken)
        {
            throw new NotImplementedException();
        }
    }
}
