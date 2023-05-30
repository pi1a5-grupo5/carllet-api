using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services;

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
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Course course)
        {
            //course.OwnerId = HttpContext.Items["UserId"];
            var result = await _courseService.Register(course);

            return Ok(result);
        }

        [HttpGet("ByUserId/{id:int}")]
        public async Task<IActionResult> GetByUserId(Guid driverId)
        {
            //course.OwnerId = HttpContext.Items["UserId"];
            var result = await _courseService.GetByUserId(driverId);

            return Ok(result);
        }
    }
}
