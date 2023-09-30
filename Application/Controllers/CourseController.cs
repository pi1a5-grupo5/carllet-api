using Application.Requests.Course;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        /// <summary>
        /// Registra um novo percurso
        /// </summary>
        /// <returns>
        /// Retorna o percurso registrado
        /// </returns>
        /// <response code="200">Retorna o percurso registrado</response>
        /// <response code="400">Se o percurso não for registrado</response>
        /// <response code="500">Se houver algum erro interno</response>

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewCourseRequest request)
        {
            Course course = new Course()
            {
                CourseEndTime = request.CourseEndTime,
                CourseLength = request.CourseLength,
                OwnerId = request.OwnerId,
            };

            //course.OwnerId = HttpContext.Items["UserId"];
            var result = await _courseService.Register(course);

            return Ok(result);
        }


        /// <summary>
        /// Retorna todos os percursos de um motorista
        /// </summary>
        /// <param name="driverId">Id do motorista</param>
        /// <returns>Lista de percursos</returns>
        /// <response code="200">Retorna a lista de percursos</response>
        /// <response code="400">Se não houver percursos registrados para o motorista</response>
        /// <response code="500">Se houver algum erro interno</response>

        [HttpGet("ByUserId/{driverId:Guid}")]
        public async Task<IActionResult> GetByUserId(Guid driverId)
        {
            //course.OwnerId = HttpContext.Items["UserId"];
            var result = await _courseService.GetByUserId(driverId);

            return Ok(result);
        }
    }
}
