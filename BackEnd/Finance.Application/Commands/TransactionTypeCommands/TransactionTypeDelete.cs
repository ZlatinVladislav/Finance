using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.Validations;
using Finance.Domain.Interfaces.Base;
using MediatR;

namespace Finance.Application.Commands.TransactionTypeCommands
{
    public class TransactionTypeDelete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IBaseRepository<Domain.Models.TransactionType> _transactionTypeRepository;

            public Handler(IBaseRepository<Domain.Models.TransactionType> genericRepository)
            {
                _transactionTypeRepository = genericRepository;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var transaction = await _transactionTypeRepository.GetById(request.Id);
                if (transaction == null) return null;
                await _transactionTypeRepository.Delete(request.Id);
                var result=await _transactionTypeRepository.SaveChanges();

                if (!result) return Result<Unit>.Failure("Failed to delete transaction.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}