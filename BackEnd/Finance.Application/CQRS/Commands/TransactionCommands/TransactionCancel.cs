using System;
using System.Threading;
using System.Threading.Tasks;
using Finance.Application.Validations;
using Finance.Infrastructure.Data.Interfaces;
using MediatR;

namespace Finance.Application.CQRS.Commands.TransactionCommands
{
    public class TransactionCancel
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IAppUserRepository _appUserRepository;
            private readonly ITransactionRepository _transactionRepository;

            public Handler(ITransactionRepository transactionRepository,
                IAppUserRepository appUserRepository)
            {
                _transactionRepository = transactionRepository;
                _appUserRepository = appUserRepository;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var transaction = await _transactionRepository.GetTransactionsForeignDataById(request.Id);
                if (transaction == null) return null;

                var user = await _appUserRepository.GetUser();
                if (user == null) return null;

                var transactionAuth = transaction.AppUser.UserName == user.UserName;

                if (transactionAuth) transaction.IsCanceled = !transaction.IsCanceled;

                var result = await _transactionRepository.SaveChanges();

                return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to cancel transaction");
            }
        }
    }
}