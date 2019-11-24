using AutoMapper;
using MOP.Abstract.Dto;
using MOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOP.Resolver.Profiles
{
    public class ConferenceRoomProfile : Profile
    {
        public ConferenceRoomProfile()
        {
            CreateMap<ConferenceRoom, ConferenceRoomDto>().ReverseMap();

        }
    }
}
