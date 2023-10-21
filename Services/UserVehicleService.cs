using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserVehicleService : IUserVehicleService
    {
        public Task<UserVehicle> CreateRelation(Guid userId, Guid vehicleId)
        {
            throw new NotImplementedException();
        }
    }
}
