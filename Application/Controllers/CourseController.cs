using Application.ViewModels.Course;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : HomeController
    {
        private readonly ICourseService _courseService;
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public CourseController(ICourseService courseService, IMapper mapper, IVehicleService vehicleService)
        {
            _courseService = courseService;
            _mapper = mapper;
            _vehicleService = vehicleService;
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
        public async Task<IActionResult> Post([FromBody] CourseDTO request)
        {
            var courseReq = _mapper.Map<Course>(request);
            var courseRes = await _courseService.Register(courseReq);
            if (courseRes == null)
            {
                return BadRequest();
            }

            var result = _mapper.Map<CourseDTO>(courseRes);

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
            var courseRes = await _courseService.GetByUserId(driverId);

            if (courseRes == null)
            {
                return BadRequest();
            }

            if(courseRes.Count== 0)
            {
                return NoContent();
            }

            var result = _mapper.Map<List<CourseDTO>>(courseRes);

            return Ok(result);
        }
    }
}
