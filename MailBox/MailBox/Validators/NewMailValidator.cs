using FluentValidation;
using MailBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Validators
{
    public class NewMailValidator : AbstractValidator<NewMail>
    {
        public readonly int  topicMaxLength = 100;
        public readonly int  textMaxLength = 1000000000;
        public NewMailValidator()
        {
            RuleFor(x => x.CCRecipients)
                .NotEmpty().When(x => (x.BCCRecipients == null || x.BCCRecipients.Count == 0)).WithMessage("You should select at least one CC or BCC recipient");
            
            RuleFor(x => x.BCCRecipients)
                .NotEmpty().When(x => (x.CCRecipients == null || x.CCRecipients.Count == 0)).WithMessage("You should select at least one CC or BCC recipient");

            RuleFor(x => x.Topic)
                .NotEmpty()
                .MaximumLength(topicMaxLength);

            RuleFor(x => x.Text)
                .NotEmpty()
                .MaximumLength(textMaxLength); //max string length 1073741791 should we keep Text as a string?
        }
    }
}
