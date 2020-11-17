using System;
using Xunit;
using MailBox.Models;
using System.Collections.Generic;
using FluentValidation.TestHelper;
using System.Text;
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
            MailReadUpdate mailReadUpdate = new MailReadUpdate(mailId,read);
            var result = validator.TestValidate(mailReadUpdate);
            #region Tests
            result.ShouldNotHaveAnyValidationErrors();
            #endregion
        }
    }
}
