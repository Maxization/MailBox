using Xunit;
using MailBox.Models.UserModels;

namespace UnitTests.ModelsTest.UserModelsTests
{
    public class UserGlobalViewTest
    {
        [Fact]
        public void ConstructorTest()
        {
            
            #region Init variables
                string name = "testname";
                string surname = "testsurname";
                string address = "test@address.com";
            #endregion
            UserGlobalView userGlobalView = new UserGlobalView
            {
                Name = name,
                Surname = surname,
                Address = address
            };
            #region Tests
                Assert.NotNull(userGlobalView);
                Assert.Equal(userGlobalView.Name, name);
                Assert.Equal(userGlobalView.Surname, surname);
                Assert.Equal(userGlobalView.Address, address);
            #endregion
        }
    }
}
