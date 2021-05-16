using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DtoModels.TransactionType;
using Finance.Application.DtoModels.User;
using Finance.Application.Validations;
using Finance.Domain.Interfaces;
using Finance.Domain.Interfaces.Base;
using MediatR;

namespace Finance.Application.Querries.User
{
    public class UserDetails
    {
        public class Query : IRequest<Result<UserProfileDto>>
        {
            public string UserName { get; set; }
        }
        
        public class Handler : IRequestHandler<Query, Result<UserProfileDto>>
        {
            private readonly IMapper _mapper;
            private readonly IAppUserRepository _appUserRepository;
            private readonly IBaseRepository<Domain.Models.TransactionType> _transactionTypeRepository;

            public Handler(IAppUserRepository appUserRepository,IBaseRepository<Domain.Models.TransactionType> transactionTypeRepository, IMapper mapper)
            {
                _appUserRepository = appUserRepository;
                _transactionTypeRepository = transactionTypeRepository;
                _mapper = mapper;
            }

            public async Task<Result<UserProfileDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _appUserRepository.GetUserByUrlUsername(request.UserName);
                if (user == null) return null;
                var userProfileDto = _mapper.Map<UserProfileDto>(user);
                
                return Result<UserProfileDto>.Success(userProfileDto);
            }
        }
    }
}