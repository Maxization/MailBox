using Xunit;
using MailBox.Models;

namespace UnitTests.ModelsTest.RoleModelsTest
{
    public class RoleTest
    {
        [Fact]
        public void ConstructorTest()
        {
            Role role = null;
            #region Init variables
                string name = "testname";
            #endregion
            role = new Role(name);
            #region Tests
                Assert.NotNull(role);
                Assert.Equal(role.Name, name);
            #endregion
        }
    }
}
