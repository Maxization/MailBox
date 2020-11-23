
using System;
using Xunit;
using System.Collections.Generic;
using MailBox.Models.MailModels;

namespace UnitTests.ModelsTest.MailModelsTest
{
    public class NewMailTest
    {
        [Fact]
        public void ConstructorTest()
        {
            
            #region Init variables
            List<string> CCRecipientsAddresses = new List<string>();
            List<string> BCCRecipientsAddresses = new List<string>();
            string topic = "testtocpic";
            string text = "testtext";
            DateTime dateTime = new DateTime(2021, 1, 1);
            #endregion
            NewMail newMail = new NewMail
            {
                BCCRecipientsAddresses = BCCRecipientsAddresses,
                CCRecipientsAddresses = CCRecipientsAddresses,
                Date = dateTime,
                Text = text,
                Topic = topic
            };
            #region Tests
            Assert.NotNull(newMail);
            Assert.Equal(newMail.CCRecipientsAddresses, CCRecipientsAddresses);
            Assert.Equal(newMail.BCCRecipientsAddresses, BCCRecipientsAddresses);
            Assert.Equal(newMail.Topic, topic);
            Assert.Equal(newMail.Text, text);
            Assert.Equal(newMail.Date, dateTime);
            #endregion
        }
    }
}