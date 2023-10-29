using Application.ViewModels.User;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<NewUserRequest, User>();
            CreateMap<LoginRequest, User>();
            CreateMap<ResetPasswordRequest, User>();
            CreateMap<User, LoginResponse>();
            CreateMap<User, UserResponse>();
             
        }
    }
}
