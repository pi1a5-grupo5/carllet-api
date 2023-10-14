using Application.Requests.Vehicle;
using Domain.Entities.Budget;
using Domain.Entities.VehicleNS;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EarningController : ControllerBase
    {
        private readonly IEarningService _earningService;

        public EarningController(IEarningService earningService)
        {
            _earningService = earningService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEarning(Guid Id)
        {
            var result = await _earningService.GetEarningByUser(Id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Earning earning)
        {
            var result = await _earningService.RegisterEarning(earning);

            return Ok(result);
        }

    }
}
