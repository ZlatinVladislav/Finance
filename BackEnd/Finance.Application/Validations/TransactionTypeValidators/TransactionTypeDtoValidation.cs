using Finance.Application.DtoModels.TransactionType;
using FluentValidation;

namespace Finance.Application.Validations.TransactionTypeValidators
{
    public class TransactionTypeDtoValidation : AbstractValidator<TransactionTypeDto>
    {
        public TransactionTypeDtoValidation()
        {
            RuleFor(x => x.TransactionType).NotEmpty();
        }
    }
}