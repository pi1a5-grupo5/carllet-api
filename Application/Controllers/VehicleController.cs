using Application.ViewModels;
using Application.ViewModels.Vehicle;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.VehicleNS;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Application.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : HomeController
    {
        private readonly IVehicleService _vehicleService;
        private readonly IUserVehicleService _userVehicleService;
        private readonly IMapper _mapper;

        public VehicleController(IVehicleService vehicleService, IMapper mapper, IUserVehicleService userVehicleService)
        {
            _vehicleService = vehicleService;
            _userVehicleService = userVehicleService;
            _mapper = mapper;
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
        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var result = await _vehicleService.GetVehicleList();


        //    if (result == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(result);
        //}

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
        [HttpGet("{VehicleId:Guid}")]
        public async Task<IActionResult> GetVehicle(Guid VehicleId)
        {
            var vehicle = await _vehicleService.GetVehicleById(VehicleId);
            var result = _mapper.Map<VehicleResponse>(vehicle);

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
            var vehicles = await _vehicleService.GetVehicleByOwner(ownerId);
            var result = _mapper.Map<List<VehicleResponse>>(vehicles);

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
            var vehicle = _mapper.Map<Vehicle>(request);

            var createdVehicle = await _vehicleService.CreateVehicle(vehicle);
            var result = _mapper.Map<Vehicle, VehicleResponse>(createdVehicle);
            var createdUserVehicle = await _userVehicleService.CreateRelation(request.UserId, vehicle.VehicleId);
            result = _mapper.Map<UserVehicle, VehicleResponse>(createdUserVehicle, result);

            return Ok(result);
        }

        [HttpPost("relate")]
        public async Task<IActionResult> RelateUserVehicle([FromBody] Guid userId, Guid vehicleId)
        {
            var createdUserVehicle = await _userVehicleService.CreateRelation(vehicleId, userId);
            var result = _mapper.Map<VehicleResponse>(createdUserVehicle);

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
        [HttpDelete("{VehicleId}")]
        public async Task<IActionResult> DeleteVehicle(Guid VehicleId)
        {
            var vehicle = await _vehicleService.DeleteVehicle(VehicleId);
            var result = _mapper.Map<VehicleResponse>(vehicle);

            return Ok(result);
        }

        [HttpPost("brand")]
        public async Task<IActionResult> RegisterVehicleBrand([FromBody] VehicleBrand brand)
        {
            var vehicleBrand = await _vehicleService.CreateVehicleBrand(brand);
            var result = _mapper.Map<VehicleBrandResponse>(vehicleBrand);
            return Ok(result);
        }

        [HttpPost("type")]
        public async Task<IActionResult> RegisterVehicleType([FromBody] VehicleType type)
        {
            var result = await _vehicleService.CreateVehicleType(type);
            return Ok(result);
        }

        [HttpGet("brand")]
        public async Task<IActionResult> GetVehicleBrandsList()
        {
            var result = await _vehicleService.GetVehicleBrandList();
            return Ok(result);
        }

        [HttpGet("type")]
        public async Task<IActionResult> GetVehicleTypesList()
        {
            var result = await _vehicleService.GetVehicleTypesList();
            return Ok(result);
        }
    }
}
