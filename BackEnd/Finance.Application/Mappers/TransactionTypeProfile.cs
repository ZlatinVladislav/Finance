using AutoMapper;
using Finance.Application.DtoModels.TransactionType;
using Finance.Domain.Models;

namespace Finance.Application.Mappers
{
    public class TransactionTypeProfile : Profile
    {
        public TransactionTypeProfile()
        {
            CreateMap<TransactionType, TransactionTypeDto>()
                .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionTypes))
                .ReverseMap();
        }
    }
}