﻿using AutoMapper;
using Domain.Entities;
using Domain.Models.Users;
using System.Security.Principal;

namespace Infra.Data
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserResponse>();

            CreateMap<User, AuthenticateResponse>();

            CreateMap<RegisterRequest, User>();

            CreateMap<CreateRequest, User>();

            CreateMap<UpdateRequest, User>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        // ignore null role
                        if (x.DestinationMember.Name == "Role" && src.Role == null) return false;

                        return true;
                    }
                ));
        }
    }
}
