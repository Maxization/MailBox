
using Xunit;
using MailBox.Models.GroupModels;

namespace UnitTests.ModelsTest.GroupModelsTest
{
    public class NewGroupTest
    {
        [Fact]
        public void ConstructorTest()
        {
            #region Init variables
            string name = "testname";
            #endregion
            NewGroup newGroup = new NewGroup
            {
                Name = name
            };
            #region Tests
            Assert.NotNull(newGroup);
            Assert.Equal(newGroup.Name, name);
            #endregion
        }
    }
}
