using Finance.Application.DtoModels.Transaction;
using FluentValidation;

namespace Finance.Application.Validations.Transactions
{
    public class TransactionDtoValidation : AbstractValidator<TransactionDto>
    {
        public TransactionDtoValidation()
        {
            RuleFor(x => x.Money).NotEmpty();
            RuleFor(x => x.DateTransaction).NotEmpty();
            RuleFor(x => x.TransactionTypeId).NotEmpty();
        }
    }
}