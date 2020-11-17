using Xunit;
using MailBox.Models.UserModels;


namespace UnitTests.ModelsTest.UserModelsTests
{
    public class NewUserTest
    {
        [Fact]
        public void ConstructorTest()
        {
            NewUser newUser = null;
            #region Init variables
                string name = "testname";
                string surname = "surname";
                string address = "test@address.com";
            #endregion
            newUser = new NewUser(name, surname, address);
            #region Tests
                Assert.NotNull(newUser);
                Assert.Equal(newUser.Name, name);
                Assert.Equal(newUser.Surname, surname);
                Assert.Equal(newUser.Address, address);
            #endregion
        }
    }
}
