using AutoMapper;
using Finance.Application.DtoModels.Transaction;
using Finance.Application.DtoModels.TransactionType;
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
                .ForMember(dest => dest.TransactionType,
                    opt => opt.MapFrom(src => src.TransactionType.TransactionTypes))
                .ForMember(dest => dest.BankDto, opt => opt.MapFrom(src => src.Banks))
                .ReverseMap();


            CreateMap<TransactionDto, Transaction>()
                .ForMember(dest => dest.TransactionTypeId, opt => opt.MapFrom(src => src.TransactionTypeId))
                .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.UserProfileDto))
                .ForMember(dest => dest.Banks, opt => opt.MapFrom(src => src.BankDto))
                .ReverseMap();

            CreateMap<Transaction, Transaction>().ReverseMap();
        }
    }
}