using System;
using Xunit;
using MailBox.Models.MailModels;
using System.Collections.Generic;
using FluentValidation.TestHelper;
using System.Text;
using MailBox.Validators.MailValidators;

namespace UnitTests
{
    public class NewMailValidatorTest
    {
        [Fact]
        public void WhenNewMailHaveNoCCOrBCCRecipients_ShouldHaveError()
        {
            var validator = new NewMailValidator();
            #region Init variables
                List<string> CCRecipientsAddresses = new List<string>();
                List<string> BCCRecipientsAddresses = new List<string>();
                string topic = "testtopic";
                string text = "testtext";
                DateTime dateTime = new DateTime(2021, 1, 1);
            #endregion
            NewMail newMail = new NewMail
            {
                CCRecipientsAddresses = CCRecipientsAddresses,
                BCCRecipientsAddresses = BCCRecipientsAddresses,
                Topic = topic,
                Text = text,
                Date = dateTime
            };
            var result = validator.TestValidate(newMail);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.CCRecipientsAddresses);
                result.ShouldHaveValidationErrorFor(x => x.BCCRecipientsAddresses);
            #endregion
        }

        [Fact]
        public void WhenNewMailHaveAtLeastOneCCRecipients_ShouldNotHaveAnyError()
        {
            var validator = new NewMailValidator();
            #region Init variables
                List<string> CCRecipientsAddresses = new List<string> { "test@address.com" };
                List<string> BCCRecipientsAddresses = new List<string>();
                string topic = "testtopic";
                string text = "testtext";
                DateTime dateTime = new DateTime(2021, 1, 1);
            #endregion
            NewMail newMail = new NewMail
            {
                CCRecipientsAddresses = CCRecipientsAddresses,
                BCCRecipientsAddresses = BCCRecipientsAddresses,
                Topic = topic,
                Text = text,
                Date = dateTime
            };
            var result = validator.TestValidate(newMail);
            #region Tests
                result.ShouldNotHaveAnyValidationErrors();
            #endregion
        }

        [Fact]
        public void WhenNewMailHaveAtLeastOneBCCRecipients_ShouldNotHaveAnyError()
        {
            var validator = new NewMailValidator();
            #region Init variables
                List<string> CCRecipientsAddresses = new List<string>();
                List<string> BCCRecipientsAddresses = new List<string> { "test@address.com" };
                string topic = "testtopic";
                string text = "testtext";
                DateTime dateTime = new DateTime(2021, 1, 1);
            #endregion
            NewMail newMail = new NewMail
            {
                CCRecipientsAddresses = CCRecipientsAddresses,
                BCCRecipientsAddresses = BCCRecipientsAddresses,
                Topic = topic,
                Text = text,
                Date = dateTime
            };
            var result = validator.TestValidate(newMail);
            #region Tests
                result.ShouldNotHaveAnyValidationErrors();
            #endregion
        }

