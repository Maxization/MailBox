
using FluentValidation;
using MailBox.Models.UserModels;

namespace MailBox.Validators.UserValidators
{
    public class UserRoleUpdateValidator : AbstractValidator<UserRoleUpdate>
    {
        public UserRoleUpdateValidator()
        {
            RuleFor(x => x.Address)
                .NotNull()
                .NotEmpty()
                .Matches(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");

            RuleFor(x => x.Role)
                .NotNull();
        }
    }
}
