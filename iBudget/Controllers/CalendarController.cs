using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cronofy;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;

namespace iBudget.Controllers
{
    public class CalendarController : Controller
    {
        public IActionResult Index(string description, DateTime date)
        {
            var accessToken = "";
            var cronofy = new CronofyAccountClient(accessToken);
            var eventBuilder = new UpsertEventRequestBuilder()
                .EventId("unique-event-id")
                .Summary("A Bill Due")
                .Description("Phone Bill Due")
                .Start(2018, 11, 16, 12, 0)
                .End(2018, 11, 16, 12, 30)
                .Location("Home");
            var calendarId = "";
            cronofy.UpsertEvent(calendarId, eventBuilder);

            SmsController sms = new SmsController();
            sms.SendText();

            return View();
        }
    }
}