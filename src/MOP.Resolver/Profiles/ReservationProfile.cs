using AutoMapper;
using MOP.Abstract.Dto;
using MOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOP.Resolver.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationDto>().ReverseMap();
            CreateMap<ReservationUser, ReservationUsersDto>().ReverseMap();
        }
    }
}
