
using FluentValidation;
using MailBox.Models.MailModels;

namespace MailBox.Validators.MailValidators
{
    public class NewMailResponseValidator : AbstractValidator<NewMailResponse>
    {
        public readonly int topicMaxLength = 100;
        public readonly int textMaxLength = 1000000000;
        public NewMailResponseValidator()
        {
            RuleFor(x => x.Topic)
                .NotNull()
                .NotEmpty()
                .MaximumLength(topicMaxLength);

            RuleFor(x => x.Text)
                .NotNull()
                .NotEmpty()
                .MaximumLength(textMaxLength);

            RuleFor(x => x.Date)
                .NotNull();

            RuleFor(x => x.MailReplyID)
                .NotNull();
        }
    }
}
