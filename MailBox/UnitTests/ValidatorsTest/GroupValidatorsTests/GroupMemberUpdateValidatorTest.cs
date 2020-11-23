
using Xunit;
using FluentValidation.TestHelper;
using MailBox.Validators;
using MailBox.Models.GroupModels;

namespace UnitTests
{
    public class GroupMemberUpdateValidatorTest
    {
        [Fact]
        public void WhenGroupMemberHaveAddressWithoutAt_ShouldHaveError()
        {
            var validator = new GroupMemberUpdateValidator();
            #region Init variables
                int groupID = 0;
                string address = "wrogaddress.pl";
            #endregion
            GroupMemberUpdate groupMemberUpdate = new GroupMemberUpdate
            {
                GroupID = groupID,
                GroupMemberAddress = address
            };
            var result = validator.TestValidate(groupMemberUpdate);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.GroupMemberAddress);
            #endregion
        }

        [Fact]
        public void WhenGroupMemberHaveAddressWithoutDomain_ShouldHaveError()
        {
            var validator = new GroupMemberUpdateValidator();
            #region Init variables
                int groupID = 0;
                string address = "wroga@ddresspl";
            #endregion
            GroupMemberUpdate groupMemberUpdate = new GroupMemberUpdate
            {
                GroupID = groupID,
                GroupMemberAddress = address
            };
            var result = validator.TestValidate(groupMemberUpdate);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.GroupMemberAddress);
            #endregion
        }

        [Fact]
        public void WhenGroupMemberHaveCorrectAddress_ShouldNotHaveAnyError()
        {
            var validator = new GroupMemberUpdateValidator();
            #region Init variables
                int groupID = 0;
                string address = "test@address.pl";
            #endregion
            GroupMemberUpdate groupMemberUpdate = new GroupMemberUpdate
            {
                GroupID = groupID,
                GroupMemberAddress = address
            };
            var result = validator.TestValidate(groupMemberUpdate);
            #region Tests
                result.ShouldNotHaveAnyValidationErrors();
            #endregion
        }
    }
}
