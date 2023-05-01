using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;


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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _userService.GetUserList();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var result = await _userService.GetUser(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            user.Password = HashPassword(user.Password, WorkFactor);
            var result = await _userService.Register(user);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);

            return Ok(result);
        }
    }
}
