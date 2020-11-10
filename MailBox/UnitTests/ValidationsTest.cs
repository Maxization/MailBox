using System;
using Xunit;
using MailBox;
using MailBox.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MailBox.Models;
using System.Collections.Generic;
using MailBox.Models.UserModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using MailBox.Validators;
using FluentValidation.TestHelper;
using System.Text;

namespace UnitTests
{
    public class NewMailValidatorTest
    {
        [Fact]
        public void WhenNewMailHaveNoCCOrBCCRecipients_ShouldHaveError()
        {
            string name = "testname";
            string surname = "testsurname";
            string address = "testaddress";
            Sender sender = new Sender(name, surname, address);
            List<Recipient> CCrecipients = new List<Recipient>();
            List<Recipient> BCCrecipients = new List<Recipient>();
            string topic = "testtocpic";
            string text = "testtext";
            DateTime dateTime = new DateTime(2021, 1, 1);
            InboxMail nullInboxMail = null;
            NewMail newMail = new NewMail(sender, CCrecipients, BCCrecipients, topic, text, dateTime, nullInboxMail);
            var validator = new NewMailValidator();
            var result = validator.TestValidate(newMail);
            result.ShouldHaveValidationErrorFor(x => x.BCCRecipients);
            result.ShouldHaveValidationErrorFor(x => x.CCRecipients);
        }

        [Fact]
        public void WhenNewMailHaveAtLeastOneCCRecipients_ShouldNoHaveError()
        {
            string name = "testname";
            string surname = "testsurname";
            string address = "testaddress";
            Sender sender = new Sender(name, surname, address);
            List<Recipient> CCrecipients = new List<Recipient>() { new Recipient(address) };
            List<Recipient> BCCrecipients = new List<Recipient>();
            string topic = "testtocpic";
            string text = "testtext";
            DateTime dateTime = new DateTime(2021, 1, 1);
            InboxMail nullInboxMail = null;
            NewMail newMail = new NewMail(sender, CCrecipients, BCCrecipients, topic, text, dateTime, nullInboxMail);
            var validator = new NewMailValidator();
            var result = validator.TestValidate(newMail);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void WhenNewMailHaveAtLeastOneBCCRecipients_ShouldNoHaveError()
        {
            string name = "testname";
            string surname = "testsurname";
            string address = "testaddress";
            Sender sender = new Sender(name, surname, address);
            List<Recipient> CCrecipients = new List<Recipient>();
            List<Recipient> BCCrecipients = new List<Recipient>() { new Recipient(address) };
            string topic = "testtocpic";
            string text = "testtext";
            DateTime dateTime = new DateTime(2021, 1, 1);
            InboxMail nullInboxMail = null;
            NewMail newMail = new NewMail(sender, CCrecipients, BCCrecipients, topic, text, dateTime, nullInboxMail);
            var validator = new NewMailValidator();
            var result = validator.TestValidate(newMail);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact] 
        public void WhenNewMailHaveNoTitle_ShouldHaveError()
        {
            string name = "testname";
            string surname = "testsurname";
            string address = "testaddress";
            Sender sender = new Sender(name, surname, address);
            List<Recipient> CCrecipients = new List<Recipient>() { new Recipient(address) };
            List<Recipient> BCCrecipients = new List<Recipient>() { new Recipient(address) };
            string topic = "";
            string text = "testtext";
            DateTime dateTime = new DateTime(2021, 1, 1);
            InboxMail nullInboxMail = null;
            NewMail newMail = new NewMail(sender, CCrecipients, BCCrecipients, topic, text, dateTime, nullInboxMail);
            var validator = new NewMailValidator();
            var result = validator.TestValidate(newMail);
            result.ShouldHaveValidationErrorFor(m => m.Topic);
        }

        [Fact]
        public void WhenNewMailHaveNoText_ShouldHaveError()
        {
            string name = "testname";
            string surname = "testsurname";
            string address = "testaddress";
            Sender sender = new Sender(name, surname, address);
            List<Recipient> CCrecipients = new List<Recipient>() { new Recipient(address) };
            List<Recipient> BCCrecipients = new List<Recipient>() { new Recipient(address) };
            string topic = "testtopic";
            string text = "";
            DateTime dateTime = new DateTime(2021, 1, 1);
            InboxMail nullInboxMail = null;
            NewMail newMail = new NewMail(sender, CCrecipients, BCCrecipients, topic, text, dateTime, nullInboxMail);
            var validator = new NewMailValidator();
            var result = validator.TestValidate(newMail);
            result.ShouldHaveValidationErrorFor(m => m.Text);
        }

        [Fact]
        public void WhenNewMailHaveToLongTopic_ShouldHaveError()
        {
            var validator = new NewMailValidator();
            string name = "testname";
            string surname = "testsurname";
            string address = "testaddress";
            Sender sender = new Sender(name, surname, address);
            List<Recipient> CCrecipients = new List<Recipient>() { new Recipient(address) };
            List<Recipient> BCCrecipients = new List<Recipient>() { new Recipient(address) };
            //string topic = "testtopic";
            StringBuilder stringBuilder = new StringBuilder();
            for(int i=0;i<validator.topicMaxLength+1;i++)
            {
                stringBuilder.Append('x');
            }
            string topic = stringBuilder.ToString();
            string text = "testtext";
            DateTime dateTime = new DateTime(2021, 1, 1);
            InboxMail nullInboxMail = null;
            NewMail newMail = new NewMail(sender, CCrecipients, BCCrecipients, topic, text, dateTime, nullInboxMail);           
            var result = validator.TestValidate(newMail);
            result.ShouldHaveValidationErrorFor(m => m.Topic);
        }

        [Fact]
        public void WhenNewMailHaveToLongText_ShouldHaveError()
        {
            var validator = new NewMailValidator();
            string name = "testname";
            string surname = "testsurname";
            string address = "testaddress";
            Sender sender = new Sender(name, surname, address);
            List<Recipient> CCrecipients = new List<Recipient>() { new Recipient(address) };
            List<Recipient> BCCrecipients = new List<Recipient>() { new Recipient(address) };
            //string topic = "testtopic";
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < validator.textMaxLength + 1; i++)
            {
                stringBuilder.Append('x');
            }
            string topic = "texttopic";
            string text = stringBuilder.ToString();
            DateTime dateTime = new DateTime(2021, 1, 1);
            InboxMail nullInboxMail = null;
            NewMail newMail = new NewMail(sender, CCrecipients, BCCrecipients, topic, text, dateTime, nullInboxMail);
            var result = validator.TestValidate(newMail);
            result.ShouldHaveValidationErrorFor(m => m.Text);
        }
    }
}
