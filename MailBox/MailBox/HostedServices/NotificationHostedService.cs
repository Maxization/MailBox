
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.DependencyInjection;
using MailBox.Services;

namespace MailBox.HostedServices
{
    public class NotificationHostedService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        public readonly IServiceProvider _serviceProvider;
        private TimeSpan timeout;
        public NotificationHostedService(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            timeout = new TimeSpan(24, 0, 0);
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var section = _configuration.GetSection("SendGrid");
                string apiKey = section.GetValue<string>("SendGridKey");
                string senderMail = section.GetValue<string>("Email");
                string senderName = section.GetValue<string>("Name");

                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(senderMail, senderName);

                List<EmailAddress> tos = new List<EmailAddress>();

                using (var scope = _serviceProvider.CreateScope())
                {
                    var userService =
                        scope.ServiceProvider
                            .GetRequiredService<IUserService>();
                    var data = userService.GetUsersAndNumberOfUnreadMails();

                    foreach (var un in data)
                    {
                        tos.Add(new EmailAddress(un.Email, un.Name));
                    }
                }

                var subject = "Nieodczytane wiadomości";
                var htmlContent = "<strong>Na stronie MailBox masz nieprzeczytanie wiadomości. Zaloguj sie i sprawdź!</strong>";
                var displayRecipients = false;
                var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, "", htmlContent, displayRecipients);
                //var response = await client.SendEmailAsync(msg);

                await Task.Delay(timeout, stoppingToken);
            }
        }
    }
}
