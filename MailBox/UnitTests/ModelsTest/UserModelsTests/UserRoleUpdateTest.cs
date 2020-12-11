
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
                string roleName = "testname" ;
            #endregion
            UserRoleUpdate userRoleUpdate = new UserRoleUpdate
            {
                Address = address,
                RoleName = roleName
            };
            #region Tests
                Assert.NotNull(userRoleUpdate);
                Assert.Equal(userRoleUpdate.Address, address);
                Assert.Equal(userRoleUpdate.RoleName, roleName);
            #endregion
        }
    }
}
