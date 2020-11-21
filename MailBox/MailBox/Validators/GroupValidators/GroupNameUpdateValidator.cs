using FluentValidation;
using MailBox.Models.GroupModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Validators
{
    public class GroupNameUpdateValidator : AbstractValidator<GroupNameUpdate>
    {
        public readonly int nameMaxLength = 30;
        public GroupNameUpdateValidator()
        {
            RuleFor(x => x.GroupId)
                .NotNull();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(nameMaxLength);
        }
    }
}
