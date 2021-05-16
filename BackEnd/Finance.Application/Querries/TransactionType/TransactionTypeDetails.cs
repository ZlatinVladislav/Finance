using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DtoModels.TransactionType;
using Finance.Application.Validations;
using Finance.Domain.Interfaces;
using MediatR;

namespace Finance.Application.Querries.TransactionType
{
    public class TransactionTypeDetails
    {
        public class Query : IRequest<Result<TransactionTypeDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<TransactionTypeDto>>
        {
            private readonly IMapper _mapper;
            private readonly ITransactionTypeRepository _transactionTypeRepository;

            public Handler(IMapper mapper,
                ITransactionTypeRepository transactionTypeRepository)
            {
                _mapper = mapper;
                _transactionTypeRepository = transactionTypeRepository;
            }

            public async Task<Result<TransactionTypeDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var transactionTypeDto = await _transactionTypeRepository.GetById(request.Id);
                var transactionType = _mapper.Map<TransactionTypeDto>(transactionTypeDto);

                return Result<TransactionTypeDto>.Success(transactionType);
            }
        }
    }
}