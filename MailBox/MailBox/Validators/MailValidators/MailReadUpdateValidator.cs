using FluentValidation;
using MailBox.Models.MailModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Validators.MailValidators
{
    public class MailReadUpdateValidator : AbstractValidator<MailReadUpdate>
    {
        public MailReadUpdateValidator()
        {
            RuleFor(x => x.MailId)
                .NotNull();

            RuleFor(x => x.Read)
                .NotNull();
        }
    }
}
