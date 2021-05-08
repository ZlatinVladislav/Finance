using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DtoModels.Transaction;
using Finance.Application.Validations;
using Finance.Domain.Interfaces;
using Finance.Domain.Interfaces.Base;
using MediatR;

namespace Finance.Application.Querries.Transaction
{
    public class TransactionDetails
    {
        public class Query : IRequest<Result<TransactionGetDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<TransactionGetDto>>
        {
            private readonly IMapper _mapper;
            private readonly ITransactionRepository _transactionRepository;

            public Handler(IMapper mapper,
                ITransactionRepository transactionRepository)
            {
                _mapper = mapper;
                _transactionRepository = transactionRepository;
            }

            public async Task<Result<TransactionGetDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var transactionDto = await _transactionRepository.GetTransactionsForeignDataById(request.Id);
                var transaction = _mapper.Map<TransactionGetDto>(transactionDto);

                return Result<TransactionGetDto>.Success(transaction);
            }
        }
    }
}