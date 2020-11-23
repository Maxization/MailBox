
﻿using FluentValidation;
using MailBox.Models.MailModels;

namespace MailBox.Validators.MailValidators
{
    public class MailReadUpdateValidator : AbstractValidator<MailReadUpdate>
    {
        public MailReadUpdateValidator()
        {
            RuleFor(x => x.MailID)
                .NotNull();

            RuleFor(x => x.Read)
                .NotNull();
        }
    }
}
