using Xunit;
using FluentValidation.TestHelper;
using MailBox.Validators.MailValidators;
using MailBox.Models.MailModels;
using System.Collections.Generic;
using System;
using System.Text;

namespace UnitTests.ValidatorsTest.MailValidatorsTests
{
    public class NewMailResponseValidatorTest
    {
        [Fact]
        public void WhenNewMailResponseTopicIsEmpty_ShouldHaveError()
        {
            var validator = new NewMailResponseValidator();
            #region Init variables
                int senderId = 0;
                string topic = "";
                string text = "testtext";
                DateTime dateTime = new DateTime(2021, 1, 1);
                int mailReplyId = 0;
            #endregion
            NewMailResponse newMail = new NewMailResponse(senderId, topic, text, dateTime, mailReplyId);
            var result = validator.TestValidate(newMail);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Topic);
            #endregion
        }

        [Fact]
        public void WhenNewMailResponseTopicIsTooLong_ShouldHaveError()
        {
            var validator = new NewMailResponseValidator();
            #region Init variables
                int senderId = 0;
                StringBuilder stringBuilder = new StringBuilder('x');
                for(int i=0;i< validator.topicMaxLength+1;i++)
                {
                    stringBuilder.Append('x');
                }
                string topic = stringBuilder.ToString();
                string text = "testtext";
                DateTime dateTime = new DateTime(2021, 1, 1);
                int mailReplyId = 0;
            #endregion
            NewMailResponse newMail = new NewMailResponse(senderId, topic, text, dateTime, mailReplyId);
            var result = validator.TestValidate(newMail);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Topic);
            #endregion
        }

        [Fact]
        public void WhenNewMailResponseTextIsTooLong_ShouldHaveError()
        {
            var validator = new NewMailResponseValidator();
            #region Init variables
                int senderId = 0;
                StringBuilder stringBuilder = new StringBuilder('x');
                for (int i = 0; i < validator.textMaxLength + 1; i++)
                {
                    stringBuilder.Append('x');
                }
                string topic = "testtopic";
                string text = stringBuilder.ToString();
                DateTime dateTime = new DateTime(2021, 1, 1);
                int mailReplyId = 0;
            #endregion
            NewMailResponse newMail = new NewMailResponse(senderId, topic, text, dateTime, mailReplyId);
            var result = validator.TestValidate(newMail);
            #region Tests
                result.ShouldHaveValidationErrorFor(x => x.Text);
            #endregion
        }

        [Fact]
        public void WhenNewMailResponsIsOk_ShouldNotHaveAnyError()
        {
            var validator = new NewMailResponseValidator();
            #region Init variables
                int senderId = 0;
                string topic = "testtopic";
                string text = "testtext";
                DateTime dateTime = new DateTime(2021, 1, 1);
                int mailReplyId = 0;
            #endregion
            NewMailResponse newMail = new NewMailResponse(senderId, topic, text, dateTime, mailReplyId);
            var result = validator.TestValidate(newMail);
            #region Tests
                result.ShouldNotHaveAnyValidationErrors();
            #endregion
        }
    }
}
