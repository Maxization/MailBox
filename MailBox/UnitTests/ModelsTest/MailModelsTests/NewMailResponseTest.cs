using System;
using Xunit;
using MailBox.Models;
using System.Collections.Generic;
using MailBox.Models.UserModels;
using MailBox.Models.MailModels;

namespace UnitTests.ModelsTest.MailModelsTest
{
    public class NewMailResponseTest
    {
        [Fact]
        public void ConstructorTest()
        {
            NewMailResponse newMail = null;
            #region Init variables
                int senderId = 0;
                string topic = "testtocpic";
                string text = "testtext";
                DateTime dateTime = new DateTime(2021, 1, 1);
                int mailReplyId = 0;
            #endregion
            newMail = new NewMailResponse(senderId, topic, text, dateTime, mailReplyId);
            #region Tests
                Assert.NotNull(newMail);
                Assert.Equal(newMail.SenderId, senderId);
                Assert.Equal(newMail.Topic, topic);
                Assert.Equal(newMail.Text, text);
                Assert.Equal(newMail.Date, dateTime);
                Assert.Equal(newMail.MailReplyId, mailReplyId);
            #endregion
        }
    }
}
