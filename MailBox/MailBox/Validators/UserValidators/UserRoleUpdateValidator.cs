
using FluentValidation;
using MailBox.Models.UserModels;

namespace MailBox.Validators.UserValidators
{
    public class UserRoleUpdateValidator : AbstractValidator<UserRoleUpdate>
    {
        public UserRoleUpdateValidator()
        {
            RuleFor(x => x.Address)
                .NotEmpty()
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            RuleFor(x => x.RoleName)
                .NotEmpty()
                .Must(rolename => (rolename == "Admin" || rolename == "User" || rolename == "Banned" || rolename == "New")).WithMessage("The role should match to one of [User,Admin,Banned,New]");
        }
    }
}
