using Application.ViewModels.Vehicle;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.VehicleNS;

namespace Application.Mappers
{
    public class VehicleMapperProfile : Profile
    {
        public VehicleMapperProfile()
        {
            CreateMap<NewVehicleRequest, Vehicle>();
            CreateMap<Vehicle, VehicleResponse>();
            CreateMap<UserVehicle, VehicleResponse>();
            CreateMap<VehicleType, VehicleResponse>();
            CreateMap<VehicleBrand, VehicleResponse>();
        }
    }
}
