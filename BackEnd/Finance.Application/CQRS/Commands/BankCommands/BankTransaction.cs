using System;
using System.Threading;
using System.Threading.Tasks;
using Finance.Application.Validations;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Interfaces;
using Finance.Infrastructure.Data.Interfaces.Base;
using FluentValidation;
using MediatR;

namespace Finance.Application.CQRS.Commands.BankCommands
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
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IBaseRepository<Bank> _bankRepository;
            private readonly IBankTransactionRepository _bankTransactionRepository;
            private readonly IBaseRepository<Transaction> _transactionRepository;

            public Handler(IBaseRepository<Transaction> transactionRepository,
                IBaseRepository<Bank> bankRepository, IBankTransactionRepository bankTransactionRepository)
            {
                _transactionRepository = transactionRepository;
                _bankRepository = bankRepository;
                _bankTransactionRepository = bankTransactionRepository;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _bankTransactionRepository.CheckIfExist(request.BankId, request.TransactionId))
                    return Result<Unit>.Failure("Transaction is already assigned to this bank");
                
                var bank = await _bankRepository.GetById(request.BankId);
                var transaction = await _transactionRepository.GetById(request.TransactionId);

                if (bank == null || transaction == null)
                    return null;

                var bankTransactionModel = new BankTransaction
                    {BankId = request.BankId, TransactionId = request.TransactionId};

                await _bankTransactionRepository.Post(bankTransactionModel);
                var result = await _bankRepository.SaveChanges();
                return !result
                    ? Result<Unit>.Failure("Failed to assign bank to transaction.")
                    : Result<Unit>.Success(Unit.Value);
            }
        }
    }
}