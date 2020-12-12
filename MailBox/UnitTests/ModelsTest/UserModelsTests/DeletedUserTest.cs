
using Xunit;
using MailBox.Models.UserModels;

namespace UnitTests.ModelsTest.UserModelsTests
{
    public class DeletedUserTest
    {
        [Fact]
        public void ConstructorTest()
        {
            #region Init variables
            string address = "test@address.com";
            #endregion
            DeletedUser userEnableUpdate = new DeletedUser
            {
                Address = address
            };
            #region Tests
            Assert.NotNull(userEnableUpdate);
            Assert.Equal(userEnableUpdate.Address, address);
            #endregion
        }
    }
}
