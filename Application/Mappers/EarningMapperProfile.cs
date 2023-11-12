using Application.ViewModels.Earning;
using AutoMapper;
using Domain.Entities.Budget;

namespace Application.Mappers
{
    public class EarningMapperProfile : Profile
    {
        public EarningMapperProfile()
        {
            CreateMap<EarningRequest, Earning>().ReverseMap();
            CreateMap<EarningResponse, Earning>().ReverseMap();

        }
    }
}
