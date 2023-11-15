using Application.ViewModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class MiscMapperProfile : Profile
    {
        public MiscMapperProfile()
        {
            CreateMap<GoalRequest, Goal>().ReverseMap();
            CreateMap<Goal, GoalResponse>().ReverseMap();   
        }
    }
}
