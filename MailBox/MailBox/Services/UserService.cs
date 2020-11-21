using MailBox.Database;
using MailBox.Models;
using MailBox.Models.UserModels;
using MailBox.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Services
{
    public class UserService : IUserService
    {
        MailBoxDBContext _context;

        public UserService(MailBoxDBContext context)
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
            foreach(var um in userMails)
            {
                MailInboxView miv = new MailInboxView
                {
                    MailId = um.MailID,
                    Read = um.Read,
                    Sender = new UserGlobalView(um.Mail.Sender.FirstName, um.Mail.Sender.LastName, um.Mail.Sender.Email),
                    RecipientsAddresses = GetMailRecipients(um.Mail.ID),
                    Topic = um.Mail.Topic,
                    Text = um.Mail.Text,
                    Date = um.Mail.Date,
                    MailReply = null,
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
                MailId = mail.Mail.ID,
                Read = mail.Read,
                Sender = new UserGlobalView(mail.Mail.Sender.FirstName, mail.Mail.Sender.LastName, mail.Mail.Sender.Email),
                RecipientsAddresses = GetMailRecipients(mailID),
                Topic = mail.Mail.Topic,
                Text = mail.Mail.Text,
                Date = mail.Mail.Date,
                MailReply = null,
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

        public void CreateMail(int userID, NewMail mail)
        {
            //await using var transaction = await context.Database.BeginTransactionAsync();
            throw new NotImplementedException();
        }
    }
}
