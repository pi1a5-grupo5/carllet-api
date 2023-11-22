using Domain.Entities;
using Domain.Entities.VehicleNS;
using Domain.Interfaces;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class VehicleService : IVehicleService
    {
        private readonly CarlletDbContext _dbContext;


        public VehicleService(CarlletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Vehicle> CreateVehicle(Vehicle vehicle)
        {
            var setVehicle = _dbContext.Vehicles.Add(vehicle);

            if (setVehicle == null)
            {
                return null;
            }

            await _dbContext.SaveChangesAsync();
            var returnVehicle = _dbContext.Vehicles.Where(v => v.VehicleId == vehicle.VehicleId)
            .Include(v => v.VehicleType)
            .ThenInclude(vt => vt.VehicleBrand)
            .FirstOrDefault();

            return returnVehicle;
        }

        public async Task<Vehicle> DeleteVehicle(Guid VehicleId)
        {
            var vehicle = _dbContext.Vehicles.Find(VehicleId);

            if (vehicle == null)
            {
                return null;
            }

            _dbContext.Vehicles.Remove(vehicle);
            _dbContext.SaveChanges();

            return vehicle;
        }

        public async Task<UserVehicle> GetVehicleById(Guid VehicleId)
        {
            var vehicle = _dbContext.UserVehicles.Where(uv => uv.Vehicle.VehicleId == VehicleId)
                .Include(uv => uv.Vehicle)
                .ThenInclude(v => v.VehicleType)
                .ThenInclude(vt => vt.VehicleBrand)
                .FirstOrDefault();

            if (vehicle == null)
            {
                return null;
            }

            return vehicle;
        }

        public async Task<List<UserVehicle>> GetVehicleByOwner(Guid userId)
        {
            var vehicles = _dbContext.UserVehicles
                .Where(uv => uv.UserId == userId)
                .Include(uv => uv.Vehicle)
                .ThenInclude(v => v.VehicleType)
                .ThenInclude(vt => vt.VehicleBrand)
                .ToList();    

            if (vehicles == null)
            {
                return null;
            }

            return vehicles;
        }

        public async Task<List<Vehicle>> GetVehicleList()
        {
            var vehicles = _dbContext.Vehicles.ToList();
            if (vehicles.Count == 0)
            {
                return null;
            }

            return vehicles;
        }

        public async Task<VehicleBrand> CreateVehicleBrand(VehicleBrand brand)
        {
            _dbContext.Add(brand);
            _dbContext.SaveChanges();
            return brand;
        }
        public async Task<VehicleType> CreateVehicleType(VehicleType type)
        {
            _dbContext.Add(type);
            _dbContext.SaveChanges();
            return type;
        }

        public async Task<List<VehicleBrand>> GetVehicleBrandList()
        {
            var vehiclesBrands = _dbContext.VehicleBrands.ToList();
            if (vehiclesBrands.Count == 0)
            {
                return null;
            }

            return vehiclesBrands;
        }

        public async Task<List<VehicleType>> GetVehicleTypesList()
        {
            var vehiclesTypes = _dbContext.VehicleTypes.ToList();
            if (vehiclesTypes.Count == 0)
            {
                return null;
            }

            return vehiclesTypes;
        }

        public async Task<List<VehicleType>> GetVehicleTypesByBrand(int BrandId)
        {
            var vehiclesTypes = _dbContext.VehicleTypes.Where(vt => vt.VehicleBrandId == BrandId).ToList();
            if (vehiclesTypes.Count == 0)
            {
                return null;
            }

            return vehiclesTypes;
        }

        public async Task<int> ExistVehicleBrand(string BrandName)
        {
            var existBrand = await _dbContext.VehicleBrands.Where(vb => vb.Name == BrandName).FirstOrDefaultAsync();
            if (existBrand != null) {
                return existBrand.VehicleBrandId;
            }
            return 0; 
        }

        public async Task<int> ExistVehicleType(string TypeName)
        {
            var existType = await _dbContext.VehicleTypes.Where(vt => vt.Name == TypeName).FirstOrDefaultAsync();
            if (existType != null)
            {
                return existType.VehicleTypeId;
            }
            return 0;

        }
    }
}
