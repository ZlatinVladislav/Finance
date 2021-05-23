using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DtoModels.Bank;
using Finance.Application.DtoModels.Transaction;
using Finance.Application.Services;
using Finance.Application.Validations;
using Finance.Domain.Interfaces;
using MediatR;

namespace Finance.Application.Querries.Bank
{
    public class BankList
    {
        public class Query : IRequest<Result<PagedList<BankDto>>>
        {
            public PagingParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<BankDto>>>
        {
            private readonly IMapper _mapper;
            private readonly IAppUserRepository _appUserRepository;
            private readonly IBankRepository _bankRepository;


            public Handler(IAppUserRepository appUserRepository, IBankRepository bankRepository,
                IMapper mapper)
            {
                _appUserRepository = appUserRepository;
                _bankRepository = bankRepository;
                _mapper = mapper;
            }

            public async Task<Result<PagedList<BankDto>>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var query = await _bankRepository.GetAll();

                var banks = await PagedList<Domain.Models.Bank>.CreateAsync(query,
                    request.Params.PageNumber, request.Params.PageSize);

                var bankDto = _mapper.Map<IEnumerable<BankDto>>(banks);

                var response = new PagedList<BankDto>(bankDto
                    , banks.CurrentPage,
                    banks.TotalPages, banks.PageSize, banks.TotalCount);

                return Result<PagedList<BankDto>>.Success(response);
            }
        }
    }
}