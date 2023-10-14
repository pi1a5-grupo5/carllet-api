

using Domain.Entities.VehicleNS;

namespace Domain.Interfaces
{
    public interface IVehicleService
    {
        Task<Vehicle> CreateVehicle(Vehicle vehicle);
        Task<Vehicle> GetVehicleById(int id);
        Task<List<Vehicle>> GetVehicleByOwner(Guid userId);
        Task<List<Vehicle>> GetVehicleList();
        Task<Vehicle> DeleteVehicle(int id);
    }
}
