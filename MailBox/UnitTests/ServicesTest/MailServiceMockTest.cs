
using MailBox.Database;
using MailBox.Models.MailModels;
using MailBox.Services;
using MailBox.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests.ServicesTest
{
    public class MailServiceMockTest
    {
        [Fact]
        public void GetUserMails_ValidCall()
        {
            var mails = GetSampleUserMails().AsQueryable();
            var users = GetSamlpeUsers();

            var mockMailsSet = new Mock<DbSet<UserMail>>();
            mockMailsSet.As<IQueryable<UserMail>>().Setup(m => m.Provider).Returns(mails.Provider);
            mockMailsSet.As<IQueryable<UserMail>>().Setup(m => m.Expression).Returns(mails.Expression);
            mockMailsSet.As<IQueryable<UserMail>>().Setup(m => m.ElementType).Returns(mails.ElementType);
            mockMailsSet.As<IQueryable<UserMail>>().Setup(m => m.GetEnumerator()).Returns(mails.GetEnumerator());

            var mockUsersSet = new Mock<DbSet<User>>();
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var mockNotificationService = new Mock<NotificationService>();

            var mockContext = new Mock<MailBoxDBContext>();
            mockContext.Setup(c => c.UserMails).Returns(mockMailsSet.Object);
            mockContext.Setup(c => c.Users).Returns(mockUsersSet.Object);

            var mockConfiguration = new Mock<IConfiguration>();

            var service = new MailService(mockConfiguration.Object, mockContext.Object, mockNotificationService.Object);

            var user9Mais = service.GetUserMails(9, 1, SortingEnum.ByDateFromNewest, FilterEnum.NoFilter, null);
            var user10Mais = service.GetUserMails(10, 1, SortingEnum.BySenderAZ, FilterEnum.FilterTopic, "testtopic");
            var user11Mais = service.GetUserMails(11, 1, SortingEnum.ByTopicZA, FilterEnum.FilterSenderName, "");

            Assert.Empty(user9Mais.Mails);
            Assert.Equal(5, user10Mais.Mails.Count);
            Assert.Single(user11Mais.Mails);
            Assert.Equal("SenderName2", user10Mais.Mails[2].Sender.Name);
            Assert.Equal("sender3@address.com", user11Mais.Mails[0].Sender.Address);
        }

        [Fact]
        public void GetMail_ValidCall()
        {
            var mails = GetSampleUserMails().AsQueryable();
            var users = GetSamlpeUsers();
            var attachment = new List<Attachment>();

            var mockMailsSet = new Mock<DbSet<UserMail>>();
            mockMailsSet.As<IQueryable<UserMail>>().Setup(m => m.Provider).Returns(mails.Provider);
            mockMailsSet.As<IQueryable<UserMail>>().Setup(m => m.Expression).Returns(mails.Expression);
            mockMailsSet.As<IQueryable<UserMail>>().Setup(m => m.ElementType).Returns(mails.ElementType);
            mockMailsSet.As<IQueryable<UserMail>>().Setup(m => m.GetEnumerator()).Returns(mails.GetEnumerator());

            var mockUsersSet = new Mock<DbSet<User>>();
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var mockAttachmentsSet = new Mock<DbSet<Attachment>>();
            mockAttachmentsSet.As<IQueryable<Attachment>>().Setup(m => m.Provider).Returns(attachment.AsQueryable().Provider);
            mockAttachmentsSet.As<IQueryable<Attachment>>().Setup(m => m.Expression).Returns(attachment.AsQueryable().Expression);
            mockAttachmentsSet.As<IQueryable<Attachment>>().Setup(m => m.ElementType).Returns(attachment.AsQueryable().ElementType);
            mockAttachmentsSet.As<IQueryable<Attachment>>().Setup(m => m.GetEnumerator()).Returns(attachment.GetEnumerator());

            var mockNotificationService = new Mock<NotificationService>();

            var mockContext = new Mock<MailBoxDBContext>();
            mockContext.Setup(c => c.UserMails).Returns(mockMailsSet.Object);
            mockContext.Setup(c => c.Users).Returns(mockUsersSet.Object);
            mockContext.Setup(c => c.Attachments).Returns(mockAttachmentsSet.Object);
            var mockConfiguration = new Mock<IConfiguration>();

            var service = new MailService(mockConfiguration.Object, mockContext.Object, mockNotificationService.Object);

            var user10Mais = service.GetMail(10, 3);
            var user11Mais = service.GetMail(11, 3);

            Assert.Equal("sender3@address.com", user10Mais.Sender.Address);
            Assert.Equal("sender3@address.com", user11Mais.Sender.Address);
            Assert.Equal("testtext3", user10Mais.Text);
            Assert.Equal("testtext3", user11Mais.Text);
        }

        [Fact]
        public void BCCandCCTest_ValidCall()
        {
            var mails = GetSampleUserMails().AsQueryable();
            var users = GetSamlpeUsers();
            var attachment = new List<Attachment>();

            var mockMailsSet = new Mock<DbSet<UserMail>>();
            mockMailsSet.As<IQueryable<UserMail>>().Setup(m => m.Provider).Returns(mails.Provider);
            mockMailsSet.As<IQueryable<UserMail>>().Setup(m => m.Expression).Returns(mails.Expression);
            mockMailsSet.As<IQueryable<UserMail>>().Setup(m => m.ElementType).Returns(mails.ElementType);
            mockMailsSet.As<IQueryable<UserMail>>().Setup(m => m.GetEnumerator()).Returns(mails.GetEnumerator());

            var mockUsersSet = new Mock<DbSet<User>>();
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var mockAttachmentsSet = new Mock<DbSet<Attachment>>();
            mockAttachmentsSet.As<IQueryable<Attachment>>().Setup(m => m.Provider).Returns(attachment.AsQueryable().Provider);
            mockAttachmentsSet.As<IQueryable<Attachment>>().Setup(m => m.Expression).Returns(attachment.AsQueryable().Expression);
            mockAttachmentsSet.As<IQueryable<Attachment>>().Setup(m => m.ElementType).Returns(attachment.AsQueryable().ElementType);
            mockAttachmentsSet.As<IQueryable<Attachment>>().Setup(m => m.GetEnumerator()).Returns(attachment.GetEnumerator());

            var mockContext = new Mock<MailBoxDBContext>();
            mockContext.Setup(c => c.UserMails).Returns(mockMailsSet.Object);
            mockContext.Setup(c => c.Users).Returns(mockUsersSet.Object);
            mockContext.Setup(c => c.Attachments).Returns(mockAttachmentsSet.Object);

            var mockNotificationService = new Mock<NotificationService>();

            var mockConfiguration = new Mock<IConfiguration>();

            var service = new MailService(mockConfiguration.Object, mockContext.Object, mockNotificationService.Object);

            var user10Mais = service.GetMail(10, 3);
            var user11Mais = service.GetMail(11, 3);

            Assert.Equal(2, user10Mais.RecipientsAddresses.Count);
            Assert.Single(user11Mais.RecipientsAddresses);
            Assert.Equal("sender11@address.com", user11Mais.RecipientsAddresses[0]);
            Assert.Equal("sender10@address.com", user10Mais.RecipientsAddresses[0]);
            Assert.Equal("sender11@address.com", user10Mais.RecipientsAddresses[1]);
        }

        [Fact]
        public void UpdateMailRead_ValidCall()
        {
            var userMails = GetSampleUserMails().AsQueryable();

            var mockUserMailsSet = new Mock<DbSet<UserMail>>();
            mockUserMailsSet.As<IQueryable<UserMail>>().Setup(m => m.Provider).Returns(userMails.Provider);
            mockUserMailsSet.As<IQueryable<UserMail>>().Setup(m => m.Expression).Returns(userMails.Expression);
            mockUserMailsSet.As<IQueryable<UserMail>>().Setup(m => m.ElementType).Returns(userMails.ElementType);
            mockUserMailsSet.As<IQueryable<UserMail>>().Setup(m => m.GetEnumerator()).Returns(userMails.GetEnumerator());

            var mockNotificationService = new Mock<NotificationService>();

            var mockContext = new Mock<MailBoxDBContext>();
            mockContext.Setup(c => c.UserMails).Returns(mockUserMailsSet.Object);

            var mockConfiguration = new Mock<IConfiguration>();

            var service = new MailService(mockConfiguration.Object, mockContext.Object, mockNotificationService.Object);

            MailReadUpdate mailReadUpdate = new MailReadUpdate { MailID = 3, Read = true };

            service.UpdateMailRead(11, mailReadUpdate);

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        private List<User> GetSamlpeUsers()
        {
            List<User> users = new List<User>
            {
                new User { ID = 0, FirstName = "SenderName1", LastName = "SenderSurname1", Email = "sender1@address.com" },
                new User { ID = 10, FirstName = "SenderName10", LastName = "SenderSurname10", Email = "sender10@address.com" },
                new User { ID = 1, FirstName = "SenderName2", LastName = "SenderSurname2", Email = "sender2@address.com" },
                new User { ID = 2, FirstName = "SenderName0", LastName = "SenderSurname0", Email = "sender0@address.com" },
                new User { ID = 3, FirstName = "SenderName3", LastName = "SenderSurname3", Email = "sender3@address.com" },
                new User { ID = 11, FirstName = "SenderName11", LastName = "SenderSurname11", Email = "sender11@address.com" },
                new User { ID = 4, FirstName = "SenderName4", LastName = "SenderSurname4", Email = "sender4@address.com" },
                new User { ID = 9, FirstName = "SenderName9", LastName = "SenderSurname9", Email = "sender9@address.com" }
            };
            return users;
        }

        private List<UserMail> GetSampleUserMails()
        {
            List<UserMail> mails = new List<UserMail>
            {
                new UserMail
                {
                    Mail = new Mail
                    {
                        ID = 0,
                        Date = new DateTime(),
                        Sender = new User { ID = 0, FirstName = "SenderName1", LastName = "SenderSurname1", Email = "sender1@address.com" },
                        Text = "testtext0",
                        Topic = "testtopic0"
                    },
                    Read = false,
                    User = new User { ID = 10, FirstName = "SenderName10", LastName = "SenderSurname10", Email = "sender10@address.com" },
                    RecipientType = RecipientType.CC,
                    UserID = 10,
                    MailID = 0
                },
                new UserMail
                {
                    Mail = new Mail
                    {
                        ID = 1,
                        Date = new DateTime(),
                        Sender = new User { ID = 1, FirstName = "SenderName2", LastName = "SenderSurname2", Email = "sender2@address.com" },
                        Text = "testtext1",
                        Topic = "testtopic1"
                    },
                    Read = true,
                    User = new User { ID = 10, FirstName = "SenderName10", LastName = "SenderSurname10", Email = "sender10@address.com" },
                    RecipientType = RecipientType.CC,
                    UserID = 10,
                    MailID = 1
                },
                new UserMail
                {
                    Mail = new Mail
                    {
                        ID = 2,
                        Date = new DateTime(),
                        Sender = new User { ID = 2, FirstName = "SenderName0", LastName = "SenderSurname0", Email = "sender0@address.com" },
                        Text = "testtext2",
                        Topic = "testtopic2"
                    },
                    Read = true,
                    User = new User { ID = 10, FirstName = "SenderName10", LastName = "SenderSurname10", Email = "sender10@address.com" },
                    RecipientType = RecipientType.BCC,
                    UserID = 10,
                    MailID = 2
                },
                new UserMail
                {
                    Mail = new Mail
                    {
                        ID = 3,
                        Date = new DateTime(),
                        Sender = new User { ID = 3, FirstName = "SenderName3", LastName = "SenderSurname3", Email = "sender3@address.com" },
                        Text = "testtext3",
                        Topic = "testtopic3"
                    },
                    Read = true,
                    User = new User { ID = 10, FirstName = "SenderName10", LastName = "SenderSurname10", Email = "sender10@address.com" },
                    RecipientType = RecipientType.BCC,
                    UserID = 10,
                    MailID = 3
                },
                new UserMail
                {
                    Mail = new Mail
                    {
                        ID = 3,
                        Date = new DateTime(),
                        Sender = new User { ID = 3, FirstName = "SenderName3", LastName = "SenderSurname3", Email = "sender3@address.com" },
                        Text = "testtext3",
                        Topic = "testtopic3"
                    },
                    Read = false,
                    User = new User { ID = 11, FirstName = "SenderName11", LastName = "SenderSurname11", Email = "sender11@address.com" },
                    RecipientType = RecipientType.CC,
                    UserID = 11,
                    MailID = 3
                },
                new UserMail
                {
                    Mail = new Mail
                    {
                        ID = 4,
                        Date = new DateTime(),
                        Sender = new User { ID = 4, FirstName = "SenderName4", LastName = "SenderSurname4", Email = "sender4@address.com" },
                        Text = "testtext4",
                        Topic = "testtopic4"
                    },
                    Read = true,
                    User = new User { ID = 10, FirstName = "SenderName10", LastName = "SenderSurname10", Email = "sender10@address.com" },
                    RecipientType = RecipientType.BCC,
                    UserID = 10,
                    MailID = 4
                }
            };
            return mails;
        }
    }
}
