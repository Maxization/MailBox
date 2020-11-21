using FluentValidation.TestHelper;
using MailBox.Models.GroupModels;
using MailBox.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class NewGroupValidatorTest
    {
        [Fact]
        public void WhenGroupNameIsCorrect_ShouldNotHaveAnyError()
        {
            var validator = new NewGroupValidator();
            #region Init variables
                int ownerId = 0;
                string name = "testname";
            #endregion
            NewGroup newGroup = new NewGroup(ownerId, name);
            var result = validator.TestValidate(newGroup);
            #region Tests
                result.ShouldNotHaveAnyValidationErrors();
            #endregion
        }

        [Fact]
        public void WhenGroupNameIsEmpty_ShouldHaveError()
        {
            var validator = new NewGroupValidator();
            #region Init variables
                int ownerId = 0;
                string name = "";
            #endregion
            NewGroup newGroup = new NewGroup(ownerId, name);
            var result = validator.TestValidate(newGroup);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Name);
            #endregion
        }
    }
}
