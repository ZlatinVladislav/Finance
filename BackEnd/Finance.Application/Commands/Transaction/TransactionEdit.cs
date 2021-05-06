using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DtoModels.Transaction;
using Finance.Application.Validations;
using Finance.Application.Validations.Transactions;
using Finance.Domain.Interfaces.Base;
using FluentValidation;
using MediatR;

namespace Finance.Application.Commands.Transaction
{
    public class TransactionEdit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public TransactionDto TransactionDto { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.TransactionDto).SetValidator(new TransactionDtoValidation());
            }
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
                var transactionModel = _mapper.Map<Domain.Models.Transaction>(request.TransactionDto);                  
                var transaction = await _transactionRepository.GetById(request.TransactionDto.Id);
                if (transaction == null) return null;
                await _transactionRepository.Put(transactionModel);
                var result= await _transactionRepository.SaveChanges();
                if (!result) return Result<Unit>.Failure("Failed to update transaction.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
