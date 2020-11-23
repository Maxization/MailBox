using MailBox.Models;
using MailBox.Models.MailModels;
using System.Collections.Generic;

namespace MailBox.Services.Interfaces
{
    public interface IMailService
    {
        List<MailInboxView> GetUserMails(int userId);
        MailInboxView GetMail(int userID, int mailID);
        void CreateMail(int userID, NewMail mail);
    }
}
