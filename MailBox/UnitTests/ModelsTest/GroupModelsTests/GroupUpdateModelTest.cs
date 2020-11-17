using System;
using Xunit;
using MailBox.Models;
using System.Collections.Generic;
using MailBox.Models.GroupModels;

namespace UnitTests.ModelsTest.GroupModelsTest
{
    public class GroupMemberUpdateTest
    {
        [Fact]
        public void ConstructorTest()
        {
            GroupMemberUpdate groupUpdate = null;
            #region Init variables
                int groupId = 0;
                string groupMemberAddress = "test@address.com";
            #endregion
            groupUpdate = new GroupMemberUpdate(groupId, groupMemberAddress);
            #region Tests
                Assert.NotNull(groupUpdate);
                Assert.Equal(groupUpdate.GroupId, groupId);
                Assert.Equal(groupUpdate.GroupMemberAddress, groupMemberAddress);
            #endregion
        }
    }
}
