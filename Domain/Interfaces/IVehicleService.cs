

using Domain.Entities;
using Domain.Entities.VehicleNS;

namespace Domain.Interfaces
{
    public interface IVehicleService
    {
        Task<Vehicle> CreateVehicle(Vehicle vehicle);
        Task<UserVehicle> GetVehicleById(Guid VehicleId);
        Task<List<UserVehicle>> GetVehicleByOwner(Guid userId);
        Task<List<Vehicle>> GetVehicleList();
        Task<Vehicle> DeleteVehicle(Guid VehicleId);
        Task<VehicleBrand> CreateVehicleBrand(VehicleBrand brand);
        Task<VehicleType> CreateVehicleType(VehicleType type);
        Task<List<VehicleType>> GetVehicleTypesByBrand(int BrandId);
        Task<List<VehicleBrand>> GetVehicleBrandList();
        Task<List<VehicleType>> GetVehicleTypesList();
    }
}
