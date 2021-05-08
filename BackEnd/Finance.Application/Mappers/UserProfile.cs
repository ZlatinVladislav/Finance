﻿using AutoMapper;
using Finance.Application.DtoModels.User;
using Finance.Domain.Models;

namespace Finance.Application.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            /*
                This is where we are mapping from 
                entity to view model and vice versa.
            */
            CreateMap<AppUser, UserProfileDto>()
                .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap();
        }
    }
}