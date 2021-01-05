
using FluentValidation;
using MailBox.Models.UserModels;

namespace MailBox.Validators.UserValidators
{
    public class NewUserValidator : AbstractValidator<NewUser>
    {
        public readonly int nameMaxLength = 100;
        public readonly int surnameMaxLength = 100;
        public NewUserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(nameMaxLength);

            RuleFor(x => x.Surname)
                .NotEmpty()
                .MaximumLength(surnameMaxLength);

            RuleFor(x => x.Address)
                .NotEmpty()
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }
    }
}
