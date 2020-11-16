
using FluentValidation;
using MailBox.Models.GroupModels;

namespace MailBox.Validators
{
    public class NewGroupValidator : AbstractValidator<NewGroup>
    {
        public readonly int nameMaxLength = 100;
        public NewGroupValidator()
        {
            RuleFor(x => x.OwnerID)
                .NotNull();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(nameMaxLength);
        }
    }
}
