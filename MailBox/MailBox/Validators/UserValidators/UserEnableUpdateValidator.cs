
using FluentValidation;
using MailBox.Models.UserModels;

namespace MailBox.Validators.UserValidators
{
    public class UserEnableUpdateValidator : AbstractValidator<UserEnableUpdate>
    {
        public UserEnableUpdateValidator()
        {
            RuleFor(x => x.Address)
                .NotNull()
                .NotEmpty()
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            RuleFor(x => x.Enable)
                .NotNull();
        }
    }
}
