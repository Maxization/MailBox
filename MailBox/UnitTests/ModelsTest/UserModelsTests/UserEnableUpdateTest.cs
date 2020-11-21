using Xunit;
using MailBox.Models.UserModels;

namespace UnitTests.ModelsTest.UserModelsTests
{
    public class UserEnableUpdateTest
    {
        [Fact]
        public void ConstructorTest()
        {
            UserEnableUpdate userEnableUpdate = null;
            #region Init variables
                string address = "test@address.com";
                bool enable = false;
            #endregion
            userEnableUpdate = new UserEnableUpdate(address, enable);
            #region Tests
                Assert.NotNull(userEnableUpdate);
                Assert.Equal(userEnableUpdate.Address, address);
                Assert.Equal(userEnableUpdate.Enable, enable);
            #endregion
        }
    }
}
