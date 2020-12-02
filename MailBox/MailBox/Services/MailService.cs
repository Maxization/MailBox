
using MailBox.Database;
using MailBox.Models.MailModels;
using MailBox.Models.UserModels;
using MailBox.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailBox.Services
{
    public class MailService : IMailService
    {
        private readonly MailBoxDBContext _context;

        public MailService(MailBoxDBContext context)
        {
            _context = context;
        }

        public List<MailInboxView> GetUserMails(int userID)
        {
            var userMails = _context.UserMails
                .Include(x => x.Mail)
                .ThenInclude(x => x.Sender)
                .Where(x => x.UserID == userID).AsQueryable();

            List<MailInboxView> result = new List<MailInboxView>();
            foreach (var um in userMails)
            {
                MailInboxView miv = new MailInboxView
                {
                    MailID = um.MailID,
                    Read = um.Read,
                    Sender = new UserGlobalView
                    {
                        Name = um.Mail.Sender.FirstName,
                        Surname = um.Mail.Sender.LastName,
                        Address = um.Mail.Sender.Email
                    },
                    RecipientsAddresses = GetMailRecipients(userID, um.Mail.ID),
                    Topic = um.Mail.Topic,
                    Text = um.Mail.Text,
                    Date = um.Mail.Date,
                };
                result.Add(miv);
            }

            return result;
        }

        public MailInboxView GetMail(int userID, int mailID)
        {
            var mail = _context.UserMails
                .Include(x => x.Mail)
                .ThenInclude(x => x.Sender)
                .Where(x => x.MailID == mailID && x.UserID == userID)
                .FirstOrDefault();
            if (mail == null)
                return null;
            return new MailInboxView
            {
                MailID = mail.Mail.ID,
                Read = mail.Read,
                Sender = new UserGlobalView
                {
                    Name = mail.Mail.Sender.FirstName,
                    Surname = mail.Mail.Sender.LastName,
                    Address = mail.Mail.Sender.Email
                },
                RecipientsAddresses = GetMailRecipients(userID, mailID),
                Topic = mail.Mail.Topic,
                Text = mail.Mail.Text,
                Date = mail.Mail.Date,
            };
        }

        private List<string> GetMailRecipients(int userID, int mailID)
        {
            var userMails = _context.UserMails
                .Include(x => x.User)
                .Where(x => x.MailID == mailID)
                .ToList();

            List<string> recipients = new List<string>();
            var user = _context.Users.Find(userID);
            recipients.Add(user.Email);
            foreach (UserMail um in userMails)
            {
                if (um.RecipientType == RecipientType.CC)
                    if (um.UserID != userID)
                        recipients.Add(um.User.Email);
            }

            return recipients;
        }

        public void CreateMail(int userID, NewMail newMail)
        {
            if (newMail.BCCRecipientsAddresses != null)
                newMail.BCCRecipientsAddresses = newMail.BCCRecipientsAddresses.Distinct().ToList();
            if (newMail.CCRecipientsAddresses != null)
                newMail.CCRecipientsAddresses = newMail.CCRecipientsAddresses.Distinct().ToList();

            var users = _context.Users.ToList();
            List<string> emails = new List<string>();
            foreach (User usr in users)
                emails.Add(usr.Email);

            if (newMail.BCCRecipientsAddresses != null)
                foreach (string email in newMail.BCCRecipientsAddresses)
                {
                    if (!emails.Contains(email))
                        throw new Exception("BCCRecipientsAddresses", new Exception("No such email in global contacts list."));
                }

            if (newMail.CCRecipientsAddresses != null)
                foreach (string email in newMail.CCRecipientsAddresses)
                {
                    if (!emails.Contains(email))
                        throw new Exception("CCRecipientsAddresses", new Exception("No such email in global contacts list."));
                }

            var transaction = _context.Database.BeginTransaction();

            try
            {
                User usr = _context.Users.Find(userID);
                Mail mail = new Mail
                {
                    Date = DateTime.Now,
                    Topic = newMail.Topic,
                    Text = newMail.Text,
                    Sender = usr,
                };
                _context.Mails.Add(mail);
                _context.SaveChanges();

                if (newMail.CCRecipientsAddresses != null)
                    foreach (string email in newMail.CCRecipientsAddresses)
                    {
                        usr = _context.Users.Where(x => x.Email == email).FirstOrDefault();
                        if (usr == null)
                            continue;
                        UserMail um = new UserMail
                        {
                            UserID = usr.ID,
                            MailID = mail.ID,
                            RecipientType = RecipientType.CC,
                            Read = false,
                        };
                        _context.UserMails.Add(um);
                    }

                if (newMail.BCCRecipientsAddresses != null)
                    foreach (string email in newMail.BCCRecipientsAddresses)
                    {
                        usr = _context.Users.Where(x => x.Email == email).FirstOrDefault();
                        if (usr == null || (newMail.CCRecipientsAddresses != null && newMail.CCRecipientsAddresses.Contains(email)))
                            continue;
                        UserMail um = new UserMail
                        {
                            UserID = usr.ID,
                            MailID = mail.ID,
                            RecipientType = RecipientType.BCC,
                            Read = false,
                        };
                        _context.UserMails.Add(um);
                    }

                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }

        }

        public void UpdateMailRead(int userID, MailReadUpdate mailReadUpdate)
        {
            Mail mail = _context.Mails.Find(mailReadUpdate.MailID);
            UserMail userMail = _context.UserMails.Where(um => um.MailID == mailReadUpdate.MailID && um.UserID == userID).First();
            userMail.Read = mailReadUpdate.Read;
            _context.SaveChanges();
        }
    }
}
