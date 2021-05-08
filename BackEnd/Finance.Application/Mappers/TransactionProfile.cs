using AutoMapper;
using Finance.Application.DtoModels.Transaction;
using Finance.Domain.Models;

namespace Finance.Application.Mappers
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<TransactionType, TransactionTypeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionTypes))
                .ReverseMap();

            CreateMap<Transaction, TransactionGetDto>()
                .ForMember(dest => dest.UserProfileDto, opt => opt.MapFrom(src => src.AppUser))
                .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType))
                .ReverseMap();
            //
            CreateMap<TransactionDto, Transaction>()
                .ForMember(dest => dest.TransactionTypeId, opt => opt.MapFrom(src => src.TransactionTypeId))
                .ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => src.UserProfileDto))
                .ReverseMap();


            //  CreateMap<Transaction, TransactionEditDto>().ReverseMap();

            CreateMap<Transaction, Transaction>().ReverseMap();
        }
    }
}