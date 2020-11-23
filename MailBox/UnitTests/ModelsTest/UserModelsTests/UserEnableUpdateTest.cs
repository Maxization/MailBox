
using Xunit;
using MailBox.Models.UserModels;

namespace UnitTests.ModelsTest.UserModelsTests
{
    public class UserEnableUpdateTest
    {
        [Fact]
        public void ConstructorTest()
        {
            #region Init variables
                string address = "test@address.com";
                bool enable = false;
            #endregion
            UserEnableUpdate userEnableUpdate = new UserEnableUpdate
            {
                Address = address,
                Enable = enable
            };
            #region Tests
                Assert.NotNull(userEnableUpdate);
                Assert.Equal(userEnableUpdate.Address, address);
                Assert.Equal(userEnableUpdate.Enable, enable);
            #endregion
        }
    }
}
