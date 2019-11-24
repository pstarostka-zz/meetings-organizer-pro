using AutoMapper;
using MOP.Abstract.Dto;
using MOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOP.Resolver.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserRegistrationDto>().ReverseMap();
            CreateMap<UserDto, UserRegistrationDto>().ReverseMap();
        }
    }
}
