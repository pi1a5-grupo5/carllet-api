using Domain.Entities;
using Domain.Entities.VehicleNS;
using Domain.Interfaces;
using Infra.Data;

namespace Services
{
    public class VehicleService : IVehicleService
    {
        private readonly CarlletDbContext _dbContext;


        public VehicleService(CarlletDbContext dbContext, IUserVehicleService userVehicleService)
        {
            _dbContext = dbContext;
        }

        public async Task<Vehicle> CreateVehicle(Vehicle vehicle)
        {
            var setVehicle = _dbContext.Vehicle.Add(vehicle);

            if (setVehicle == null)
            {
                return null;
            }

            _dbContext.SaveChanges();
            return vehicle;
        }

        public async Task<Vehicle> DeleteVehicle(Guid VehicleId)
        {
            var vehicle = _dbContext.Vehicle.Find(VehicleId);

            if (vehicle == null)
            {
                return null;
            }

            _dbContext.Vehicle.Remove(vehicle);
            _dbContext.SaveChanges();

            return vehicle;
        }

        public async Task<Vehicle> GetVehicleById(Guid VehicleId)
        {
            var vehicle = _dbContext.Vehicle.Find(VehicleId);

            if (vehicle == null)
            {
                return null;
            }

            return vehicle;
        }

        public async Task<List<Vehicle>> GetVehicleByOwner(Guid userId)
        {
            var vehicles = _dbContext.UserVehicles.Where(uv => uv.UserId == userId)
                .Select(uv => uv.Vehicle)
                .ToList();

            if(vehicles == null)
            {
                return null;
            }

            return vehicles;
        }

        //public async Task<List<Vehicle>> GetVehicleByOwner(int userId)
        //{

        //    var user = _dbContext.User.Include(u => u.Courses).FirstOrDefault(u => u.Id == userId);

        //    if (user == null)
        //    {
        //        return null;
        //    }
        //    List<Vehicle> vehicles  = user.Courses.Select(c => c.Vehicle).ToList();

        //    return vehicles;
        //}

        public async Task<List<Vehicle>> GetVehicleList()
        {
            var vehicles = _dbContext.Vehicle.ToList();
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
            var vehiclesBrands = _dbContext.VehicleBrand.ToList();
            if (vehiclesBrands.Count == 0)
            {
                return null;
            }

            return vehiclesBrands;
        }

        public async Task<List<VehicleType>> GetVehicleTypesList()
        {
            var vehiclesTypes = _dbContext.VehicleType.ToList();
            if (vehiclesTypes.Count == 0)
            {
                return null;
            }

            return vehiclesTypes;
        }
    }
}
