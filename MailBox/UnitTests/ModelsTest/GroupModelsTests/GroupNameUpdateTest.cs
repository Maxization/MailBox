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
            #region Init variables
                int groupId = 0;
                string name = "testname";
            #endregion
            GroupNameUpdate groupUpdate = new GroupNameUpdate
            {
                Name = name,
                GroupId = groupId
            };
            #region Tests
                Assert.NotNull(groupUpdate);
                Assert.Equal(groupUpdate.GroupId, groupId);
                Assert.Equal(groupUpdate.Name, name);
            #endregion
        }
    }
}
