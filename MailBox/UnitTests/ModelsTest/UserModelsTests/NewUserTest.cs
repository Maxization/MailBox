
using Xunit;
using MailBox.Models.UserModels;

namespace UnitTests.ModelsTest.UserModelsTests
{
    public class NewUserTest
    {
        [Fact]
        public void ConstructorTest()
        {
            #region Init variables
                string name = "testname";
                string surname = "surname";
                string address = "test@address.com";
            #endregion
            NewUser newUser = new NewUser
            {
                Name = name,
                Surname = surname,
                Address = address
            };
            #region Tests
                Assert.NotNull(newUser);
                Assert.Equal(newUser.Name, name);
                Assert.Equal(newUser.Surname, surname);
                Assert.Equal(newUser.Address, address);
            #endregion
        }
    }
}
