
using Xunit;
using FluentValidation.TestHelper;
using MailBox.Validators.UserValidators;
using MailBox.Models.UserModels;
using System.Text;
using MailBox.Models;

namespace UnitTests.ValidatorsTest.UserValidationsTests
{
    public class UserRoleUpdateValidatorTest
    {
        [Fact]
        public void WhenUserEnableUpdateIsOk_ShouldNotHaveAnyError()
        {
            var validator = new UserRoleUpdateValidator();
            #region Init variables
                string address = "test@address.com";
                Role role = new Role { Name = "testname" };
            #endregion
            UserRoleUpdate mailReadUpdate = new UserRoleUpdate
            {
                Address = address,
                Role = role
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
                result.ShouldNotHaveAnyValidationErrors();
            #endregion
        }

        [Fact]
        public void WhenUserEnableUpdateAddressHaveNoAt_ShouldHaveError()
        {
            var validator = new UserRoleUpdateValidator();
            #region Init variables
                string address = "testaddress.com";
                Role role = new Role { Name = "testname"};
            #endregion
            UserRoleUpdate mailReadUpdate = new UserRoleUpdate
            {
                Address = address,
                Role = role
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Address);
            #endregion
        }

        [Fact]
        public void WhenUserEnableUpdateAddressHaveNoDomain_ShouldHaveError()
        {
            var validator = new UserRoleUpdateValidator();
            #region Init variables
                string address = "test@address";
                Role role = new Role { Name = "testname" };
            #endregion
            UserRoleUpdate mailReadUpdate = new UserRoleUpdate
            {
                Address = address,
                Role = role
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Address);
            #endregion
        }

        [Fact]
        public void WhenUserEnableUpdateHaveNoRole_ShouldHaveError()
        {
            var validator = new UserRoleUpdateValidator();
            #region Init variables
                string address = "test@address.com";
                Role role = null;
            #endregion
            UserRoleUpdate mailReadUpdate = new UserRoleUpdate
            {
                Address = address,
                Role = role
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Role);
            #endregion
        }
    }
}
