using Domain.Entities.Budget;
using Domain.Entities.VehicleNS;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EarningController : HomeController
    {
        private readonly IEarningService _earningService;

        public EarningController(IEarningService earningService)
        {
            _earningService = earningService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEarningById(Guid Id)
        {
            var result = await _earningService.GetEarningById(Id);

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

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Earning earning)
        {
            var result = await _earningService.RegisterEarning(earning);

            return Ok(result);
        }



        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Guid earningId)
        {
            var result = await _earningService.DeleteEarning(earningId);

            return Ok(result);
        }

    }
}
