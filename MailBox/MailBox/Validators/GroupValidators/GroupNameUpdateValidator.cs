
using FluentValidation;
using MailBox.Models.GroupModels;

namespace MailBox.Validators
{
    public class GroupNameUpdateValidator : AbstractValidator<GroupNameUpdate>
    {
        public readonly int nameMaxLength = 100;
        public GroupNameUpdateValidator()
        {
            RuleFor(x => x.GroupID)
                .NotNull();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(nameMaxLength);
        }
    }
}
