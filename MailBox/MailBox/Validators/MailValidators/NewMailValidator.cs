
using FluentValidation;
using MailBox.Models.MailModels;
using System.Linq;

namespace MailBox.Validators.MailValidators
{
    public class NewMailValidator : AbstractValidator<NewMail>
    {
        public readonly int topicMaxLength = 100;
        public readonly int textMaxLength = 1000000000;
        public NewMailValidator()
        {
            RuleFor(x => x.CCRecipientsAddresses)
                .NotNull()
                .NotEmpty().When(x => (x.BCCRecipientsAddresses == null || x.BCCRecipientsAddresses.Count == 0)).WithMessage("You should select at least one CC or BCC recipient")
                .ForEach(x => x.Matches(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"));

            RuleFor(x => x.BCCRecipientsAddresses)
                .NotNull()
                .NotEmpty().When(x => (x.CCRecipientsAddresses == null || x.CCRecipientsAddresses.Count == 0)).WithMessage("You should select at least one CC or BCC recipient")
                .ForEach(x => x.Matches(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"));


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
        }
    }
}
