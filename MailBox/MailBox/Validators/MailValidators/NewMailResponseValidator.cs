using FluentValidation;
using MailBox.Models.MailModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Validators.MailValidators
{
    public class NewMailResponseValidator : AbstractValidator<NewMailResponse>
    {
        public readonly int topicMaxLength = 100;
        public readonly int textMaxLength = 1000000;
        public NewMailResponseValidator()
        {
            RuleFor(x => x.Topic)
                .NotNull()
                .NotEmpty()
                .MaximumLength(topicMaxLength);

            RuleFor(x => x.Text)
                .NotNull()
                .MaximumLength(textMaxLength);

            RuleFor(x => x.Date)
                .NotNull();

            RuleFor(x => x.MailReplyId)
                .NotNull();
        }
    }
}
