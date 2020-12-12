
using FluentValidation.TestHelper;
using MailBox.Models.GroupModels;
using MailBox.Validators;
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
            string name = "testname";
            #endregion
            NewGroup newGroup = new NewGroup
            {
                Name = name
            };
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
            string name = "";
            #endregion
            NewGroup newGroup = new NewGroup
            {
                Name = name
            };
            var result = validator.TestValidate(newGroup);
            #region Tests
            result.ShouldHaveValidationErrorFor(x => x.Name);
            #endregion
        }
    }
}
