using Xunit;
using FluentValidation.TestHelper;
using MailBox.Validators.UserValidators;
using MailBox.Models.UserModels;
using System.Text;

namespace UnitTests.ValidatorsTest.UserValidationsTests
{
    public class UserEnableUpdateValidatorTest
    {
        [Fact]
        public void WhenUserEnableUpdateIsOk_ShouldNotHaveAnyError()
        {
            var validator = new UserEnableUpdateValidator();
            #region Init variables
                string address = "test@address.com";
                bool enable = true;
            #endregion
            UserEnableUpdate mailReadUpdate = new UserEnableUpdate
            {
                Address = address,
                Enable = enable
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
                result.ShouldNotHaveAnyValidationErrors();
            #endregion
        }

        [Fact]
        public void WhenUserEnableUpdateAddressHaveNoAt_ShouldHaveError()
        {
            var validator = new UserEnableUpdateValidator();
            #region Init variables
                string address = "testaddress.com";
                bool enable = true;
            #endregion
            UserEnableUpdate mailReadUpdate = new UserEnableUpdate
            {
                Address = address,
                Enable = enable
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Address);
            #endregion
        }

        [Fact]
        public void WhenUserEnableUpdateAddressHaveNoDomain_ShouldHaveError()
        {
            var validator = new UserEnableUpdateValidator();
            #region Init variables
                string address = "test@address";
                bool enable = true;
            #endregion
            UserEnableUpdate mailReadUpdate = new UserEnableUpdate
            {
                Address = address,
                Enable = enable
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Address);
            #endregion
        }
    }
}
