using MailBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Services.Interfaces
{
    public interface IMailService
    {
        List<MailInboxView> GetUserMails(int userId);
        MailInboxView GetMail(int userID, int mailID);
        void CreateMail(int userID, NewMail mail);
    }
}
