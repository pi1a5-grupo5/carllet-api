using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using Infra.Data;
using Application.Requests;

namespace Application.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {        
        private readonly IUserService _userService;
        private const int WorkFactor = 8;

        public UserController(IUserService userService)
        {
            _userService = userService; 
         }

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
            var result = await _userService.GetUserList();

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var result = await _userService.Login(model.Email, model.Password);

            return Ok(result);
        }

        /// <summary>
        ///     Retorna um usuário espeficio do sistema
        /// </summary>
        /// <remarks>
        /// Exemplo de request
        ///     GET /User/{id}
        /// </remarks>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var result = await _userService.GetUser(id);

            return Ok(result);
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
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            user.Password = HashPassword(user.Password, WorkFactor);
            var result = await _userService.Register(user);

            return Ok(result);
        }


        /// <summary>
        ///     Deleta um usuária o partir do seu Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Delete /id
        ///     {
        ///     }
        ///
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);

            return Ok(result);
        }
    }

}