        [Fact] 
        public void WhenNewMailHaveNoTopic_ShouldHaveError()
        {
            var validator = new NewMailValidator();
            #region Init variables
                List<string> CCRecipientsAddresses = new List<string> { "test@address.com" };
                List<string> BCCRecipientsAddresses = new List<string> { "test@address.com" };
                string topic = "";
                string text = "testtext";
                DateTime dateTime = new DateTime(2021, 1, 1);
            #endregion
            NewMail newMail = new NewMail
            {
                CCRecipientsAddresses = CCRecipientsAddresses,
                BCCRecipientsAddresses = BCCRecipientsAddresses,
                Topic = topic,
                Text = text,
                Date = dateTime
            };
            var result = validator.TestValidate(newMail);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Topic);
            #endregion
        }

        [Fact]
        public void WhenNewMailHaveNoText_ShouldHaveError()
        {
            var validator = new NewMailValidator();
            #region Init variables
                List<string> CCRecipientsAddresses = new List<string> { "test@address.com" };
                List<string> BCCRecipientsAddresses = new List<string> { "test@address.com" };
                string topic = "testtopic";
                string text = "";
                DateTime dateTime = new DateTime(2021, 1, 1);
            #endregion
            NewMail newMail = new NewMail
            {
                CCRecipientsAddresses = CCRecipientsAddresses,
                BCCRecipientsAddresses = BCCRecipientsAddresses,
                Topic = topic,
                Text = text,
                Date = dateTime
            };
            var result = validator.TestValidate(newMail);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Text);
            #endregion
        }

        [Fact]
        public void WhenNewMailHaveToLongTopic_ShouldHaveError()
        {
            var validator = new NewMailValidator();
            #region Init variables
                List<string> CCRecipientsAddresses = new List<string> { "test@address.com" };
                List<string> BCCRecipientsAddresses = new List<string> { "test@address.com" };
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < validator.topicMaxLength + 1; i++)
                {
                    stringBuilder.Append('x');
                }
                string topic = stringBuilder.ToString();
                string text = "testtext";
                DateTime dateTime = new DateTime(2021, 1, 1);
            #endregion
            NewMail newMail = new NewMail
            {
                CCRecipientsAddresses = CCRecipientsAddresses,
                BCCRecipientsAddresses = BCCRecipientsAddresses,
                Topic = topic,
                Text = text,
                Date = dateTime
            };
            var result = validator.TestValidate(newMail);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Topic);
            #endregion          
        }

        [Fact]
        public void WhenNewMailHaveToLongText_ShouldHaveError()
        {
            var validator = new NewMailValidator();
            #region Init variables
                List<string> CCRecipientsAddresses = new List<string> { "test@address.com" };
                List<string> BCCRecipientsAddresses = new List<string> { "test@address.com" };
                string topic = "testtopic";
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < validator.textMaxLength + 1; i++)
                {
                    stringBuilder.Append('x');
                }
                string text = stringBuilder.ToString();
                DateTime dateTime = new DateTime(2021, 1, 1);
            #endregion
            NewMail newMail = new NewMail
            {
                CCRecipientsAddresses = CCRecipientsAddresses,
                BCCRecipientsAddresses = BCCRecipientsAddresses,
                Topic = topic,
                Text = text,
                Date = dateTime
            };
            var result = validator.TestValidate(newMail);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Text);
            #endregion          
        }

        [Fact]
        public void WhenNewMailHaveAtLeastOneWrongCCRecipientAddress_ShouldHaveError()
        {
            var validator = new NewMailValidator();
            #region Init variables
                List<string> CCRecipientsAddresses = new List<string>() { "testaddress.com", "test1@address.com", "test2@address.com" };
                List<string> BCCRecipientsAddresses = new List<string> { "test@address.com" };
                string topic = "testtopic";
                string text = "testtext";
                DateTime dateTime = new DateTime(2021, 1, 1);
            #endregion
            NewMail newMail = new NewMail
            {
                CCRecipientsAddresses = CCRecipientsAddresses,
                BCCRecipientsAddresses = BCCRecipientsAddresses,
                Topic = topic,
                Text = text,
                Date = dateTime
            };
            var result = validator.TestValidate(newMail);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.CCRecipientsAddresses);
            #endregion
        }

        [Fact]
        public void WhenNewMailHaveAtLeastOneWrongBCCRecipientAddress_ShouldHaveError()
        {
            var validator = new NewMailValidator();
            #region Init variables
                List<string> CCRecipientsAddresses = new List<string>() { "test@address.com", "test1@address.com", "test2@address.com" };
                List<string> BCCRecipientsAddresses = new List<string> { "test@address.com" , "test@addresscom" };
                string topic = "testtopic";
                string text = "testtext";
                DateTime dateTime = new DateTime(2021, 1, 1);
            #endregion
            NewMail newMail = new NewMail
            {
                CCRecipientsAddresses = CCRecipientsAddresses,
                BCCRecipientsAddresses = BCCRecipientsAddresses,
                Topic = topic,
                Text = text,
                Date = dateTime
            };
            var result = validator.TestValidate(newMail);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.BCCRecipientsAddresses);
            #endregion
        }

    }
}
