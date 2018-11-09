using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace iBudget
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            // Find your Account Sid and Token at twilio.com/console
            const string accountSid = "AC878e5513a8dfc8a639c7c9f5ba08f7ec";
            const string authToken = "3e334c54d8d1f81a2eac24d65150af72";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Reminder about bill due on Tuesday.",
                from: new Twilio.Types.PhoneNumber("+14142694765"),
                to: new Twilio.Types.PhoneNumber("+14143260740")
            );

            Console.WriteLine(message.Sid);
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

    }
}
