
﻿using FluentValidation;
using MailBox.Models.GroupModels;

namespace MailBox.Validators
{
    public class GroupMemberUpdateValidator : AbstractValidator<GroupMemberUpdate>
    {
        public GroupMemberUpdateValidator()
        {
            RuleFor(x => x.GroupMemberAddress)
                .NotNull()
                .NotEmpty()
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }
    }
}
