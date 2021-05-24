using Finance.Application.DtoModels.User;
using FluentValidation;

namespace Finance.Application.Validations.UserValidators
{
    public class UserDescriptionValidation : AbstractValidator<UserDescriptionDto>
    {
        public UserDescriptionValidation()
        {
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}