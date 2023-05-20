using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Authorization;
using Domain.Models.Users;



namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {   
        
        private readonly IUserService _userService;

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model, ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public ActionResult<AuthenticateResponse> RefreshToken()
        /// <summary>
        ///     Retorna todos os usuários do sistema
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição
        ///     GET /User
        /// </remarks>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = _userService.RefreshToken(refreshToken, ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("revoke-token")]
        public IActionResult RevokeToken(RevokeTokenRequest model)
        {
            // accept token from request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            // users can revoke their own tokens and admins can revoke any tokens
            if (!User.OwnsToken(token) && User.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            _userService.RevokeToken(token, ipAddress());
            return Ok(new { message = "Token revoked" });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)
        {
            _userService.Register(model, Request.Headers["origin"]);
            return Ok(new { message = "Registration successful, please check your email for verification instructions" });
        }

        //[AllowAnonymous]
        //[HttpPost("verify-email")]
        //public IActionResult VerifyEmail(VerifyEmailRequest model)
        //{
        //    _userService.VerifyEmail(model.Token);
        //    return Ok(new { message = "Verification successful, you can now login" });
        //}

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(ForgotPasswordRequest model)
        {
            _userService.ForgotPassword(model, Request.Headers["origin"]);
            return Ok(new { message = "Please check your email for password reset instructions" });
        }

        [AllowAnonymous]
        [HttpPost("validate-reset-token")]
        public IActionResult ValidateResetToken(ValidateResetTokenRequest model)
        {
            _userService.ValidateResetToken(model);
            return Ok(new { message = "Token is valid" });
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public IActionResult ResetPassword(ResetPasswordRequest model)
        {
            _userService.ResetPassword(model);
            return Ok(new { message = "Password reset successful, you can now login" });
        }

        /// <summary>
        ///     Retorna todos os usuários do sistema
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição
        ///     GET /User
        /// </remarks>
        [Authorize(Role.Admin)]
        [HttpGet]
        public ActionResult<IEnumerable<UserResponse>> GetAll()
        {
            var accounts = _userService.GetAll();
            return Ok(accounts);
        }

        /// <summary>
        ///     Retorna um usuário espeficio do sistema
        /// </summary>
        /// <remarks>
        /// Exemplo de request
        ///     GET /User/{id}
        /// </remarks>
        [HttpGet("{id:int}")]
        public ActionResult<UserResponse> GetById(int id)
        {
            // users can get their own account and admins can get any account
            if (id != User.UserId && User.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            var user = _userService.GetById(id);
            return Ok(user);
        }

        /// <summary>
        ///     Registra um novo usuário e criptografa sua senha
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /User
        ///     {
        ///         "name": "João",
        ///         "email": "joao@gmail.com",
        ///         "password": "123456",
        ///         "devideId": "167536"
        ///     }
        ///     
        /// </remarks>
        [Authorize(Role.Admin)]
        [HttpPost]
        public ActionResult<UserResponse> Create(CreateRequest model)
        {
            var user = _userService.Create(model);
            return Ok(user);
        }

        [HttpPut("{id:int}")]
        public ActionResult<UserResponse> Update(int id, UpdateRequest model)
        {
            // users can update their own account and admins can update any account
            if (id != User.Id && User.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            // only admins can update role
            if (User.Role != Role.Admin)
                model.Role = null;

            var account = _userService.Update(id, model);
            return Ok(account);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            // users can delete their own account and admins can delete any account
            if (id != User.UserId && User.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            _userService.Delete(id);
            return Ok(new { message = "Account deleted successfully" });
        }


        /// <summary>
        ///     Deleta um usuária o partir do seu Id
        /// </summary>
        /// Sample request:
        ///     Delete /id
        ///     {
        ///        "id": 1,
        ///        "name": "Item #1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
        // helper methods

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

    }
}
