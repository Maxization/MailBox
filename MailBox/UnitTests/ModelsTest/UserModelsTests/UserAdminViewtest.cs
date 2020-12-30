
using Xunit;
using MailBox.Models.UserModels;

namespace UnitTests.ModelsTest.UserModelsTests
{
    public class UserAdminViewtest
    {
        [Fact]
        public void ConstructorTest()
        {
            #region Init variables
            string name = "testname";
            string surname = "surname";
            string address = "test@address.com";
            string roleName = "testRoleName";
            #endregion
            UserAdminView userAdminView = new UserAdminView
            {
                Name = name,
                Surname = surname,
                Address = address,
                RoleName = roleName
            };
            #region Tests
            Assert.NotNull(userAdminView);
            Assert.Equal(userAdminView.Name, name);
            Assert.Equal(userAdminView.Surname, surname);
            Assert.Equal(userAdminView.Address, address);
            Assert.Equal(userAdminView.RoleName, roleName);
            #endregion
        }
    }
}
