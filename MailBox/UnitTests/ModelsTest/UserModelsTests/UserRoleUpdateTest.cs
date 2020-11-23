
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

            #region Init variables
                string address = "test@address.com";
                Role role = new Role { Name = "testname" };
            #endregion
            UserRoleUpdate userRoleUpdate = new UserRoleUpdate
            {
                Address = address,
                Role = role
            };
            #region Tests
                Assert.NotNull(userRoleUpdate);
                Assert.Equal(userRoleUpdate.Address, address);
                Assert.Equal(userRoleUpdate.Role, role);
            #endregion
        }
    }
}
