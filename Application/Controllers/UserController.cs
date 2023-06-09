﻿using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using Infra.Data;
using Application.Requests.User;

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
    /// <returns>Lista de usuários</returns>
    /// <response code="200">Retorna a lista de usuários</response>
    /// <response code="400">Se a lista de usuário for nula</response>
    /// <response code="500">Se houver algum erro interno</response>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var result = await _userService.GetUserList();

      if (result == null)
        return BadRequest(new { message = "Não há usuários cadastrados" });

      return Ok(result);
    }

        /// <summary>
        ///     Autentica um usuário no sistema
        /// </summary>
        /// <returns>Retorna o usuário autenticado</returns>
        /// <response code="200">Retorna o usuário autenticado</response>
        /// <response code="204">Se não houver usuários</response>
        /// <response code="500">Se houver algum erro interno</response>


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
    public async Task<IActionResult> Post([FromBody] NewUserRequest request)
    {
      User user = new User()
      {
        Name = request.Name,
        Email = request.Email,
        Password = request.Password,
        DeviceId = request.DeviceId
      };

      var result = await _userService.Register(user);

      return Ok(result);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Put([FromBody] User user)
    {
      var result = await _userService.Update(user);

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
      var result = await _userService.DeleteUser(id);

      return Ok(result);
    }
  }

}
