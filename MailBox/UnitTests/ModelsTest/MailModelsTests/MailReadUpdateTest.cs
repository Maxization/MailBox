
using Xunit;
using MailBox.Models.MailModels;

namespace UnitTests.ModelsTest.MailModelsTest
{
    public class MailReadUpdateTest
    {
        [Fact]
        public void ConstructorTest()
        {
            #region Init variables
                int mailID = 0;
                bool read = true;
            #endregion
            MailReadUpdate mailReadUpdate = new MailReadUpdate
            {
                MailID = mailID,
                Read = read
            };
            #region Tests
                Assert.NotNull(mailReadUpdate);
                Assert.Equal(mailReadUpdate.MailID, mailID);
                Assert.Equal(mailReadUpdate.Read, read);
            #endregion
        }
    }
}
