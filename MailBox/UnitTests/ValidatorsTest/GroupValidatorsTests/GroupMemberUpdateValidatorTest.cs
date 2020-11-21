using System;
using Xunit;
using MailBox;
using MailBox.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MailBox.Models;
using System.Collections.Generic;
using MailBox.Models.UserModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using FluentValidation.TestHelper;
using System.Text;
using MailBox.Validators.MailValidators;
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
                int groupId = 0;
                string address = "wrogaddress.pl";
            #endregion
            GroupMemberUpdate groupMemberUpdate = new GroupMemberUpdate(groupId, address);
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
                int groupId = 0;
                string address = "wroga@ddresspl";
            #endregion
            GroupMemberUpdate groupMemberUpdate = new GroupMemberUpdate(groupId, address);
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
                int groupId = 0;
                string address = "test@address.pl";
            #endregion
            GroupMemberUpdate groupMemberUpdate = new GroupMemberUpdate(groupId, address);
            var result = validator.TestValidate(groupMemberUpdate);
            #region Tests
                result.ShouldNotHaveAnyValidationErrors();
            #endregion
        }
    }
}
