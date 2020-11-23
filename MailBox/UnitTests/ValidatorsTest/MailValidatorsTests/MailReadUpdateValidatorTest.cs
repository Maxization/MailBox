using Xunit;
using FluentValidation.TestHelper;
using MailBox.Validators.MailValidators;
using MailBox.Models.MailModels;

namespace UnitTests.ValidatorsTest.MailValidatorsTests
{
    public class MailReadUpdateValidatorTest
    {
        [Fact]
        public void WhenReadUpdateIsOk_ShouldNotHaveAnyError()
        {
            var validator = new MailReadUpdateValidator();
            #region Init variables
                int mailId = 0;
                bool read = true;
            #endregion
            MailReadUpdate mailReadUpdate = new MailReadUpdate
            {
                MailID = mailId,
                Read = read
            };
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
                result.ShouldNotHaveAnyValidationErrors();
            #endregion
        }
    }
}
