using System.Linq;
using AutoMapper;
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
                .ForMember(dest => dest.UserDescription, opt => opt.MapFrom(src => src.UserDescription.Description))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ReverseMap();
            
            CreateMap<UserDescription, UserDescriptionDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ReverseMap();
        }
    }
}