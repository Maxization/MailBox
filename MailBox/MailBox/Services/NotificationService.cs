using MailBox.Models.NotificationModel;
using MailBox.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MailBox.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _configuration;
        public NotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendNotification(List<string> emails, string content, bool withAttachments)
        {
            Task SendNotification = Task.Run(() => SendNotificationToRecipients(emails, content, withAttachments));
            SendNotification.Wait();
        }


        private async void SendNotificationToRecipients(List<string> emails, string contentMes, bool withAttachments)
        {
            using (HttpClient client = new HttpClient())
            {
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
}
