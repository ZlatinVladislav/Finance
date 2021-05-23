using Finance.Application.DtoModels.Bank;
using FluentValidation;

namespace Finance.Application.Validations.Bank
{
    public class BankDtoValidation : AbstractValidator<BankDto>
    {
        public BankDtoValidation()
        {
            // RuleFor(x => x.Name).NotEmpty();
        }
    }
}