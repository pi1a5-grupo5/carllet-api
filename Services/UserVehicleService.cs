using Domain.Entities;
using Domain.Interfaces;
using Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserVehicleService : IUserVehicleService
    {
        private readonly CarlletDbContext _dbContext;
        public UserVehicleService(CarlletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserVehicle> CreateRelation(Guid userId, Guid vehicleId)
        {
            var newRelation = new UserVehicle(vehicleId, userId);
            _dbContext.Add(newRelation);
            _dbContext.SaveChanges();

            return newRelation;
        }
    }
}
