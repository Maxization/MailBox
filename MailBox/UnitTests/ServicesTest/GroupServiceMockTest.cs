
using MailBox.Database;
using MailBox.Models.GroupModels;
using MailBox.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests.ServicesTest
{
    public class GroupServiceMockTest
    {
        private GroupUser group0user0 = new GroupUser
        {
            GroupID = 0,
            UserID = 0,
            User = new User { ID = 0, FirstName = "SenderName1", LastName = "SenderSurname1", Email = "sender1@address.com" }
        };
        private GroupUser group0user1 = new GroupUser
        {
            GroupID = 0,
            UserID = 1,
            User = new User { ID = 1, FirstName = "SenderName2", LastName = "SenderSurname2", Email = "sender2@address.com" }
        };
        private GroupUser group0user2 = new GroupUser
        {
            GroupID = 0,
            UserID = 2,
            User = new User { ID = 2, FirstName = "SenderName0", LastName = "SenderSurname0", Email = "sender0@address.com" }
        };
        private GroupUser group1user2 = new GroupUser
        {
            GroupID = 1,
            UserID = 2,
            User = new User { ID = 2, FirstName = "SenderName0", LastName = "SenderSurname0", Email = "sender0@address.com" }
        };
        private GroupUser group1user4 = new GroupUser
        {
            GroupID = 1,
            UserID = 4,
            User = new User { ID = 4, FirstName = "SenderName4", LastName = "SenderSurname4", Email = "sender4@address.com" }
        };
        private GroupUser group1user9 = new GroupUser
        {
            GroupID = 1,
            UserID = 9,
            User = new User { ID = 9, FirstName = "SenderName9", LastName = "SenderSurname9", Email = "sender9@address.com" }
        };
        private GroupUser group2user2 = new GroupUser
        {
            GroupID = 2,
            UserID = 2,
            User = new User { ID = 2, FirstName = "SenderName0", LastName = "SenderSurname0", Email = "sender0@address.com" }
        };
        private GroupUser group2user3 = new GroupUser
        {
            GroupID = 2,
            UserID = 3,
            User = new User { ID = 3, FirstName = "SenderName3", LastName = "SenderSurname3", Email = "sender3@address.com" }
        };
        private GroupUser group2user9 = new GroupUser
        {
            GroupID = 2,
            UserID = 9,
            User = new User { ID = 9, FirstName = "SenderName9", LastName = "SenderSurname9", Email = "sender9@address.com" }
        };

        [Fact]
        public void AddUserToGroup_ValidCall()
        {
            var users = GetSampeUsers().AsQueryable();
            var groupsUsers = GetSampleGroupUsers().AsQueryable();


            var mockUsersSet = new Mock<DbSet<User>>();
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var mockGroupsUsersSet = new Mock<DbSet<GroupUser>>();
            mockGroupsUsersSet.As<IQueryable<GroupUser>>().Setup(m => m.Provider).Returns(groupsUsers.Provider);
            mockGroupsUsersSet.As<IQueryable<GroupUser>>().Setup(m => m.Expression).Returns(groupsUsers.Expression);
            mockGroupsUsersSet.As<IQueryable<GroupUser>>().Setup(m => m.ElementType).Returns(groupsUsers.ElementType);
            mockGroupsUsersSet.As<IQueryable<GroupUser>>().Setup(m => m.GetEnumerator()).Returns(groupsUsers.GetEnumerator());

            var mockContext = new Mock<MailBoxDBContext>();
            mockContext.Setup(c => c.Users).Returns(mockUsersSet.Object);
            mockContext.Setup(c => c.GroupUsers).Returns(mockGroupsUsersSet.Object);

            var service = new GroupService(mockContext.Object);

            GroupMemberUpdate groupMemberUpdate = new GroupMemberUpdate { GroupID = 0, GroupMemberAddress = "sender11@address.com" };

            service.AddUserToGroup(groupMemberUpdate);

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void ChangeGroupName_ValidCall()
        {
            var groups = GetSampleGroups().AsQueryable();

            var mockGroupsSet = new Mock<DbSet<Group>>();
            mockGroupsSet.As<IQueryable<Group>>().Setup(m => m.Provider).Returns(groups.Provider);
            mockGroupsSet.As<IQueryable<Group>>().Setup(m => m.Expression).Returns(groups.Expression);
            mockGroupsSet.As<IQueryable<Group>>().Setup(m => m.ElementType).Returns(groups.ElementType);
            mockGroupsSet.As<IQueryable<Group>>().Setup(m => m.GetEnumerator()).Returns(groups.GetEnumerator());

            var mockContext = new Mock<MailBoxDBContext>();
            mockContext.Setup(c => c.Groups).Returns(mockGroupsSet.Object);

            var service = new GroupService(mockContext.Object);

            GroupNameUpdate groupNameUpdate = new GroupNameUpdate { GroupID = 0, Name = "newname" };

            service.UpdateGroupName(groupNameUpdate);

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void DeleteGroup_ValidCall()
        {
            var groups = GetSampleGroups().AsQueryable();

            var mockGroupsSet = new Mock<DbSet<Group>>();
            mockGroupsSet.As<IQueryable<Group>>().Setup(m => m.Provider).Returns(groups.Provider);
            mockGroupsSet.As<IQueryable<Group>>().Setup(m => m.Expression).Returns(groups.Expression);
            mockGroupsSet.As<IQueryable<Group>>().Setup(m => m.ElementType).Returns(groups.ElementType);
            mockGroupsSet.As<IQueryable<Group>>().Setup(m => m.GetEnumerator()).Returns(groups.GetEnumerator());

            var mockContext = new Mock<MailBoxDBContext>();
            mockContext.Setup(c => c.Groups).Returns(mockGroupsSet.Object);

            var service = new GroupService(mockContext.Object);

            service.RemoveGroup(0);

            mockGroupsSet.Verify(m => m.Remove(It.IsAny<Group>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void AddGroup_ValidCall()
        {
            var users = GetSampeUsers().AsQueryable();

            var mockGroupsSet = new Mock<DbSet<Group>>();

            var mockUsersSet = new Mock<DbSet<User>>();
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockUsersSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var mockContext = new Mock<MailBoxDBContext>();
            mockContext.Setup(c => c.Users).Returns(mockUsersSet.Object);
            mockContext.Setup(c => c.Groups).Returns(mockGroupsSet.Object);

            var service = new GroupService(mockContext.Object);

            NewGroup newGroup = new NewGroup { Name = "newgroup" };

            service.AddGroup(9, newGroup);

            mockGroupsSet.Verify(m => m.Add(It.IsAny<Group>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void GetUserGroups_ValidCall()
        {
            var groups = GetSampleGroups().AsQueryable();

            var mockGroupsSet = new Mock<DbSet<Group>>();
            mockGroupsSet.As<IQueryable<Group>>().Setup(m => m.Provider).Returns(groups.Provider);
            mockGroupsSet.As<IQueryable<Group>>().Setup(m => m.Expression).Returns(groups.Expression);
            mockGroupsSet.As<IQueryable<Group>>().Setup(m => m.ElementType).Returns(groups.ElementType);
            mockGroupsSet.As<IQueryable<Group>>().Setup(m => m.GetEnumerator()).Returns(groups.GetEnumerator());

            var mockContext = new Mock<MailBoxDBContext>();
            mockContext.Setup(c => c.Groups).Returns(mockGroupsSet.Object);

            var service = new GroupService(mockContext.Object);

            var groupList10 = service.GetUserGroupsList(10);
            var groupList11 = service.GetUserGroupsList(11);

            Assert.Equal(2, groupList10.Count);
            Assert.Single(groupList11);
            Assert.Equal(3, groupList10[0].GroupMembers.Count);
            Assert.Equal(3, groupList10[1].GroupMembers.Count);
            Assert.Equal(3, groupList11[0].GroupMembers.Count);
        }

        private List<GroupUser> GetSampleGroupUsers()
        {
            List<GroupUser> groupUsers = new List<GroupUser>();
            foreach (GroupUser groupUser in GetSampleGroup0Users())
            {
                groupUsers.Add(groupUser);
            }
            foreach (GroupUser groupUser in GetSampleGroup1Users())
            {
                groupUsers.Add(groupUser);
            }
            foreach (GroupUser groupUser in GetSampleGroup2Users())
            {
                groupUsers.Add(groupUser);
            }
            return groupUsers;
        }

        private List<Group> GetSampleGroups()
        {
            List<Group> groups = new List<Group>();
            groups.Add(new Group
            {
                ID = 0,
                GroupName = "testname0",
                Owner = new User { ID = 10, FirstName = "SenderName10", LastName = "SenderSurname10", Email = "sender10@address.com" },
                GroupUsers = GetSampleGroup0Users()
            });
            groups.Add(new Group
            {
                ID = 1,
                GroupName = "testname1",
                Owner = new User { ID = 10, FirstName = "SenderName10", LastName = "SenderSurname10", Email = "sender10@address.com" },
                GroupUsers = GetSampleGroup1Users()
            });
            groups.Add(new Group
            {
                ID = 2,
                GroupName = "testname2",
                Owner = new User { ID = 11, FirstName = "SenderName11", LastName = "SenderSurname11", Email = "sender11@address.com" },
                GroupUsers = GetSampleGroup2Users()
            });
            return groups;
        }

        private List<GroupUser> GetSampleGroup0Users()
        {
            List<GroupUser> groupUsers = new List<GroupUser>();
            groupUsers.Add(group0user0);
            groupUsers.Add(group0user1);
            groupUsers.Add(group0user2);
            return groupUsers;
        }

        private List<GroupUser> GetSampleGroup1Users()
        {
            List<GroupUser> groupUsers = new List<GroupUser>();
            groupUsers.Add(group1user2);
            groupUsers.Add(group1user4);
            groupUsers.Add(group1user9);
            return groupUsers;
        }

        private List<GroupUser> GetSampleGroup2Users()
        {
            List<GroupUser> groupUsers = new List<GroupUser>();
            groupUsers.Add(group2user2);
            groupUsers.Add(group2user3);
            groupUsers.Add(group2user9);
            return groupUsers;
        }

        private List<User> GetSampeUsers()
        {
            List<User> users = new List<User>();
            users.Add(new User { ID = 0, FirstName = "SenderName1", LastName = "SenderSurname1", Email = "sender1@address.com", GroupUsers = new List<GroupUser> { group0user1 } });
            users.Add(new User { ID = 10, FirstName = "SenderName10", LastName = "SenderSurname10", Email = "sender10@address.com", GroupUsers = new List<GroupUser>() });
            users.Add(new User { ID = 1, FirstName = "SenderName2", LastName = "SenderSurname2", Email = "sender2@address.com", GroupUsers = new List<GroupUser> { group0user1 } });
            users.Add(new User { ID = 2, FirstName = "SenderName0", LastName = "SenderSurname0", Email = "sender0@address.com", GroupUsers = new List<GroupUser> { group0user2, group1user2, group2user2 } });
            users.Add(new User { ID = 3, FirstName = "SenderName3", LastName = "SenderSurname3", Email = "sender3@address.com", GroupUsers = new List<GroupUser> { group2user3 } });
            users.Add(new User { ID = 11, FirstName = "SenderName11", LastName = "SenderSurname11", Email = "sender11@address.com", GroupUsers = new List<GroupUser>() });
            users.Add(new User { ID = 4, FirstName = "SenderName4", LastName = "SenderSurname4", Email = "sender4@address.com", GroupUsers = new List<GroupUser> { group1user4 } });
            users.Add(new User { ID = 9, FirstName = "SenderName9", LastName = "SenderSurname9", Email = "sender9@address.com", GroupUsers = new List<GroupUser> { group2user9, group1user9 } });
            return users;
        }
    }
}
