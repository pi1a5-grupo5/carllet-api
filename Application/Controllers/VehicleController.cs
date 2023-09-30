using Application.Requests.Vehicle;
using Domain.Entities.Vehicle;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        ///     Retorna todos os veículos do sistema
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição
        ///     GET /Vehicle
        /// </remarks>
        /// <returns>Lista de veículos</returns>
        /// <response code="200">Retorna a lista de veículos</response>
        /// <response code="400">Se a lista de veiculos for nula</response>
        /// <response code="500">Se houver algum erro interno</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _vehicleService.GetVehicleList();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        ///     Retorna um veículo do sistema
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição
        ///     GET /Vehicle/1
        /// </remarks>
        /// <returns>Veículo</returns>
        /// <response code="200">Retorna o veículo</response>
        /// <response code="204">Se não houver veículo com esse ID</response>
        /// <response code="500">Se houver algum erro interno</response>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var result = await _vehicleService.GetVehicleById(id);

            return Ok(result);
        }

        /// <summary>
        ///     Retorna os veículos de um proprietário
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição
        ///     GET /Vehicle/byOwner/1
        /// </remarks>
        /// <returns>Lista de veículos</returns>
        /// <response code="200">Retorna a lista de veículos</response>
        /// <response code="400">Se a lista de veículos for nula</response>
        /// <response code="500">Se houver algum erro interno</response>
        [HttpGet("byOwner/{ownerId}")]
        public async Task<IActionResult> GetVehicleByOwner(Guid ownerId)
        {
            var result = await _vehicleService.GetVehicleByOwner(ownerId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        ///     Cria um veículo no sistema
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição
        ///     POST /Vehicle
        /// </remarks>
        /// <returns>Veículo criado</returns>
        /// <response code="200">Retorna o veículo criado</response>
        /// <response code="400">Se não houver veículo</response>
        /// <response code="500">Se houver algum erro interno</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewVehicleRequest request)
        {
            Vehicle vehicle = new Vehicle()
            {
                Brand = request.Brand,
                Model = request.Model,
                FabricationDate = request.FabricationYear,
                Odometer = request.Odometer,
                rented = request.Rented
            };

            var result = await _vehicleService.CreateVehicle(vehicle);

            return Ok(result);
        }

        /// <summary>
        ///     Deleta um veículo no sistema
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição
        ///     DELETE /Vehicle/1
        /// </remarks>
        /// <returns>Veículo deletado</returns>
        /// <response code="200">Retorna o veículo deletado</response>
        /// <response code="400">Se não houver veículo</response>
        /// <response code="500">Se houver algum erro interno</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var result = await _vehicleService.DeleteVehicle(id);

            return Ok(result);
        }

    }
}
