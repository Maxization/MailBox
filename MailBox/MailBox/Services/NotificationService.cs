
using MailBox.Models.NotificationModel;
using MailBox.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MailBox.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _configuration;
        public NotificationService() { }

        public NotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendNotification(List<string> emails, string content, bool withAttachments)
        {
            await Task.Run(() => SendNotificationToRecipients(emails, content, withAttachments));
        }

        private async void SendNotificationToRecipients(List<string> emails, string contentMes, bool withAttachments)
        {
            using HttpClient client = new HttpClient();
            var section = _configuration.GetSection("Notifications");
            client.DefaultRequestHeaders.Add(section.GetValue<string>("HeaderKey"), section.GetValue<string>("HeaderValue"));
            Notification notification = new Notification
            {
                Content = contentMes,
                RecipientsList = emails.ToArray(),
                WithAttachments = withAttachments
            };
            string json = await Task.Run(() => JsonConvert.SerializeObject(notification));
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://mini-notification-service.azurewebsites.net/notifications", content);

            var responseString = await response.Content.ReadAsStringAsync();
        }
    }
}
