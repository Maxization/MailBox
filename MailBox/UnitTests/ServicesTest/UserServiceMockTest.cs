
using MailBox.Database;
using MailBox.Models.UserModels;
using MailBox.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests.ServicesTest
{
    public class UserServiceMockTest
    {
        [Fact]
        public void GetGlobalContactList_ValidCall()
        {
            var users = GetSampleUsers().AsQueryable();

            var mockUsersSet = new Mock<DbSet<User>>();
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var mockNotificationService = new Mock<NotificationService>();

            var mockContext = new Mock<MailBoxDBContext>();
            mockContext.Setup(c => c.Users).Returns(mockUsersSet.Object);

            var service = new UserService(mockContext.Object, mockNotificationService.Object);

            var globalList = service.GetGlobalContactList();

            Assert.Equal(4, globalList.Count);
            Assert.Equal("test1@address.com", globalList[0].Address);
            Assert.Equal("TestName2", globalList[1].Name);
            Assert.Equal("TestSurname3", globalList[2].Surname);
            Assert.Equal("test7@address.com", globalList[3].Address);
        }

        [Fact]
        public void GetAdminViewList_ValidCall()
        {
            var users = GetSampleUsers().AsQueryable();

            var mockUsersSet = new Mock<DbSet<User>>();
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var mockNotificationService = new Mock<NotificationService>();

            var mockContext = new Mock<MailBoxDBContext>();
            mockContext.Setup(c => c.Users).Returns(mockUsersSet.Object);

            var service = new UserService(mockContext.Object, mockNotificationService.Object);

            var adminViewList = service.GetAdminViewList();

            Assert.Equal(7, adminViewList.Count);
            Assert.Equal("Admin", adminViewList[0].RoleName);
            Assert.Equal("TestName2", adminViewList[1].Name);
            Assert.Equal("TestSurname3", adminViewList[2].Surname);
            Assert.Equal("New", adminViewList[3].RoleName);
            Assert.Equal("Banned", adminViewList[4].RoleName);
            Assert.Equal("test6@address.com", adminViewList[5].Address);
            Assert.Equal("User", adminViewList[6].RoleName);
        }

        [Fact]
        public void UpdateUserRole_ValidCall()
        {
            var users = GetSampleUsers().AsQueryable();
            var roles = GetSampleUserRoles().AsQueryable();

            var mockUserSet = new Mock<DbSet<User>>();
            mockUserSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var mockRolesSet = new Mock<DbSet<UserRole>>();
            mockRolesSet.As<IQueryable<UserRole>>().Setup(m => m.Provider).Returns(roles.Provider);
            mockRolesSet.As<IQueryable<UserRole>>().Setup(m => m.Expression).Returns(roles.Expression);
            mockRolesSet.As<IQueryable<UserRole>>().Setup(m => m.ElementType).Returns(roles.ElementType);
            mockRolesSet.As<IQueryable<UserRole>>().Setup(m => m.GetEnumerator()).Returns(roles.GetEnumerator());

            var mockContext = new Mock<MailBoxDBContext>();
            mockContext.Setup(c => c.Users).Returns(mockUserSet.Object);
            mockContext.Setup(c => c.Roles).Returns(mockRolesSet.Object);

            var mockNotificationService = new Mock<NotificationService>();

            var service = new UserService(mockContext.Object, mockNotificationService.Object);
            UserRoleUpdate userRoleUpdate = new UserRoleUpdate
            {
                Address = "test4@address.com",
                RoleName = "User"
            };
            service.UpdateUserRole(userRoleUpdate);

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        private List<UserRole> GetSampleUserRoles()
        {
            List<UserRole> userRoles = new List<UserRole>();
            userRoles.Add(
                new UserRole
                {
                    ID = 1,
                    RoleName = "Admin"
                });
            userRoles.Add(
                new UserRole
                {
                    ID = 2,
                    RoleName = "User"
                });
            userRoles.Add(
                new UserRole
                {
                    ID = 3,
                    RoleName = "New"
                });
            userRoles.Add(
                new UserRole
                {
                    ID = 4,
                    RoleName = "Banned"
                });
            return userRoles;
        }

        private List<User> GetSampleUsers()
        {
            List<User> users = new List<User>();
            users.Add(
                new User
                {
                    FirstName = "TestName1",
                    LastName = "TestSurname1",
                    Email = "test1@address.com",
                    ID = 0,
                    Role = new UserRole { ID = 1, RoleName = "Admin" },
                });
            users.Add(
                new User
                {
                    FirstName = "TestName2",
                    LastName = "TestSurname2",
                    Email = "test2@address.com",
                    ID = 1,
                    Role = new UserRole { ID = 2, RoleName = "User" },
                });
            users.Add(
                new User
                {
                    FirstName = "TestName3",
                    LastName = "TestSurname3",
                    Email = "test3@address.com",
                    ID = 2,
                    Role = new UserRole { ID = 2, RoleName = "User" },
                });
            users.Add(
                new User
                {
                    FirstName = "TestName4",
                    LastName = "TestSurname4",
                    Email = "test4@address.com",
                    ID = 3,
                    Role = new UserRole { ID = 3, RoleName = "New" },
                });
            users.Add(
                new User
                {
                    FirstName = "TestName5",
                    LastName = "TestSurname5",
                    Email = "test5@address.com",
                    ID = 4,
                    Role = new UserRole { ID = 4, RoleName = "Banned" },
                });
            users.Add(
                new User
                {
                    FirstName = "TestName6",
                    LastName = "TestSurname6",
                    Email = "test6@address.com",
                    ID = 5,
                    Role = new UserRole { ID = 4, RoleName = "Banned" },
                });
            users.Add(
                new User
                {
                    FirstName = "TestName7",
                    LastName = "TestSurname7",
                    Email = "test7@address.com",
                    ID = 6,
                    Role = new UserRole { ID = 2, RoleName = "User" },
                });
            return users;
        }
    }
}
