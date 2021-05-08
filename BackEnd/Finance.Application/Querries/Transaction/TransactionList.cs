using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DtoModels.Transaction;
using Finance.Application.Validations;
using Finance.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Finance.Application.Querries.Transaction
{
    public class TransactionList
    {
        public class Query : IRequest<Result<TransactionListDto>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<TransactionListDto>>
        {
            private readonly IMapper _mapper;
            private readonly IAppUserRepository _appUserRepository;
            private readonly ITransactionRepository _transactionRepository;

            public Handler(IAppUserRepository appUserRepository,ITransactionRepository transactionRepository, IMapper mapper)
            {
                _appUserRepository = appUserRepository;
                _transactionRepository = transactionRepository;
                _mapper = mapper;
            }

            public async Task<Result<TransactionListDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _appUserRepository.GetUser();
                
                var transactions = await _transactionRepository.GetTransactionsForeignData(user.Id);
                var transactionDto = _mapper.Map<IEnumerable<TransactionGetDto>>(transactions);

                var transactionListDto = new TransactionListDto
                {
                    Transactions = transactionDto
                };

                return Result<TransactionListDto>.Success(transactionListDto);
            }
        }
    }
}
// var tansactionId = Guid.Parse(_httpContextAccessor.HttpContext?.Request.RouteValues
//     .SingleOrDefault(x => x.Key == "id").Value.ToString());
//
// var transaction = _transactionRepository.GetTransactionsForeignDataById(tansactionId).Result;
//             
// if(transaction==null) return  Task.CompletedTask;
//            
// if (userId.Value== transaction.AppUser.Id) context.Succeed(requirement);