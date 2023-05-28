using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Application.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _vehicleService.GetVehicleList();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var result = await _vehicleService.GetVehicleById(id);

            return Ok(result);
        }

        [HttpGet("byOwner/")]
        public async Task<IActionResult> GetVehicleByOwner(int ownerId)
        {
            var result = await _vehicleService.GetVehicleByOwner(ownerId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Vehicle vehicle)
        {
            var result = await _vehicleService.CreateVehicle(vehicle);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var result = await _vehicleService.DeleteVehicle(id);

            return Ok(result);
        }

    }
}
