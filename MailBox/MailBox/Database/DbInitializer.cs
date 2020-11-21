using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Database
{
    public static class DbInitializer
    {
        public static void Initialize(MailBoxDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            var roles = new UserRole[]
            {
                new UserRole{RoleName="User"},
                new UserRole{RoleName="Admin"}
            };
            foreach(UserRole r in roles)
            {
                context.Roles.Add(r);
            }
            context.SaveChanges();

            var users = new User[]
            {
                new User{FirstName="Marian", LastName="Kowalski", Email="marian@kowalski.pl", Role=roles[0]},
                new User{FirstName="Piotr", LastName="Kaczmarczyk", Email="pioter@wp.pl", Role=roles[0]},
                new User{FirstName="Heronim", LastName="Szulc", Email="heronius@gmail.com", Role=roles[0]},
                new User{FirstName="Borys", LastName="Woźniak", Email="borat@onet.pl", Role=roles[0]},
                new User{FirstName="Adam", LastName="Adminowski", Email="admin@mailbox.pl", Role=roles[1]},
            };
            foreach(User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

            var groups = new Group[]
            {
                new Group{GroupName="Friends", Owner=users[0]},
                new Group{GroupName="Work", Owner=users[0]},
                new Group{GroupName="Friends", Owner=users[1]},
                new Group{GroupName="Noobs", Owner=users[1]},
                new Group{GroupName="All", Owner=users[2]},
                new Group{GroupName="All", Owner=users[3]},
                new Group{GroupName="All", Owner=users[4]}
            };
            foreach(Group g in groups)
            {
                context.Groups.Add(g);
            }
            context.SaveChanges();

            var groupUsers = new GroupUser[]
            {
                new GroupUser{GroupID=1, UserID=3},
                new GroupUser{GroupID=1, UserID=2},
                new GroupUser{GroupID=2, UserID=3},
                new GroupUser{GroupID=3, UserID=4},
                new GroupUser{GroupID=4, UserID=1},
                new GroupUser{GroupID=5, UserID=1}
            };
            foreach(GroupUser gu in groupUsers)
            {
                context.GroupUsers.Add(gu);
            }
            context.SaveChanges();

            var mails = new Mail[]
            {
                new Mail{Topic="Hej", Text="Nowa wiadomosc", Date=DateTime.Parse("2019-09-01"), Sender=users[0]},
                new Mail{Topic="Poważna wiadomość", Text="Nowa wiadomosc", Date=DateTime.Parse("2020-10-03"), Sender=users[0]},
                new Mail{Topic="Ważna informacja", Text="Nowa wiadomosc", Date=DateTime.Parse("2020-11-04"), Sender=users[1]},
                new Mail{Topic="Sprawy Organizacyjne", Text="Nowa wiadomosc", Date=DateTime.Parse("2020-10-15"), Sender=users[2]},
                new Mail{Topic="Gk Lab - zadanie2", Text="Zadanie do oddania za tydzien", Date=DateTime.Parse("2020-11-07"), Sender=users[3]},
                new Mail{Topic="Jutrzejszy wykład", Text="Jutrzejszy wykład zostaje przełożony", Date=DateTime.Parse("2020-10-20"), Sender=users[4]}
            };
            foreach(Mail m in mails)
            {
                context.Mails.Add(m);
            }
            context.SaveChanges();

            //var mailReplies = new Mail[]
            //{
            //    new Mail{Topic="Czesc", Text="Nowa wiadomosc", Date=DateTime.Parse("2020-11-15"), Sender=users[1], MailReply=mails[0]},
            //    new Mail{Topic="Siemanko", Text="Nowa wiadomosc", Date=DateTime.Parse("2020-09-15"), Sender=users[2], MailReply=mails[0]},
                
            //};
            //foreach (Mail m in mailReplies)
            //{
            //    context.Mails.Add(m);
            //}
            //context.SaveChanges();

            //var mailReplies2 = new Mail[]
            //{
            //    new Mail{Topic="Co tam?", Text="Nowa wiadomosc", Date=DateTime.Parse("2020-09-20"), Sender=users[3], MailReply=mailReplies[0]},
            //    new Mail{Topic="Jak tam?", Text="Nowa wiadomosc", Date=DateTime.Parse("2020-09-01"), Sender=users[4], MailReply=mailReplies[1]},
            //};
            //foreach (Mail m in mailReplies2)
            //{
            //    context.Mails.Add(m);
            //}
            //context.SaveChanges();

            var userMails = new UserMail[]
            {
                new UserMail{UserID=1,MailID=1, RecipientType=RecipientType.BCC, Read = true},
                new UserMail{UserID=2,MailID=2, RecipientType=RecipientType.BCC, Read = true},
                new UserMail{UserID=3,MailID=3, RecipientType=RecipientType.BCC, Read = false},
                new UserMail{UserID=4,MailID=4, RecipientType=RecipientType.BCC, Read = true},
                new UserMail{UserID=5,MailID=5, RecipientType=RecipientType.BCC, Read = true},
                new UserMail{UserID=3,MailID=6, RecipientType=RecipientType.BCC, Read = false},
                new UserMail{UserID=4,MailID=7, RecipientType=RecipientType.CC, Read = true},
                new UserMail{UserID=4,MailID=8, RecipientType=RecipientType.CC, Read = false},
                new UserMail{UserID=5,MailID=9, RecipientType=RecipientType.BCC, Read = false},
                new UserMail{UserID=1,MailID=10, RecipientType=RecipientType.CC, Read = true},
            };
            foreach (UserMail um in userMails)
            {
                context.UserMails.Add(um);
            }
            context.SaveChanges();

        }
    }
}
