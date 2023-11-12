using Application.ViewModels.Course;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class CourseMapperProfile : Profile
    {
        public CourseMapperProfile()
        {
            CreateMap<CourseDTO, Course>().ReverseMap();
        }
    }
}
