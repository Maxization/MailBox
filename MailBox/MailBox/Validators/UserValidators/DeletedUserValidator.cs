
using FluentValidation;
using MailBox.Models.UserModels;

namespace MailBox.Validators.UserValidators
{
    public class DeletedUserValidator : AbstractValidator<DeletedUser>
    {
        public DeletedUserValidator()
        {
            RuleFor(x => x.Address)
                .NotEmpty()
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }
    }
}
