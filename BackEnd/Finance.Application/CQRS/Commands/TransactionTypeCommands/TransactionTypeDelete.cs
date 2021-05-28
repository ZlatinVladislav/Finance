using System;
using System.Threading;
using System.Threading.Tasks;
using Finance.Application.Validations;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Interfaces.Base;
using MediatR;

namespace Finance.Application.CQRS.Commands.TransactionTypeCommands
{
    public class TransactionTypeDelete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IBaseRepository<TransactionType> _transactionTypeRepository;

            public Handler(IBaseRepository<TransactionType> genericRepository)
            {
                _transactionTypeRepository = genericRepository;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var transaction = await _transactionTypeRepository.GetById(request.Id);
                if (transaction == null) return null;
                await _transactionTypeRepository.Delete(request.Id);
                var result = await _transactionTypeRepository.SaveChanges();

                return !result
                    ? Result<Unit>.Failure("Failed to delete transaction.")
                    : Result<Unit>.Success(Unit.Value);
            }
        }
    }
}