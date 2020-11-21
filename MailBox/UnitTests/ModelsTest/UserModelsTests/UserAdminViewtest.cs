using Xunit;
using MailBox.Models.UserModels;
using MailBox.Models;

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
                Role role = new Role {  Name = name };
                bool enable = true;
            #endregion
            UserAdminView userAdminView = new UserAdminView
            {
                Name = name,
                Surname = surname,
                Address = address,
                Enable = enable,
                Role = role
            };
            #region Tests
            Assert.NotNull(userAdminView);
                Assert.Equal(userAdminView.Name, name);
                Assert.Equal(userAdminView.Surname, surname);
                Assert.Equal(userAdminView.Address, address);
                Assert.Equal(userAdminView.Role, role);
                Assert.Equal(userAdminView.Enable, enable);
            #endregion
        }
    }
}
