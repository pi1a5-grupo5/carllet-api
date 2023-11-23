using Application.ViewModels;
using Application.ViewModels.Vehicle;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.VehicleNS;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
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
            var userVehicle = await _vehicleService.GetVehicleById(VehicleId);

            if (userVehicle == null)
                return NotFound();

            var result = new VehicleResponse
            {
                VehicleId = userVehicle.Vehicle.VehicleId.ToString(),
                VehicleTypeName = userVehicle.Vehicle.VehicleType.Name,
                VehicleBrandName = userVehicle.Vehicle.VehicleType.VehicleBrand.Name,
                FabricationDate = userVehicle.Vehicle.FabricationDate,
                VehicleColor = userVehicle.Vehicle.VehicleColor,
                Odometer = userVehicle.Vehicle.Odometer,
                Rented = userVehicle.Vehicle.Rented,
                UserVehicleId = userVehicle.UserVehicleId.ToString(),
            };

            return Ok(result);
        }


        [HttpGet("ByUser/{UserId:Guid}")]
        public async Task<IActionResult> GetUserVehicleList(Guid UserId)
        {
            var UserVehicles = await _userVehicleService.GetUserVehicleByUserId(UserId);
            var result = _mapper.Map<List<UserVehicleResponse>>(UserVehicles);

            if (result.Count == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }

        /// <summary>
        ///     Retorna os veículos de um proprietário
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição
        ///     GET /Vehicle/byOwner/1
        ///     
        /// 
        /// </remarks>
        /// <returns>Lista de veículos</returns>
        /// <response code="200">Retorna a lista de veículos</response>
        /// <response code="400">Se a lista de veículos for nula</response>
        /// <response code="500">Se houver algum erro interno</response>
        [HttpGet("byOwner/{ownerId}")]
        public async Task<IActionResult> GetVehicleByOwner(Guid ownerId)
        {
            var userVehicles = await _vehicleService.GetVehicleByOwner(ownerId);
            var result = new List<VehicleResponse>();

            if (userVehicles == null)
                return NotFound();

            foreach (var userVehicle in userVehicles)
            {
                var vehicleByOwner = new VehicleResponse
                {
                    VehicleId = userVehicle.Vehicle.VehicleId.ToString(),
                    VehicleTypeName = userVehicle.Vehicle.VehicleType.Name,
                    VehicleBrandName = userVehicle.Vehicle.VehicleType.VehicleBrand.Name,
                    FabricationDate = userVehicle.Vehicle.FabricationDate,
                    VehicleColor = userVehicle.Vehicle.VehicleColor,
                    Odometer = userVehicle.Vehicle.Odometer,
                    Rented = userVehicle.Vehicle.Rented,
                    UserVehicleId = userVehicle.UserVehicleId.ToString(),
                };

                result.Add(vehicleByOwner);
            }

            return Ok(result);
        }
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
            int brandId, typeId;
            if (request.VehicleTypeId == 0 || request.VehicleTypeId == null)
            {
                brandId = await _vehicleService.ExistVehicleBrand(request.VehicleBrandName);
                if (brandId == 0)
                {
                    var createdBrand = await _vehicleService.CreateVehicleBrand(new VehicleBrand
                    {
                        Name = request.VehicleBrandName,
                    });

                    brandId = createdBrand.VehicleBrandId;
                }

                typeId = await _vehicleService.ExistVehicleType(request.VehicleTypeName);
                if (typeId == 0)
                {
                    var createdType = await _vehicleService.CreateVehicleType(new VehicleType { Name = request.VehicleTypeName, VehicleBrandId = brandId });
                    typeId = createdType.VehicleTypeId;
                }

                request.VehicleTypeId = typeId;
            }

            var vehicle = _mapper.Map<Vehicle>(request);
            var createdVehicle = await _vehicleService.CreateVehicle(vehicle);
            var createdUserVehicle = await _userVehicleService.CreateRelation(request.UserId, vehicle.VehicleId);

            var result = new VehicleResponse
            {
                VehicleId = createdVehicle.VehicleId.ToString(),
                VehicleTypeName = createdVehicle.VehicleType.Name,
                VehicleBrandName = createdVehicle.VehicleType.VehicleBrand.Name,
                FabricationDate = createdVehicle.FabricationDate,
                VehicleColor = createdVehicle.VehicleColor,
                Odometer = createdVehicle.Odometer,
                Rented = createdVehicle.Rented,
                UserVehicleId = createdUserVehicle.UserVehicleId.ToString(),
            };

            HttpClient client = new HttpClient();

            var values = new Dictionary<string, string> 
            {
                  { "brand", result.VehicleBrandName },
                  { "model", result.VehicleTypeName },
                    {"brand", result.VehicleColor }
              };
            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("", content);

            var responseString = await response.Content.ReadAsStringAsync();

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
        public async Task<IActionResult> RegisterVehicleBrand([FromBody] VehicleBrandRequest request)
        {
            var brand = _mapper.Map<VehicleBrand>(request);
            var vehicleBrand = await _vehicleService.CreateVehicleBrand(brand);
            var result = _mapper.Map<VehicleBrandResponse>(vehicleBrand);
            return Ok(result);
        }

        [HttpPost("type")]
        public async Task<IActionResult> RegisterVehicleType([FromBody] VehicleTypeRequest request)
        {
            var type = _mapper.Map<VehicleType>(request);
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

        [HttpGet("typeByBrand/{brandId:int}")]
        public async Task<IActionResult> GetVehicleTypesByBrand(int brandId)
        {
            var result = await _vehicleService.GetVehicleTypesByBrand(brandId);
            return Ok(result);
        }
    }
}
