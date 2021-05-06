using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.Validations;
using Finance.Domain.Interfaces.Base;
using MediatR;

namespace Finance.Application.Commands.Transaction
{
    public class TransactionDelete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IBaseRepository<Domain.Models.Transaction> _transactionRepository;
            private readonly IMapper _mapper;

            public Handler(IBaseRepository<Domain.Models.Transaction> genericRepository, IMapper mapper)
            {
                _transactionRepository = genericRepository;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var transaction = await _transactionRepository.GetById(request.Id);
                if (transaction == null) return null;
                await _transactionRepository.Delete(request.Id);
                var result=await _transactionRepository.SaveChanges();

                if (!result) return Result<Unit>.Failure("Failed to delete transaction.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
