
using Xunit;
using FluentValidation.TestHelper;
using MailBox.Validators.UserValidators;
using MailBox.Models.UserModels;

namespace UnitTests.ValidatorsTest.UserValidationsTests
{
    public class DeletedUserTest
    {
        [Fact]
        public void WhenDeletedUserIsOk_ShouldNotHaveAnyError()
        {
            var validator = new DeletedUserValidator();
            #region Init variables
            string address = "test@address.com";
            #endregion
            DeletedUser mailReadUpdate = new DeletedUser
            {
                Address = address
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
            result.ShouldNotHaveAnyValidationErrors();
            #endregion
        }

        [Fact]
        public void WhenDeletedUserAddressHaveNoAt_ShouldHaveError()
        {
            var validator = new DeletedUserValidator();
            #region Init variables
                string address = "testaddress.com";
            #endregion
            DeletedUser mailReadUpdate = new DeletedUser
            {
                Address = address
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Address);
            #endregion
        }

        [Fact]
        public void WhenDeletedUserAddressHaveNoDomain_ShouldHaveError()
        {
            var validator = new DeletedUserValidator();
            #region Init variables
                string address = "test@address";
            #endregion
            DeletedUser mailReadUpdate = new DeletedUser
            {
                Address = address
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Address);
            #endregion
        }
    }
}
