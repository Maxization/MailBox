using System;
using Xunit;
using MailBox.Models;
using System.Collections.Generic;
using MailBox.Models.GroupModels;
using MailBox.Models.UserModels;
using MailBox.Models.MailModels;

namespace UnitTests
{
    public class GroupMemberUpdateTest
    {
        [Fact]
        public void ConstructorTest()
        {
            GroupMemberUpdate groupUpdate = null;
            #region Init variables
            int groupId = 0;
            string groupMemberAddress = "test@address.com";
            #endregion
            groupUpdate = new GroupMemberUpdate(groupId, groupMemberAddress);
            #region Tests
            Assert.NotNull(groupUpdate);
            Assert.Equal(groupUpdate.GroupId, groupId);
            Assert.Equal(groupUpdate.GroupMemberAddress, groupMemberAddress);
            #endregion
        }
    }

    public class GroupNameUpdateTest
    {
        [Fact]
        public void ConstructorTest()
        {
            GroupNameUpdate groupUpdate = null;
            #region Init variables
            int groupId = 0;
            string name = "testname";
            #endregion
            groupUpdate = new GroupNameUpdate(groupId, name);
            #region Tests
            Assert.NotNull(groupUpdate);
            Assert.Equal(groupUpdate.GroupId, groupId);
            Assert.Equal(groupUpdate.Name, name);
            #endregion
        }
    }

    public class GroupViewTest
    {
        [Fact]
        public void ConstructorTest()
        {
            GroupView groupView = null;
            #region Init variables
            int groupId = 0;
            string name = "testname";
            List<UserGlobalView> groupMembers = new List<UserGlobalView>();
            #endregion
            groupView = new GroupView(groupId, name, groupMembers);
            #region Tests
            Assert.NotNull(groupView);
            Assert.Equal(groupView.GroupId, groupId);
            Assert.Equal(groupView.Name, name);
            Assert.Equal(groupView.GroupMembers, groupMembers);
            #endregion
        }
    }

    public class NewGroupTest
    {
        [Fact]
        public void ConstructorTest()
        {
            NewGroup newGroup = null;
            #region Init variables
            int ownerId = 0;
            string name = "testname";
            #endregion
            newGroup = new NewGroup(ownerId, name);
            #region Tests
            Assert.NotNull(newGroup);
            Assert.Equal(newGroup.OwnerId, ownerId);
            Assert.Equal(newGroup.Name, name);
            #endregion
        }
    }

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

    public class MailReadUpdateTest
    {
        [Fact]
        public void ConstructorTest()
        {
            MailReadUpdate mailReadUpdate = null;
            #region Init variables
            int mailId = 0;
            bool read = true;
            #endregion
            mailReadUpdate = new MailReadUpdate(mailId, read);
            #region Tests
            Assert.NotNull(mailReadUpdate);
            Assert.Equal(mailReadUpdate.MailId, mailId);
            Assert.Equal(mailReadUpdate.Read, read);
            #endregion
        }
    }

    public class NewMailTest
    {
        [Fact]
        public void ConstructorTest()
        {
            NewMail newMail = null;
            #region Init variables
            int senderId = 0;
            List<string> CCRecipientsAddresses = new List<string>();
            List<string> BCCRecipientsAddresses = new List<string>();
            string topic = "testtocpic";
            string text = "testtext";
            DateTime dateTime = new DateTime(2021, 1, 1);
            #endregion
            newMail = new NewMail(senderId, CCRecipientsAddresses, BCCRecipientsAddresses, topic, text, dateTime);
            #region Tests
            Assert.NotNull(newMail);
            Assert.Equal(newMail.SenderId, senderId);
            Assert.Equal(newMail.CCRecipientsAddresses, CCRecipientsAddresses);
            Assert.Equal(newMail.BCCRecipientsAddresses, BCCRecipientsAddresses);
            Assert.Equal(newMail.Topic, topic);
            Assert.Equal(newMail.Text, text);
            Assert.Equal(newMail.Date, dateTime);
            #endregion
        }
    }

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

    public class RoleTest
    {
        [Fact]
        public void ConstructorTest()
        {
            Role role = null;
            #region Init variables
            string name = "testname";
            #endregion
            role = new Role(name);
            #region Tests
            Assert.NotNull(role);
            Assert.Equal(role.Name, name);
            #endregion
        }
    }

    public class NewUserTest
    {
        [Fact]
        public void ConstructorTest()
        {
            NewUser newUser = null;
            #region Init variables
            string name = "testname";
            string surname = "surname";
            string address = "test@address.com";
            #endregion
            newUser = new NewUser(name,surname,address);
            #region Tests
            Assert.NotNull(newUser);
            Assert.Equal(newUser.Name,name);
            Assert.Equal(newUser.Surname,surname);
            Assert.Equal(newUser.Address,address);
            #endregion
        }
    }

    public class UserAdminViewtest
    {
        [Fact]
        public void ConstructorTest()
        {
            UserAdminView userAdminView = null;
            #region Init variables
            string name = "testname";
            string surname = "surname";
            string address = "test@address.com";
            Role role = new Role(name);
            bool enable = true;
            #endregion
            userAdminView = new UserAdminView(name,surname,address,role,enable);
            #region Tests
            Assert.NotNull(userAdminView);
            Assert.Equal(userAdminView.Name,name);
            Assert.Equal(userAdminView.Surname,surname);
            Assert.Equal(userAdminView.Address,address);
            Assert.Equal(userAdminView.Role,role);
            Assert.Equal(userAdminView.Enable,enable);
            #endregion
        }
    }

    public class UserEnableUpdateTest
    {
        [Fact]
        public void ConstructorTest()
        {
            UserEnableUpdate userEnableUpdate = null;
            #region Init variables
            string address = "test@address.com";
            bool enable = false;
            #endregion
            userEnableUpdate = new UserEnableUpdate(address,enable);
            #region Tests
            Assert.NotNull(userEnableUpdate);
            Assert.Equal(userEnableUpdate.Address,address);
            Assert.Equal(userEnableUpdate.Enable,enable);
            #endregion
        }
    }

    public class UserGlobalViewTest
    {
        [Fact]
        public void ConstructorTest()
        {
            UserGlobalView userGlobalView = null;
            #region Init variables
            string name = "testname";
            string surname = "testsurname";
            string address = "test@address.com";
            #endregion
            userGlobalView = new UserGlobalView(name,surname,address);
            #region Tests
            Assert.NotNull(userGlobalView);
            Assert.Equal(userGlobalView.Name,name);
            Assert.Equal(userGlobalView.Surname,surname);
            Assert.Equal(userGlobalView.Address,address);
            #endregion
        }
    }

    public class UserRoleUpdateTest
    {
        [Fact]
        public void ConstructorTest()
        {
            UserRoleUpdate userRoleUpdate = null;
            #region Init variables
            string address = "test@address.com";
            Role role = new Role("testname");
            #endregion
            userRoleUpdate = new UserRoleUpdate(address,role);
            #region Tests
            Assert.NotNull(userRoleUpdate);
            Assert.Equal(userRoleUpdate.Address,address);
            Assert.Equal(userRoleUpdate.Role,role);
            #endregion
        }
    }
}
