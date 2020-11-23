
﻿using FluentValidation;
using MailBox.Models.GroupModels;

namespace MailBox.Validators
{
    public class GroupMemberUpdateValidator : AbstractValidator<GroupMemberUpdate>
    {
        public GroupMemberUpdateValidator()
        {
            RuleFor(x => x.GroupID)
                .NotNull();
            
            RuleFor(x => x.GroupMemberAddress)
                .NotNull()
                .NotEmpty()
                .Matches(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
        }
    }
}
