using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DtoModels.Bank;
using Finance.Application.Validations;
using Finance.Application.Validations.Bank;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Interfaces.Base;
using FluentValidation;
using MediatR;

namespace Finance.Application.CQRS.Commands.BankCommands
{
    public class BankAdd
    {
        public class Command : IRequest<Result<Unit>>
        {
            public BankDto BankDto { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.BankDto).SetValidator(new BankDtoValidation());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IBaseRepository<Bank> _bankRepository;
            private readonly IMapper _mapper;

            public Handler(
                IBaseRepository<Bank> bankRepository, IMapper mapper
            )
            {
                _bankRepository = bankRepository;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var bankModel = _mapper.Map<Bank>(request.BankDto);

                var transactionBank = new Bank
                {
                    Name = bankModel.Name,
                    Transactions = bankModel.Transactions
                };

                await _bankRepository.Post(transactionBank);
                var result = await _bankRepository.SaveChanges();
                return !result
                    ? Result<Unit>.Failure("Failed to create bank with transactions.")
                    : Result<Unit>.Success(Unit.Value);
            }
        }
    }
}