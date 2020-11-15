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
            // TODO: Check if exist in the database
            RuleFor(x => x.GroupMemberAddress)
                .NotNull()
                .NotEmpty()
                .Matches(@"^[\w -\.]+@([\w -] +\.)+[\w -]{2,4}$");

        }
    }
}
