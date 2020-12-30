
﻿using FluentValidation;
using MailBox.Models.GroupModels;

namespace MailBox.Validators
{
    public class GroupMemberUpdateValidator : AbstractValidator<GroupMemberUpdate>
    {
        public GroupMemberUpdateValidator()
        {
            RuleFor(x => x.GroupMemberAddress)
                .NotEmpty().WithMessage("New member field cannot be empty.")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("New member is not correct email address.");
        }
    }
}
