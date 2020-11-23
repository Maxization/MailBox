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
            #region Init variables
                int mailId = 0;
                bool read = true;
            #endregion
            MailReadUpdate mailReadUpdate = new MailReadUpdate
            {
                MailID = mailId,
                Read = read
            };
            #region Tests
                Assert.NotNull(mailReadUpdate);
                Assert.Equal(mailReadUpdate.MailID, mailId);
                Assert.Equal(mailReadUpdate.Read, read);
            #endregion
        }
    }
}
