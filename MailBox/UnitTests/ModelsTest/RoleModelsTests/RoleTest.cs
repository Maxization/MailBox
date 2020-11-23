using Xunit;
using MailBox.Models;

namespace UnitTests.ModelsTest.RoleModelsTest
{
    public class RoleTest
    {
        [Fact]
        public void ConstructorTest()
        {
            #region Init variables
                string name = "testname";
            #endregion
            Role role = new Role
            {
                Name = name
            };
            #region Tests
                Assert.NotNull(role);
                Assert.Equal(role.Name, name);
            #endregion
        }
    }
}
