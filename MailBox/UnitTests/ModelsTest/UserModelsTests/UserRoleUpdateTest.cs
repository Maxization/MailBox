using Xunit;
using MailBox.Models.UserModels;
using MailBox.Models;

namespace UnitTests.ModelsTest.UserModelsTests
{
    public class UserRoleUpdateTest
    {
        [Fact]
        public void ConstructorTest()
        {
            UserRoleUpdate userRoleUpdate = null;
            #region Init variables
                string address = "test@address.com";
                Role role = new Role("testname");
            #endregion
            userRoleUpdate = new UserRoleUpdate(address, role);
            #region Tests
                Assert.NotNull(userRoleUpdate);
                Assert.Equal(userRoleUpdate.Address, address);
                Assert.Equal(userRoleUpdate.Role, role);
            #endregion
        }
    }
}
