using System;
using System.Threading;
using System.Threading.Tasks;
using Finance.Application.Validations;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Interfaces.Base;
using MediatR;

namespace Finance.Application.CQRS.Commands.TransactionCommands
{
    public class TransactionDelete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IBaseRepository<Transaction> _transactionRepository;

            public Handler(IBaseRepository<Transaction> genericRepository)
            {
                _transactionRepository = genericRepository;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var transaction = await _transactionRepository.GetById(request.Id);
                if (transaction == null) return null;
                await _transactionRepository.Delete(request.Id);
                var result = await _transactionRepository.SaveChanges();

                return !result
                    ? Result<Unit>.Failure("Failed to delete transaction.")
                    : Result<Unit>.Success(Unit.Value);
            }
        }
    }
}