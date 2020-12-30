using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Services.Interfaces
{
    public interface INotificationService
    {
        public void SendNotification(List<string> emails, string content, bool withAttachments);
    }
}
