
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailBox.Services.Interfaces
{
    public interface INotificationService
    {
        public Task SendNotification(List<string> emails, string content, bool withAttachments);
    }
}
