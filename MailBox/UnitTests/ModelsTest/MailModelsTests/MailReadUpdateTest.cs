using System;
using Xunit;
using MailBox.Models;
using System.Collections.Generic;
using MailBox.Models.UserModels;
using MailBox.Models.MailModels;

namespace UnitTests.ModelsTest.MailModelsTest
{
    public class MailReadUpdateTest
    {
        [Fact]
        public void ConstructorTest()
        {
            MailReadUpdate mailReadUpdate = null;
            #region Init variables
                int mailId = 0;
                bool read = true;
            #endregion
            mailReadUpdate = new MailReadUpdate(mailId, read);
            #region Tests
                Assert.NotNull(mailReadUpdate);
                Assert.Equal(mailReadUpdate.MailId, mailId);
                Assert.Equal(mailReadUpdate.Read, read);
            #endregion
        }
    }
}
