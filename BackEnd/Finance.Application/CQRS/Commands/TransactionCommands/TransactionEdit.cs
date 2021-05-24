using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DtoModels.Transaction;
using Finance.Application.Validations;
using Finance.Application.Validations.TransactionValidators;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Interfaces;
using Finance.Infrastructure.Data.Interfaces.Base;
using FluentValidation;
using MediatR;

namespace Finance.Application.CQRS.Commands.TransactionCommands
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
            private readonly IAppUserRepository _appUserRepository;
            private readonly IMapper _mapper;
            private readonly IBaseRepository<Transaction> _transactionRepository;

            public Handler(IBaseRepository<Transaction> genericRepository, IMapper mapper,
                IAppUserRepository appUserRepository)
            {
                _transactionRepository = genericRepository;
                _mapper = mapper;
                _appUserRepository = appUserRepository;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var transactionModel = _mapper.Map<Transaction>(request.TransactionDto);
                var user = await _appUserRepository.GetUser();
                transactionModel.AppUser = user;
                await _transactionRepository.Put(transactionModel);
                var result = await _transactionRepository.SaveChanges();
                return !result
                    ? Result<Unit>.Failure("Failed to update transaction.")
                    : Result<Unit>.Success(Unit.Value);
            }
        }
    }
}