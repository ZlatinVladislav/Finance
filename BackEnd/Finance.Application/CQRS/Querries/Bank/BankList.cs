using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DtoModels.Bank;
using Finance.Application.Validations;
using Finance.Infrastructure.Data.Interfaces;
using MediatR;

namespace Finance.Application.CQRS.Querries.Bank
{
    public class BankList
    {
        public class Query : IRequest<Result<IEnumerable<BankDto>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<IEnumerable<BankDto>>>
        {
            private readonly IBankRepository _bankRepository;
            private readonly IMapper _mapper;


            public Handler(IBankRepository bankRepository,
                IMapper mapper)
            {
                _bankRepository = bankRepository;
                _mapper = mapper;
            }

            public async Task<Result<IEnumerable<BankDto>>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var query = await _bankRepository.GetAll();

                var banks = query.ToList();
                var bankDto = _mapper.Map<IEnumerable<BankDto>>(banks);

                return Result<IEnumerable<BankDto>>.Success(bankDto);
            }
        }
    }
}