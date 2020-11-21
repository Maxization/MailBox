using System;
using Xunit;
using MailBox.Models;
using System.Collections.Generic;
using MailBox.Models.GroupModels;

namespace UnitTests.ModelsTest.GroupModelsTest
{
    public class NewGroupTest
    {
        [Fact]
        public void ConstructorTest()
        {
            NewGroup newGroup = null;
            #region Init variables
                int ownerId = 0;
                string name = "testname";
            #endregion
            newGroup = new NewGroup(ownerId, name);
            #region Tests
                Assert.NotNull(newGroup);
                Assert.Equal(newGroup.OwnerId, ownerId);
                Assert.Equal(newGroup.Name, name);
            #endregion
        }
    }
}
