using AutoMapper;
using Finance.Application.DtoModels.Bank;
using Finance.Domain.Models;

namespace Finance.Application.Mappers
{
    public class BankProfile : Profile
    {
        public BankProfile()
        {
            CreateMap<Bank, BankDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
            CreateMap<BankTransaction, BankDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BankId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Bank.Name))
                .ReverseMap();
        }
    }
}