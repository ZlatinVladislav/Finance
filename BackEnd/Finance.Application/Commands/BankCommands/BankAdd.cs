using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DtoModels.Bank;
using Finance.Application.Validations;
using Finance.Application.Validations.Bank;
using Finance.Domain.Interfaces;
using Finance.Domain.Interfaces.Base;
using Finance.Domain.Models;
using FluentValidation;
using MediatR;

namespace Finance.Application.Commands.BankCommands
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
            private readonly IAppUserRepository _appUserRepository;
            private readonly IBaseRepository<Transaction> _transactionRepository;
            private readonly IBaseRepository<Bank> _bankRepository;
            private readonly IMapper _mapper;

            public Handler(IBaseRepository<Domain.Models.Transaction> transactionRepository,
                IBaseRepository<Bank> bankRepository, IMapper mapper,
                IAppUserRepository appUserRepository)
            {
                _transactionRepository = transactionRepository;
                _bankRepository = bankRepository;
                _mapper = mapper;
                _appUserRepository = appUserRepository;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var bankModel = _mapper.Map<Domain.Models.Bank>(request.BankDto);

                var transactionBank = new Bank
                {
                    Name = bankModel.Name,
                    Transactions = bankModel.Transactions
                };

                await _bankRepository.Post(transactionBank);
                var result = await _bankRepository.SaveChanges();
                if (!result) return Result<Unit>.Failure("Failed to create bank with transactions.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}