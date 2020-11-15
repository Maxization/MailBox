using FluentValidation;
using MailBox.Models.GroupModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Validators
{
    public class GroupMemberUpdateValidator : AbstractValidator<GroupMemberUpdate>
    {
        public GroupMemberUpdateValidator()
        {
            RuleFor(x => x.GroupId)
                .NotNull();
            
            RuleFor(x => x.GroupMemberAddress)
                .NotNull()
                .NotEmpty()
                .Matches(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");

        }
    }
}
