using System;
using Xunit;
using MailBox.Models;
using System.Collections.Generic;
using MailBox.Models.GroupModels;
using MailBox.Models.UserModels;

namespace UnitTests.ModelsTest.GroupModelsTest
{
    public class GroupViewTest
    {
        [Fact]
        public void ConstructorTest()
        {
            GroupView groupView = null;
            #region Init variables
                int groupId = 0;
                string name = "testname";
                List<UserGlobalView> groupMembers = new List<UserGlobalView>();
            #endregion
            groupView = new GroupView(groupId, name, groupMembers);
            #region Tests
                Assert.NotNull(groupView);
                Assert.Equal(groupView.GroupId, groupId);
                Assert.Equal(groupView.Name, name);
                Assert.Equal(groupView.GroupMembers, groupMembers);
            #endregion
        }
    }
}
