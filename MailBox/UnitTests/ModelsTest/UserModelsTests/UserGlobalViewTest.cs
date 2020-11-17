using Xunit;
using MailBox.Models.UserModels;

namespace UnitTests.ModelsTest.UserModelsTests
{
    public class UserGlobalViewTest
    {
        [Fact]
        public void ConstructorTest()
        {
            UserGlobalView userGlobalView = null;
            #region Init variables
                string name = "testname";
                string surname = "testsurname";
                string address = "test@address.com";
            #endregion
            userGlobalView = new UserGlobalView(name, surname, address);
            #region Tests
                Assert.NotNull(userGlobalView);
                Assert.Equal(userGlobalView.Name, name);
                Assert.Equal(userGlobalView.Surname, surname);
                Assert.Equal(userGlobalView.Address, address);
            #endregion
        }
    }
}
