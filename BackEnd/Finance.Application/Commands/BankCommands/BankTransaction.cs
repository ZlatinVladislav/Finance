using System;
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
    public class BankTransactionAdd
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid TransactionId { get; set; }
            public Guid BankId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                // RuleFor(x => x.BankTransactionDto).SetValidator(new BankTransactionDtoValidation());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IAppUserRepository _appUserRepository;
            private readonly IBaseRepository<Transaction> _transactionRepository;
            private readonly IBaseRepository<Bank> _bankRepository;
            private readonly IBaseRepository<BankTransaction> _bankTransactionRepository;
            private readonly IMapper _mapper;

            public Handler(IBaseRepository<Domain.Models.Transaction> transactionRepository,
                IBaseRepository<Bank> bankRepository,   IBaseRepository<BankTransaction> bankTransactionRepository, IMapper mapper,
                IAppUserRepository appUserRepository)
            {
                _transactionRepository = transactionRepository;
                _bankRepository = bankRepository;
                _bankTransactionRepository = bankTransactionRepository;
                _mapper = mapper;
                _appUserRepository = appUserRepository;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            { 
                var bank = await _bankRepository.GetById(request.BankId);
                var transaction = await _transactionRepository.GetById(request.TransactionId);
                
                if (bank == null || transaction == null)
                    return null;

                var bankTransactionModel = new BankTransaction
                    {BankId = request.BankId, TransactionId = request.TransactionId};
                
                await _bankTransactionRepository.Post(bankTransactionModel);
                var result = await _bankRepository.SaveChanges();
                if (!result) return Result<Unit>.Failure("Failed to assign bank to transaction.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}