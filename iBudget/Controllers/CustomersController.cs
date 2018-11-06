using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iBudget.Data;
using iBudget.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace iBudget.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CustomersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }
        public async Task<IActionResult> FinancialAnalystIndex()
        {
            return View(await _context.FinancialAnalysts.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _context.Customers
                .FirstOrDefaultAsync(m => m.ApplicationUserId == userId);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        public IActionResult FinancialAnalystDetails(int? id)
        {
            var financialAnalyst = _context.FinancialAnalysts.Where(b => b.FinancialAnalystID == id).FirstOrDefault();
            if (id == null)
            {
                return NotFound();
            }

            return View(financialAnalyst);
        }
        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,Name,StreetAddress,CityStateZip,ApplicationUserId,Subscribed")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //var mailMessage = new MailMessage
            //{
            //    From = new MailAddress("support@dotnetthoughts.net"),
            //    Subject = "Hello World",
            //    Body = "Test email from Send Grid SMTP Settings"
            //};

            //mailMessage.To.Add("anuraj@dotnetthoughts.net");

            //var smtpClient = new SmtpClient
            //{
            //    Credentials = new NetworkCredential("Your-Username@azure.com", "Your-Password"),
            //    Host = "smtp.sendgrid.net",
            //    Port = 587
            //};

            //smtpClient.Send(mailMessage);
            SendEmail();
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", customer.ApplicationUserId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _context.Customers
                .FirstOrDefaultAsync(m => m.ApplicationUserId == userId);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerID,Name,StreetAddress,CityStateZip,ApplicationUserId")] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", customer.ApplicationUserId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }
        public async Task<IActionResult> Map(int? id)
        {
            {
                if (id == null)
                {

                }
                Customer customer = _context.Customers.Find(id);
                if (customer == null)
                {
                    return NotFound();
                }
                ViewBag.ApplicationUserId = new SelectList(_context.Users, "Id", "UserRole", customer.ApplicationUser);
                ViewBag.CustomerAddress = customer.StreetAddress;
                ViewBag.CustomerZip = customer.CityStateZip;
                return View(customer);
            }
        }
        //public static IRestResponse SendSimpleMessage() 
        //{
        //    RestClient client = new RestClient();
        //    client.BaseUrl = new Uri("https://api.mailgun.net/v3");
        //    client.Authenticator =
        //        new HttpBasicAuthenticator("api",
        //                                    Key.mailgunKey);
        //    RestRequest request = new RestRequest();
        //    request.AddParameter("domain", "sandboxd38a61ad30c840cfb92cf1515f62e7ea.mailgun.org", ParameterType.UrlSegment);
        //    request.Resource = "{domain}/messages";
        //    request.AddParameter("from", "Mailgun Sandbox <mailgun@sandboxd38a61ad30c840cfb92cf1515f62e7ea.mailgun.org>");
        //    request.AddParameter("to", "marisajanowski@gmail.com");
        //    request.AddParameter("to", "YOU@sandboxd38a61ad30c840cfb92cf1515f62e7ea.mailgun.org");
        //    request.AddParameter("subject", "Hello");
        //    request.AddParameter("text", "Testing some Mailgun awesomeness!");
        //    request.Method = Method.POST;
        //    return client.Execute(request);
        //}
        //private async Task SendEmail(string email, string subject, string htmlContent)
        //{
        //    var apiKey = "YOUR SENDGRID API Key";
        //    var client = new SendGridClient(apiKey);
        //    var from = new EmailAddress("support@dotnetthoughts.net", "Support");
        //    var to = new EmailAddress(email);
        //    var plainTextContent = Regex.Replace(htmlContent, "<[^>]*>", "");
        //    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //    var response = await client.SendEmailAsync(msg);
        //}
        public static async Task SendEmail()
        {
            var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(Key.sendGridKey);
            var from = new EmailAddress("mj108815@gmail.com", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("mj108815@gmail.com", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
