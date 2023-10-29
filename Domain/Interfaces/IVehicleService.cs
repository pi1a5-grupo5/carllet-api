

using Domain.Entities.VehicleNS;

namespace Domain.Interfaces
{
    public interface IVehicleService
    {
        Task<Vehicle> CreateVehicle(Vehicle vehicle);
        Task<Vehicle> GetVehicleById(Guid VehicleId);
        Task<List<Vehicle>> GetVehicleByOwner(Guid userId);
        Task<List<Vehicle>> GetVehicleList();
        Task<Vehicle> DeleteVehicle(Guid VehicleId);
        Task<VehicleBrand> CreateVehicleBrand(VehicleBrand brand);
        Task<VehicleType> CreateVehicleType(VehicleType type);
        Task<List<VehicleBrand>> GetVehicleBrandList();
        Task<List<VehicleType>> GetVehicleTypesList();
    }
}
