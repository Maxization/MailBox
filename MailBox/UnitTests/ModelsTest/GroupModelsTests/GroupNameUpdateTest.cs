using System;
using Xunit;
using MailBox.Models;
using System.Collections.Generic;
using MailBox.Models.GroupModels;

namespace UnitTests.ModelsTest.GroupModelsTest
{
    public class GroupNameUpdateTest
    {
        [Fact]
        public void ConstructorTest()
        {
            GroupNameUpdate groupUpdate = null;
            #region Init variables
                int groupId = 0;
                string name = "testname";
            #endregion
            groupUpdate = new GroupNameUpdate(groupId, name);
            #region Tests
                Assert.NotNull(groupUpdate);
                Assert.Equal(groupUpdate.GroupId, groupId);
                Assert.Equal(groupUpdate.Name, name);
            #endregion
        }
    }
}
