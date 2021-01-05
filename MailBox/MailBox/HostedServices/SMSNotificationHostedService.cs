
using MailBox.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace MailBox.HostedServices
{
    public class SMSNotificationHostedService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        public readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan timeout;
        public SMSNotificationHostedService(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            timeout = new TimeSpan(168, 0, 0);
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var section = _configuration.GetSection("Twilio");
                string accountSid = section.GetValue<string>("AccountSID");
                string authToken = section.GetValue<string>("AuthToken");
                string number = section.GetValue<string>("Number");

                TwilioClient.Init(accountSid, authToken);
                List<Twilio.Types.PhoneNumber> tos = new List<Twilio.Types.PhoneNumber>();

                using (var scope = _serviceProvider.CreateScope())
                {
                    var userService =
                        scope.ServiceProvider
                            .GetRequiredService<IUserService>();

                    var data = userService.GetUsersNumberWithUnreadMails();

                    foreach (var un in data)
                    {
                        if (un.PhoneNumber != null)
                            tos.Add(new Twilio.Types.PhoneNumber(un.PhoneNumber));
                    }

                }

                foreach (Twilio.Types.PhoneNumber num in tos)
                {
                    _ = MessageResource.Create(
                        body: "Na stronie MailBox masz nieprzeczytanie wiadomości. Zaloguj sie i sprawdź!",
                        from: new Twilio.Types.PhoneNumber(number),
                        to: num
                        );
                }

                await Task.Delay(timeout, stoppingToken);
            }
        }
    }
}
