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
        public IActionResult Api()
        {
            return View();
        }
        //public IActionResult Index(string description, DateTime date)
        //{
        //    var accessToken = "Ka1LkWNQ8ini6lgrhdU313FnPoePxQAF";
        //    var cronofy = new CronofyAccountClient(accessToken);
        //    var eventBuilder = new UpsertEventRequestBuilder()
        //        .EventId("unique-event-id")
        //        .Summary("Event summary")
        //        .Description("Event description")
        //        .Start(2018, 10, 31, 12, 0)
        //        .End(2018, 10, 31, 12, 30)
        //        .Location("Meeting room");
        //    var calendarId = "cal_W8Y4varHdE2bNGh-_dOMep0jloIjFNAqxq-JVhg";
        //    cronofy.UpsertEvent(calendarId, eventBuilder);
        //    return View();
        //}
    }
}