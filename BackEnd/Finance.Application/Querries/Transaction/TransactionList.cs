using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DtoModels.Transaction;
using Finance.Application.Validations;
using Finance.Domain.Interfaces;
using MediatR;

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
            private readonly ITransactionRepository _transactionRepository;

            public Handler(ITransactionRepository transactionRepository, IMapper mapper)
            {
                _transactionRepository = transactionRepository;
                _mapper = mapper;
            }

            public async Task<Result<TransactionListDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var transactions = await _transactionRepository.GetTransactionsForeignData();
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