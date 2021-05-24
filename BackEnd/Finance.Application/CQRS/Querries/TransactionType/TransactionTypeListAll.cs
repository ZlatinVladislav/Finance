using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DtoModels.TransactionType;
using Finance.Application.Validations;
using Finance.Infrastructure.Data.Interfaces;
using Finance.Infrastructure.Data.Repositories;
using MediatR;

namespace Finance.Application.CQRS.Querries.TransactionType
{
    public class TransactionTypeListAll
    {
        public class Query : IRequest<Result<IEnumerable<TransactionTypeDto>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<IEnumerable<TransactionTypeDto>>>
        {
            private readonly ITransactionTypeRepository _transactionTypeRepository;
            private readonly IMapper _mapper;


            public Handler(ITransactionTypeRepository transactionTypeRepository,
                IMapper mapper)
            {
                _transactionTypeRepository = transactionTypeRepository;
                _mapper = mapper;
            }

            public async Task<Result<IEnumerable<TransactionTypeDto>>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var query = await _transactionTypeRepository.GetAll();

                var banks = query.ToList();
                var bankDto = _mapper.Map<IEnumerable<TransactionTypeDto>>(banks);

                return Result<IEnumerable<TransactionTypeDto>>.Success(bankDto);
            }
        }
    }
}