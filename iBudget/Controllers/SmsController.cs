using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;
using Twilio.Rest.Api.V2010.Account;


namespace iBudget.Controllers
{
    public class SmsController : Controller
    {
        //public TwiMLResult Index(SmsRequest incomingMessage)
        //{
        //    var messagingResponse = new MessagingResponse();
        //    messagingResponse.Message("The copy cat says: " +
        //                              incomingMessage.Body);
        //    return TwiML(messagingResponse);
        //} 

        public void SendText()
        {
            // Find your Account Sid and Token at twilio.com/console
            const string accountSid = "";
            const string authToken = "";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Reminder about bill due tommorow.",
                from: new Twilio.Types.PhoneNumber("+14142694765"),
                to: new Twilio.Types.PhoneNumber("+14143260740")
            );

            Console.WriteLine(message.Sid);
        }

    }
}