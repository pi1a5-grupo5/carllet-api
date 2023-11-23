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
            CreateMap<UserVehicle, VehicleResponse>();
            CreateMap<Vehicle, VehicleResponse>();
            CreateMap<VehicleType, VehicleResponse>();
            CreateMap<VehicleBrand, VehicleBrandResponse>();
            CreateMap<VehicleType, VehicleTypeResponse>();
            CreateMap<VehicleBrand, VehicleResponse>();
            CreateMap<VehicleTypeRequest, VehicleType>();
            CreateMap<VehicleBrandRequest, VehicleBrand>();

        }
    }
}
