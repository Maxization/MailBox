
using System;
using Xunit;
using MailBox.Models.MailModels;
using MailBox.Models.UserModels;

namespace UnitTests.ModelsTest.MailModelsTest
{
    public class MailInboxViewTest
    {
        [Fact]
        public void ConstructorTest()
        {
            #region Init variables
            int mailID = 1;
            bool read = true;
            string name = "testname";
            string surname = "testsurname";
            string address = "test@address.com";
            UserGlobalView sender = new UserGlobalView
            {
                Name = name,
                Surname = surname,
                Address = address
            };
            string topic = "testtopic";
            DateTime dateTime = new DateTime(2021, 1, 1);
            #endregion
            MailInboxView inboxMail = new MailInboxView
            {
                MailID = mailID,
                Sender = sender,
                Date = dateTime,
                Topic = topic,
                Read = read
            };
            #region Tests
            Assert.NotNull(inboxMail);
            Assert.Equal(inboxMail.MailID, mailID);
            Assert.Equal(inboxMail.Read, read);
            Assert.Equal(inboxMail.Sender, sender);
            Assert.Equal(inboxMail.Topic, topic);
            Assert.Equal(inboxMail.Date, dateTime);
            #endregion
        }
    }
}
