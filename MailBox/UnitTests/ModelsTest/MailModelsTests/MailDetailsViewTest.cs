
using System;
using Xunit;
using MailBox.Models.MailModels;
using System.Collections.Generic;
using MailBox.Models.UserModels;

namespace UnitTests.ModelsTest.MailModelsTest
{
    public class MailDetailsViewTest
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
            List<string> recipientsAddresses = new List<string>();
            string topic = "testtopic";
            string text = "testtext";
            DateTime dateTime = new DateTime(2021, 1, 1);
            #endregion
            MailDetailsView inboxMail = new MailDetailsView
            {
                MailID = mailID,
                Sender = sender,
                RecipientsAddresses = recipientsAddresses,
                Date = dateTime,
                Text = text,
                Topic = topic,
                Read = read
            };
            #region Tests
            Assert.NotNull(inboxMail);
            Assert.Equal(inboxMail.MailID, mailID);
            Assert.Equal(inboxMail.Read, read);
            Assert.Equal(inboxMail.Sender, sender);
            Assert.Equal(inboxMail.RecipientsAddresses, recipientsAddresses);
            Assert.Equal(inboxMail.Topic, topic);
            Assert.Equal(inboxMail.Text, text);
            Assert.Equal(inboxMail.Date, dateTime);
            #endregion
        }
    }
}
