
using MailBox.Database;
using MailBox.Models.MailModels;
using MailBox.Models.UserModels;
using MailBox.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Azure.Storage.Blobs;

namespace MailBox.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        private readonly MailBoxDBContext _context;
        private readonly INotificationService _notificationService;

        public MailService(IConfiguration configuration, MailBoxDBContext context, INotificationService notificationService)
        {
            _context = context;
            _configuration = configuration;
            _notificationService = notificationService;
        }

        public PagingMailInboxView GetUserMails(int userID, int page, SortingEnum sorting, FilterEnum filter, string filterPhrase)
        {
            filterPhrase ??= "";

            bool firstPage = (page == 1);

            var userMails = _context.UserMails
                .Include(x => x.Mail)
                .ThenInclude(x => x.Sender)
                .Where(x => x.UserID == userID).AsQueryable();

            switch (filter)
            {
                case FilterEnum.FilterTopic:
                    userMails = userMails.Where(x => x.Mail.Topic.StartsWith(filterPhrase)).AsQueryable();
                    break;
                case FilterEnum.FilterSenderName:
                    userMails = userMails.Where(x => x.Mail.Sender.FirstName.StartsWith(filterPhrase)).AsQueryable();
                    break;
                case FilterEnum.FilterSenderSurname:
                    userMails = userMails.Where(x => x.Mail.Sender.LastName.StartsWith(filterPhrase)).AsQueryable();
                    break;
            }

            switch (sorting)
            {
                case SortingEnum.ByDateFromNewest:
                    userMails = userMails.OrderByDescending(x => x.Mail.Date);
                    break;
                case SortingEnum.ByDateFromOldest:
                    userMails = userMails.OrderBy(x => x.Mail.Date);
                    break;
                case SortingEnum.BySenderAZ:
                    userMails = userMails.OrderBy(x => x.Mail.Sender.FirstName).ThenBy(x => x.Mail.Sender.LastName).ThenByDescending(x => x.Mail.Date);
                    break;
                case SortingEnum.BySenderZA:
                    userMails = userMails.OrderByDescending(x => x.Mail.Sender.FirstName).ThenByDescending(x => x.Mail.Sender.LastName).ThenByDescending(x => x.Mail.Date);
                    break;
                case SortingEnum.ByTopicAZ:
                    userMails = userMails.OrderBy(x => x.Mail.Topic).ThenByDescending(x => x.Mail.Date);
                    break;
                case SortingEnum.ByTopicZA:
                    userMails = userMails.OrderByDescending(x => x.Mail.Topic).ThenByDescending(x => x.Mail.Date);
                    break;
            }

            bool lastPage = (userMails.Count() <= 5 * page);

            userMails = userMails.Skip((page - 1) * 5).Take(5);

            List<MailInboxView> mails = new List<MailInboxView>();
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
                    Topic = um.Mail.Topic,
                    Date = um.Mail.Date,
                };
                mails.Add(miv);
            }

            return new PagingMailInboxView { Mails = mails, FirstPage = firstPage, LastPage = lastPage };
        }

        public void UpdateMailRead(int userID, MailReadUpdate mailRead)
        {
            UserMail userMail = _context.UserMails.Where(um => um.MailID == mailRead.MailID && um.UserID == userID).First();
            userMail.Read = mailRead.Read;
            _context.SaveChanges();
        }

        public MailDetailsView GetMail(int userID, int mailID)
        {
            var mail = _context.UserMails
                .Include(x => x.Mail)
                .ThenInclude(x => x.Sender)
                .Where(x => x.MailID == mailID && x.UserID == userID)
                .FirstOrDefault();
            if (mail == null)
                return null;
            return new MailDetailsView
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
                Attachments = GetMailAttachments(mailID)
            };
        }

        private List<string> GetMailRecipients(int userID, int mailID)
        {
            var userMails = _context.UserMails
                .Include(x => x.User)
                .Where(x => x.MailID == mailID)
                .ToList();

            List<string> recipients = new List<string>();
            var user = _context.Users.Where(x => x.ID == userID).AsQueryable().ToList().First();
            recipients.Add(user.Email);
            foreach (UserMail um in userMails)
            {
                if (um.RecipientType == RecipientType.CC)
                    if (um.UserID != userID)
                        recipients.Add(um.User.Email);
            }

            return recipients;
        }

        private List<(string, Guid)> GetMailAttachments(int mailID)
        {
            var attachmentsDB = _context.Attachments
                .Where(x => x.MailID == mailID)
                .ToList();

            List<(string filename, Guid id)> attachments = new List<(string filename, Guid id)>();
            foreach (var attachment in attachmentsDB)
                attachments.Add((attachment.Filename, attachment.ID));

            return attachments;
        }

        public async Task AddMail(int userID, NewMail newMail)
        {
            if (newMail.BCCRecipientsAddresses != null)
                newMail.BCCRecipientsAddresses = newMail.BCCRecipientsAddresses.Distinct().ToList();
            if (newMail.CCRecipientsAddresses != null)
                newMail.CCRecipientsAddresses = newMail.CCRecipientsAddresses.Distinct().ToList();
            CheckRecipientsAddressesCorrectness(newMail.BCCRecipientsAddresses, newMail.CCRecipientsAddresses);

            var transaction = _context.Database.BeginTransaction();

            try
            {
                int mailID = AddNewMailToDB(userID, newMail.Topic, newMail.Text);

                AddMailRecipientsToDB(newMail.CCRecipientsAddresses, RecipientType.CC, mailID);    
                AddMailRecipientsToDB(newMail.BCCRecipientsAddresses, RecipientType.BCC, mailID);

                if (IfAttachments(newMail.Files))
                    await StoreMailAttachmentsOnAzureBlob(mailID, newMail.Files);

                transaction.Commit();

                await NotifyRecipients(newMail.BCCRecipientsAddresses, newMail.CCRecipientsAddresses, IfAttachments(newMail.Files));
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
        }

        private void CheckRecipientsAddressesCorrectness(List<string> BCCRecipientsAddresses, List<string> CCRecipientsAddresses)
        {
            var users = _context.Users.ToList();
            List<string> emails = new List<string>();
            foreach (User usr in users)
                emails.Add(usr.Email);

            if (BCCRecipientsAddresses != null)
                foreach (string email in BCCRecipientsAddresses)
                {
                    if (!emails.Contains(email))
                        throw new Exception("BCCRecipientsAddresses", new Exception("No such email in global contacts list."));
                }
            if (CCRecipientsAddresses != null)
                foreach (string email in CCRecipientsAddresses)
                {
                    if (!emails.Contains(email))
                        throw new Exception("CCRecipientsAddresses", new Exception("No such email in global contacts list."));
                }
        }

        private int AddNewMailToDB(int userID, string topic, string text)
        {
            User usr = _context.Users.Find(userID);
            Mail mail = new Mail
            {
                Date = DateTime.Now,
                Topic = topic,
                Text = text,
                Sender = usr,
            };
            _context.Mails.Add(mail);
            _context.SaveChanges();
            return mail.ID;
        }

        private void AddMailRecipientsToDB(List<string> recipients, RecipientType recipientType, int mailID)
        {
            if (recipients != null)
                foreach (string email in recipients)
                {
                    User usr = _context.Users.Where(x => x.Email == email).FirstOrDefault();
                    if (usr == null)
                        continue;
                    UserMail um = new UserMail
                    {
                        UserID = usr.ID,
                        MailID = mailID,
                        RecipientType = recipientType,
                        Read = false,
                    };
                    _context.UserMails.Add(um);
                }
            _context.SaveChanges();
        }

        private bool IfAttachments(List<IFormFile> files)
        {
            return !(files == null || files.Count == 0);
        }

        private async Task StoreMailAttachmentsOnAzureBlob(int mailID, List<IFormFile> files)
        {
            string connectionString = _configuration.GetConnectionString("AzureBlob");
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            var section = _configuration.GetSection("AzureBlob");
            string containerName = section.GetValue<string>("ContainerName");
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            List<Task> uploadTasks = new List<Task>();

            foreach (var file in files)
            {
                Attachment attachment = new Attachment
                {
                    ID = Guid.NewGuid(),
                    MailID = mailID,
                    Filename = file.FileName.Trim().Replace(' ', '-')
                };
                _context.Attachments.Add(attachment);
                string fileName = attachment.ID.ToString() + attachment.Filename;
                BlobClient blobClient = containerClient.GetBlobClient(fileName);
                uploadTasks.Add(blobClient.UploadAsync(file.OpenReadStream()));
            }
            await Task.WhenAll(uploadTasks);
            _context.SaveChanges();
        }

        private async Task NotifyRecipients(List<string> BCCRecipientsAddresses, List<string> CCRecipientsAddresses, bool ifAttachments)
        {
            HashSet<string> recipients = new HashSet<string>();
            recipients.UnionWith(BCCRecipientsAddresses);
            recipients.UnionWith(CCRecipientsAddresses);
            await _notificationService.SendNotification(recipients.ToList(), "NewMail", ifAttachments);
        }

        public async Task<byte[]> DownloadAttachment(string filename)
        {
            string storageConnection = _configuration.GetConnectionString("AzureBlob");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnection);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            var section = _configuration.GetSection("AzureBlob");
            string containerName = section.GetValue<string>("ContainerName");
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerName);
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(filename);

            MemoryStream stream = new MemoryStream();
            await blockBlob.DownloadToStreamAsync(stream);

            var byteArray = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(byteArray, 0, (int)stream.Length);

            return byteArray;
        }
    }
}
