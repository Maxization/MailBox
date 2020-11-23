
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
        MailBoxDBContext _context;

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
                    RecipientsAddresses = GetMailRecipients(um.Mail.ID),
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
            var mail =_context.UserMails
                .Include(x => x.Mail)
                .ThenInclude(x => x.Sender)
                .Where(x => x.MailID == mailID && x.UserID == userID)
                .FirstOrDefault();
            if (mail == null) return null;
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
                RecipientsAddresses = GetMailRecipients(mailID),
                Topic = mail.Mail.Topic,
                Text = mail.Mail.Text,
                Date = mail.Mail.Date,
            };
        }

        private List<string> GetMailRecipients(int mailID)
        {
            var userMails = _context.UserMails
                .Include(x => x.User)
                .Where(x => x.MailID == mailID)
                .AsQueryable();

            List<string> recipients = new List<string>();
            foreach(UserMail um in userMails)
            {
                if(um.RecipientType == RecipientType.CC)
                    recipients.Add(um.User.Email);
            }

            return recipients;
        }

        public async void CreateMail(int userID, NewMail newMail)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

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
                await _context.SaveChangesAsync();

                foreach (string email in newMail.BCCRecipientsAddresses)
                {
                    usr = _context.Users.Where(x => x.Email == email).FirstOrDefault();
                    UserMail um = new UserMail
                    {
                        UserID = usr.ID,
                        MailID = mail.ID,
                        RecipientType = RecipientType.BCC,
                        Read = false,
                    };
                    _context.UserMails.Add(um);
                }

                foreach (string email in newMail.CCRecipientsAddresses)
                {
                    usr = _context.Users.Where(x => x.Email == email).FirstOrDefault();
                    UserMail um = new UserMail
                    {
                        UserID = usr.ID,
                        MailID = mail.ID,
                        RecipientType = RecipientType.CC,
                        Read = false,
                    };
                    _context.UserMails.Add(um);
                }

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch(Exception)
            {
                await transaction.RollbackAsync();
            }
            
        }
    }
}
