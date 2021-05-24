using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DtoModels.TransactionType;
using Finance.Application.Services.Pagination;
using Finance.Application.Services.Pagination.Base;
using Finance.Application.Validations;
using Finance.Infrastructure.Data.Interfaces.Base;
using MediatR;

namespace Finance.Application.CQRS.Querries.TransactionType
{
    public class TransactionTypeList
    {
        public class Query : IRequest<Result<PagedList<TransactionTypeDto>>>
        {
            public TransactionTypeParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<TransactionTypeDto>>>
        {
            private readonly IMapper _mapper;
            private readonly IBaseRepository<Domain.Models.TransactionType> _transactionTypeRepository;

            public Handler(
                IBaseRepository<Domain.Models.TransactionType> transactionTypeRepository, IMapper mapper)
            {
                _transactionTypeRepository = transactionTypeRepository;
                _mapper = mapper;
            }

            public async Task<Result<PagedList<TransactionTypeDto>>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var query = await _transactionTypeRepository.GetAll();

                var transactions = await PagedList<Domain.Models.TransactionType>.CreateAsync(query,
                    request.Params.PageNumber, request.Params.PageSize);

                var transactionTypeDto = _mapper.Map<IEnumerable<TransactionTypeDto>>(transactions);

                var response = new PagedList<TransactionTypeDto>(transactionTypeDto
                    , transactions.CurrentPage,
                    transactions.TotalPages, transactions.PageSize, transactions.TotalCount);

                return Result<PagedList<TransactionTypeDto>>.Success(response);
            }
        }
    }
}