
using Xunit;
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
                GroupID = groupId
            };
            #region Tests
                Assert.NotNull(groupUpdate);
                Assert.Equal(groupUpdate.GroupID, groupId);
                Assert.Equal(groupUpdate.Name, name);
            #endregion
        }
    }
}
