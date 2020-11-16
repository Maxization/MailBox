
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
                .Matches(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");

            RuleFor(x => x.Enable)
                .NotNull();
        }
    }
}
