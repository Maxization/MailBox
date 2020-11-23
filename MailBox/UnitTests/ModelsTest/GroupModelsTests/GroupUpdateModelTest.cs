
using Xunit;
using MailBox.Models.GroupModels;

namespace UnitTests.ModelsTest.GroupModelsTest
{
    public class GroupMemberUpdateTest
    {
        [Fact]
        public void ConstructorTest()
        {
            
            #region Init variables
                int groupID = 0;
                string groupMemberAddress = "test@address.com";
            #endregion
            GroupMemberUpdate groupUpdate = new GroupMemberUpdate
            {
                GroupID = groupID,
                GroupMemberAddress = groupMemberAddress
            };
            #region Tests
                Assert.NotNull(groupUpdate);
                Assert.Equal(groupUpdate.GroupID, groupID);
                Assert.Equal(groupUpdate.GroupMemberAddress, groupMemberAddress);
            #endregion
        }
    }
}
