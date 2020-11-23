using FluentValidation;
using MailBox.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Validators.UserValidators
{
    public class NewUserValidator : AbstractValidator<NewUser>
    {
        public readonly int nameMaxLength = 100;
        public readonly int surnameMaxLength = 100;
        public NewUserValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(nameMaxLength);

            RuleFor(x => x.Surname)
                .NotNull()
                .NotEmpty()
                .MaximumLength(surnameMaxLength);

            RuleFor(x => x.Address)
                .NotNull()
                .NotEmpty()
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }
    }
}
