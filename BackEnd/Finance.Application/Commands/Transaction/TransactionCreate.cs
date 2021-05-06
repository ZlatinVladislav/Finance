using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DtoModels.Transaction;
using Finance.Application.Validations;
using Finance.Application.Validations.Transactions;
using Finance.Domain.Interfaces;
using Finance.Domain.Interfaces.Base;
using Finance.Domain.Models;
using FluentValidation;
using MediatR;

namespace Finance.Application.Commands.Transaction
{
    public class TransactionCreate
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
            private readonly IBaseRepository<TransactionType> _tansactionTypeRepository;
            private readonly IAppUserRepository _appUserRepository;
            private readonly IMapper _mapper;

            public Handler(IBaseRepository<Domain.Models.Transaction> genericRepository,IBaseRepository<TransactionType> tansactionTypeRepository, IMapper mapper,IAppUserRepository appUserRepository)
            {
                _transactionRepository = genericRepository;
                _tansactionTypeRepository = tansactionTypeRepository;
                _mapper = mapper;
                _appUserRepository = appUserRepository;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var transactionModel = _mapper.Map<Domain.Models.Transaction>(request.TransactionDto);
                var user= await _appUserRepository.GetUser();
                transactionModel.AppUser = user;
                await _transactionRepository.Post(transactionModel);
                var result = await _transactionRepository.SaveChanges();
                if (!result) return Result<Unit>.Failure("Failed to create transaction.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}