
using FluentValidation;
using MailBox.Models.MailModels;

namespace MailBox.Validators.MailValidators
{
    public class NewMailValidator : AbstractValidator<NewMail>
    {
        public readonly int topicMaxLength = 100;
        public readonly int textMaxLength = 1000000;
        public readonly string emailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        public NewMailValidator()
        {
            RuleFor(x => x.CCRecipientsAddresses)
                .NotNull()
                .NotEmpty().When(x => (x.BCCRecipientsAddresses == null || x.BCCRecipientsAddresses.Count == 0)).WithMessage("You should select at least one CC or BCC recipient.")
                .ForEach(x => x.Matches(emailRegex).WithMessage("Incorrect email."));

            RuleFor(x => x.BCCRecipientsAddresses)
                .NotNull()
                .NotEmpty().When(x => (x.CCRecipientsAddresses == null || x.CCRecipientsAddresses.Count == 0)).WithMessage("You should select at least one CC or BCC recipient.")
                .ForEach(x => x.Matches(emailRegex).WithMessage("Incorrect email."));

            RuleFor(x => x.Topic)
                .NotNull()
                .NotEmpty()
                .MaximumLength(topicMaxLength);

            RuleFor(x => x.Text)
                .NotNull()
                .NotEmpty()
                .MaximumLength(textMaxLength);
        }
    }
}
