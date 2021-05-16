using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DtoModels.Transaction;
using Finance.Application.Services;
using Finance.Application.Validations;
using Finance.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Finance.Application.Querries.Transaction
{
    public class TransactionList
    {
        public class Query : IRequest<Result<PagedList<TransactionGetDto>>>
        {
            public TransactionParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<TransactionGetDto>>>
        {
            private readonly IMapper _mapper;
            private readonly IAppUserRepository _appUserRepository;
            private readonly ITransactionRepository _transactionRepository;

            public Handler(IAppUserRepository appUserRepository, ITransactionRepository transactionRepository,
                IMapper mapper)
            {
                _appUserRepository = appUserRepository;
                _transactionRepository = transactionRepository;
                _mapper = mapper;
            }

            public async Task<Result<PagedList<TransactionGetDto>>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var user = await _appUserRepository.GetUser();

                var query = await _transactionRepository.GetTransactionsForeignData(user.Id, request.Params.StartDate);

                if (request.Params.TransactionStatus != null)
                {
                    query = query.Where(s => s.TransactionStatus == request.Params.TransactionStatus);
                }

                var transactions = await PagedList<Domain.Models.Transaction>.CreateAsync(query,
                    request.Params.PageNumber, request.Params.PageSize);

                var transactionDto = _mapper.Map<IEnumerable<TransactionGetDto>>(transactions);

                var response = new PagedList<TransactionGetDto>(transactionDto
                    , transactions.CurrentPage,
                    transactions.TotalPages, transactions.PageSize, transactions.TotalCount);

                return Result<PagedList<TransactionGetDto>>.Success(response);
            }
        }
    }
}