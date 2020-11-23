
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
                int groupID = 0;
                string name = "testname";
            #endregion
            GroupNameUpdate groupUpdate = new GroupNameUpdate
            {
                Name = name,
                GroupID = groupID
            };
            #region Tests
                Assert.NotNull(groupUpdate);
                Assert.Equal(groupUpdate.GroupID, groupID);
                Assert.Equal(groupUpdate.Name, name);
            #endregion
        }
    }
}
