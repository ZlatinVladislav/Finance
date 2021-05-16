using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.Validations;
using Finance.Domain.Interfaces;
using MediatR;

namespace Finance.Application.Commands.TransactionCommands
{
    public class TransactionCancel
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly ITransactionRepository _transactionRepository;
            private readonly IMapper _mapper;
            private readonly IAppUserRepository _appUserRepository;

            public Handler(ITransactionRepository transactionRepository, IMapper mapper,IAppUserRepository appUserRepository)
            {
                _transactionRepository = transactionRepository;
                _mapper = mapper;
                _appUserRepository = appUserRepository;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var transaction = await _transactionRepository.GetTransactionsForeignDataById(request.Id);
                if (transaction == null) return null;
                
                var user= await _appUserRepository.GetUser();
                if (user == null) return null;

                var transactionAuth = transaction.AppUser.UserName == user.UserName;

                if (transactionAuth)
                {
                    transaction.IsCanceled = !transaction.IsCanceled;
                }
                
                var result=await _transactionRepository.SaveChanges();

                return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to cancel transaction");
            }
        }
    }
}
