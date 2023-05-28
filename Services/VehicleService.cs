using Domain.Entities;
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
            var setVehicle = _dbContext.Vehicle.Add(vehicle);

            if (setVehicle == null)
            {
                return null;
            }

            _dbContext.SaveChanges();

            return vehicle;
        }

        public async Task<Vehicle> DeleteVehicle(int id)
        {
            var vehicle = _dbContext.Vehicle.Find(id);

            if (vehicle == null)
            {
                return null;
            }

            _dbContext.Vehicle.Remove(vehicle);
            _dbContext.SaveChanges();

            return vehicle;
        }

        public async Task<Vehicle> GetVehicleById(int id)
        {
            var vehicle = _dbContext.Vehicle.Find(id);

            if (vehicle == null)
            {
                return null;
            }

            return vehicle;
        }

        public async Task<List<Vehicle>> GetVehicleByOwner(int userId)
        {
            var owners = _dbContext.User.Include(u => u.Courses)
                .ThenInclude(p => p.Vehicle)
                .FirstOrDefault(u => u.Id== userId);
               
            var vehicles = owners.Courses.Select(p => p.Vehicle).ToList();

            if (vehicles == null)
            {
                return null;
            }

            return vehicles;
        }

        public async Task<List<Vehicle>> GetVehicleList()
        {
            var vehicles = _dbContext.Vehicle.ToList();


            if (vehicles.Count == 0)
            {
                return null;
            }

            return vehicles;
        }
    }
}
