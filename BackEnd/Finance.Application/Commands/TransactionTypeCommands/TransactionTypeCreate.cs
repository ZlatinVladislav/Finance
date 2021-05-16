using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DtoModels.TransactionType;
using Finance.Application.Validations;
using Finance.Application.Validations.TransactionTypeValidators;
using Finance.Domain.Interfaces;
using Finance.Domain.Interfaces.Base;
using FluentValidation;
using MediatR;

namespace Finance.Application.Commands.TransactionTypeCommands
{
    public class TransactionTypeCreate
    {
        public class Command : IRequest<Result<Unit>>
        {
            public TransactionTypeDto TransactionTypeDto { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.TransactionTypeDto).SetValidator(new TransactionTypeDtoValidation());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IMapper _mapper;
            private readonly ITransactionTypeRepository _transactionTypeRepository;

            public Handler(IAppUserRepository appUserRepository, ITransactionTypeRepository transactionTypeRepository,
                IMapper mapper)
            {
                _transactionTypeRepository = transactionTypeRepository;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var transactionTypeModel = _mapper.Map<Domain.Models.TransactionType>(request.TransactionTypeDto);
                var tansactionType =
                    _transactionTypeRepository.GetTransactionTypeByName(transactionTypeModel.TransactionTypes);
                if (tansactionType.Result != null) return Result<Unit>.Failure("Transaction type with such name exist");
                await _transactionTypeRepository.Post(transactionTypeModel);
                var result = await _transactionTypeRepository.SaveChanges();
                if (!result) return Result<Unit>.Failure("Failed to create transaction.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}