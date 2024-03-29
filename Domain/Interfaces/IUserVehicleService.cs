﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserVehicleService
    {
        Task<UserVehicle> CreateRelation(Guid userId, Guid vehicleId);
        Task<List<UserVehicle>> GetUserVehicleByUserId(Guid userId);
    }
}
