using Application.ViewModels.User;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Application.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : HomeController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        ///     Retorna todos os usuários do sistema
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição
        ///     GET /User
        /// </remarks>
        /// <returns>Lista de usuários</returns>
        /// <response code="200">Retorna a lista de usuários</response>
        /// <response code="400">Se a lista de usuário for nula</response>
        /// <response code="500">Se houver algum erro interno</response>
        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var result = await _userService.GetUserList();

        //    if (result == null)
        //        return BadRequest(new { message = "Não há usuários cadastrados" });

        //    return Ok(result);
        //}

        /// <summary>
        ///     Autentica um usuário no sistema
        /// </summary>
        /// <returns>Retorna o usuário autenticado</returns>
        /// <response code="200">Retorna o usuário autenticado</response>
        /// <response code="204">Se não houver usuários</response>
        /// <response code="500">Se houver algum erro interno</response>


        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest model)
        {
            var result = await _userService.Login(model.Email, model.Password);

            return Ok(result);
        }

        /// <summary>
        ///     Retorna um usuário especifico do sistema
        /// </summary>
        /// <remarks>
        /// Exemplo de request
        ///     GET /User/{id}
        /// </remarks>
        /// <returns>Retorna o usuário</returns>
        /// <response code="200">Retorna o usuário</response>
        /// <response code="204">Se o usuário não for encontrado</response>
        /// <response code="500">Se houver algum erro interno</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var result = await _userService.GetUser(id);

            return Ok(result);
        }

        /// <summary>
        ///     Registra um novo usuário e criptografa sua senha
        /// </summary>
        /// <returns>Retorna o usuário criado</returns>
        /// <response code="200">Retorna o usuário criado</response>
        /// <response code="500">Se houver algum erro interno</response>
        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Post([FromBody] NewUserRequest request)
        {
            var user = _mapper.Map<User>(request);
            var registeredUser = await _userService.Register(user);
            var result = _mapper.Map<UserResponse>(registeredUser);

            return Ok(result);
        }

        [HttpGet("verificationToken/{verificationToken}")]
        public async Task<IActionResult> VerifyEmail(string verificationToken)
        {
           _userService.VerifyEmail(verificationToken);
            return Ok();
        }

        [HttpGet("forgotPassword/{email}")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            _userService.ForgotPassword(email);
            return Ok();
        }

       [HttpPost("ResetPassword/{resetToken}")]
       public async Task<IActionResult> ResetPassword(User user)
        {
            _userService.ResetPassword(user);
            return Ok();
        }


        /// <summary>
        ///     Altera os dados de um usuário
        /// </summary>
        /// <returns>Retorna o usuário criado</returns>
        /// <response code="200">Retorna o usuário criado</response>
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            var UpdatedUser = await _userService.Update(user);
            var result = _mapper.Map<UserResponse>(UpdatedUser);

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
        /// <returns>Retorna o usuário deletado</returns>
        /// <response code="200">Retorna o usuário deletado</response>
        /// <response code="204">Se o usuário não for encontrado</response>
        /// <response code="500">Se houver algum erro interno</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var deletedUser = await _userService.DeleteUser(id);
            var result = _mapper.Map<UserResponse>(deletedUser);

            return Ok(result);
        }
    }

}
