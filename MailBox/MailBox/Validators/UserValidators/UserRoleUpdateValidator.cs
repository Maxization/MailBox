
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
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            RuleFor(x => x.Role)
                .NotNull();
        }
    }
}
