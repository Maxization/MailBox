using System;
using Xunit;
using MailBox.Models;
using System.Collections.Generic;
using MailBox.Models.UserModels;

namespace UnitTests.ModelsTest.MailModelsTest
{
    public class MailInboxViewTest
    {
        [Fact]
        public void ConstructorTest()
        {
            MailInboxView inboxMail = null;
            #region Init variables
                int mailId = 1;
                bool read = true;
                string name = "testname";
                string surname = "testsurname";
                string address = "test@address.com";
                UserGlobalView sender = new UserGlobalView(name, surname, address);
                List<string> recipientsAddresses = new List<string>();
                string topic = "testtopic";
                string text = "testtext";
                DateTime dateTime = new DateTime(2021, 1, 1);
                MailInboxView nullInboxMail = null;
            #endregion
            inboxMail = new MailInboxView(mailId, read, sender, recipientsAddresses, topic, text, dateTime, nullInboxMail);
            #region Tests
                Assert.NotNull(inboxMail);
                Assert.Equal(inboxMail.MailId, mailId);
                Assert.Equal(inboxMail.Read, read);
                Assert.Equal(inboxMail.Sender, sender);
                Assert.Equal(inboxMail.RecipientsAddresses, recipientsAddresses);
                Assert.Equal(inboxMail.Topic, topic);
                Assert.Equal(inboxMail.Text, text);
                Assert.Equal(inboxMail.Date, dateTime);
                Assert.Equal(inboxMail.MailReply, nullInboxMail);
            #endregion
        }
    }
}
