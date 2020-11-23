using FluentValidation.TestHelper;
using MailBox.Models.GroupModels;
using MailBox.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class GroupNameUpdateValidatorTest
    {
        [Fact]
        public void WhenGroupNameIsCorrect_ShouldNotHaveAnyError()
        {
            var validator = new GroupNameUpdateValidator();
            #region Init variables
                int groupId = 0;
                string name = "testname";
            #endregion
            GroupNameUpdate groupNameUpdate = new GroupNameUpdate
            {
                GroupID = groupId,
                Name = name
            };
            var result = validator.TestValidate(groupNameUpdate);
            #region Tests
                result.ShouldNotHaveAnyValidationErrors();
            #endregion
        }

        [Fact]
        public void WhenGroupNameIsEmpty_ShouldHaveError()
        {
            var validator = new GroupNameUpdateValidator();
            #region Init variables
                int groupId = 0;
                string name = "";
            #endregion
            GroupNameUpdate groupNameUpdate = new GroupNameUpdate
            {
                GroupID = groupId,
                Name = name
            };
            var result = validator.TestValidate(groupNameUpdate);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Name);
            #endregion
        }
    }
}
