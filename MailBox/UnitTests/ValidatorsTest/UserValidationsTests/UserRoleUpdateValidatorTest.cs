
using Xunit;
using FluentValidation.TestHelper;
using MailBox.Validators.UserValidators;
using MailBox.Models.UserModels;

namespace UnitTests.ValidatorsTest.UserValidationsTests
{
    public class UserRoleUpdateValidatorTest
    {
        [Fact]
        public void WhenUserRoleUpdateIsOk_ShouldNotHaveAnyError()
        {
            var validator = new UserRoleUpdateValidator();
            #region Init variables
            string address = "test@address.com";
            string roleName = "User";
            #endregion
            UserRoleUpdate mailReadUpdate = new UserRoleUpdate
            {
                Address = address,
                RoleName = roleName
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
            result.ShouldNotHaveAnyValidationErrors();
            #endregion
        }

        [Fact]
        public void WhenUserRoleUpdateAddressHaveNoAt_ShouldHaveError()
        {
            var validator = new UserRoleUpdateValidator();
            #region Init variables
            string address = "testaddress.com";
            string roleName = "User";
            #endregion
            UserRoleUpdate mailReadUpdate = new UserRoleUpdate
            {
                Address = address,
                RoleName = roleName
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
            result.ShouldHaveValidationErrorFor(x => x.Address);
            #endregion
        }

        [Fact]
        public void WhenUserRoleUpdateAddressHaveNoDomain_ShouldHaveError()
        {
            var validator = new UserRoleUpdateValidator();
            #region Init variables
            string address = "test@address";
            string roleName = "User";
            #endregion
            UserRoleUpdate mailReadUpdate = new UserRoleUpdate
            {
                Address = address,
                RoleName = roleName
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
            result.ShouldHaveValidationErrorFor(x => x.Address);
            #endregion
        }

        [Fact]
        public void WhenUserRoleUpdateHaveNoRole_ShouldHaveError()
        {
            var validator = new UserRoleUpdateValidator();
            #region Init variables
            string address = "test@address.com";
            string rolename = "";
            #endregion
            UserRoleUpdate mailReadUpdate = new UserRoleUpdate
            {
                Address = address,
                RoleName = rolename
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
            result.ShouldHaveValidationErrorFor(x => x.RoleName);
            #endregion
        }

        [Fact]
        public void WhenUserRoleUpdateHaveWrongRole_ShouldHaveError()
        {
            var validator = new UserRoleUpdateValidator();
            #region Init variables
            string address = "test@address.com";
            string rolename = "WrongRole";
            #endregion
            UserRoleUpdate mailReadUpdate = new UserRoleUpdate
            {
                Address = address,
                RoleName = rolename
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
            result.ShouldHaveValidationErrorFor(x => x.RoleName);
            #endregion
        }
    }
}
