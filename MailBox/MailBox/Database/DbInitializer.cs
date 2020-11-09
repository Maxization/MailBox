using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Database
{
    public static class DbInitializer
    {
        public static void Initialize(MailBoxDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            //TODO: Fill database with test data
            context.SaveChanges();
        }
    }
}
