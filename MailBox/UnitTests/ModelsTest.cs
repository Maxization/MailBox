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

namespace UnitTests
{
    public class GroupModelTest
    {
        [Fact]
        public void ConstructorTest()
        {
            string name = "testname";
            string surname = "testsurname";
            string address = "testaddress";
            GroupMember groupMember1 = new GroupMember(name, surname, address);
            GroupMember groupMember2 = new GroupMember(name, surname, address);
            List<GroupMember> groupMembers = new List<GroupMember>();
            groupMembers.Add(groupMember1);
            groupMembers.Add(groupMember2);
            var group = new Group(name, groupMembers);
            Assert.Equal(group.Name, name);
            Assert.Equal(group.GroupMembers, groupMembers);
        }
    }

    public class InboxMailTest
    {
        [Fact]
        public void ConstructorTest()
        {
            bool read = true;
            string name = "testname";
            string surname = "testsurname";
            string address = "testaddress";
            Sender sender = new Sender(name, surname, address);
            List<Recipient> recipients = new List<Recipient>();
            Recipient recipient1 = new Recipient(address);
            Recipient recipient2 = new Recipient(address);
            recipients.Add(recipient1);
            recipients.Add(recipient2);
            string topic = "testtopic";
            string text = "testtext";
            DateTime dateTime = new DateTime(2021, 1, 1);
            InboxMail nullInboxMail = null;
            int id = 1;
            InboxMail inboxMail = new InboxMail(id, read, sender, recipients, topic, text, dateTime, nullInboxMail);
            Assert.Equal(inboxMail.Read,read);
            Assert.Equal(inboxMail.Sender,sender);            
            Assert.Equal(inboxMail.Recipients,recipients);            
            Assert.Equal(inboxMail.Topic,topic);            
            Assert.Equal(inboxMail.Text,text);            
            Assert.Equal(inboxMail.Date,dateTime);            
            Assert.Equal(inboxMail.MailReply,nullInboxMail);            
        }
    }

    public class NewMailTest
    {
        [Fact]
        public void ConstructorTest()
        {
            string name = "testname";
            string surname = "testsurname";
            string address = "testaddress";
            Sender sender = new Sender(name, surname, address);
            List<Recipient> CCrecipients = new List<Recipient>();
            List<Recipient> BCrecipients = new List<Recipient>();
            string topic = "testtocpic";
            string text = "testtext";
            DateTime dateTime = new DateTime(2021, 1, 1);
            InboxMail nullInboxMail = null;
            NewMail newMail = new NewMail(sender, CCrecipients, BCrecipients, topic, text, dateTime, nullInboxMail);
            Assert.Equal(newMail.Sender, sender);
            Assert.Equal(newMail.CCRecipients, CCrecipients);
            Assert.Equal(newMail.BCRecipients, BCrecipients);
            Assert.Equal(newMail.Topic, topic);
            Assert.Equal(newMail.Text, text);
            Assert.Equal(newMail.Date, dateTime);
            Assert.Equal(newMail.MailReply, nullInboxMail);
        }
    }

    public class RoleTest
    {
        [Fact]
        public void ConstructorTest()
        {
            string name = "testname";
            Role role = new Role(name);
            Assert.Equal(role.Name, name);
        }
    }

    public class GroupMemberTest
    {
        [Fact]
        public void ConstructorTest()
        {
            string name = "testname";
            string surname = "testsurname";
            string address = "testaddress";
            GroupMember groupMember = new GroupMember(name, surname, address);
            Assert.Equal(groupMember.Name, name);
            Assert.Equal(groupMember.Surname, surname);
            Assert.Equal(groupMember.Address, address);
        }
    }

    public class RecipientTest
    {
        [Fact]
        public void ConstructorTest()
        {
            string address = "testaddress";
            Recipient recipient = new Recipient(address);
            Assert.Equal(recipient.Address, address);
        }
    }

    public class SenderTest
    {
        [Fact]
        public void ConstructorTest()
        {
            string name = "testname";
            string surname = "testsurname";
            string address = "testaddress";
            Sender sender = new Sender(name, surname, address);
            Assert.Equal(sender.Name, name);
            Assert.Equal(sender.Surname, surname);
            Assert.Equal(sender.Address, address);
        }
    }

    public class UserTest
    {
        [Fact]
        public void ConstructorTest()
        {
            string name = "testname";
            string surname = "testsurname";
            string address = "testaddress";
            Role role = new Role(name);
            User user = new User(name, surname, address, role);
            Assert.Equal(user.Name, name);
            Assert.Equal(user.Surname, surname);
            Assert.Equal(user.Address, address);
            Assert.Equal(user.Role, role);
        }
    }
}
