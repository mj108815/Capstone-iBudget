using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iBudget.Data;
using iBudget.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.IO;
using Stripe;

namespace iBudget.Controllers
{
    public class AdsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment he;
        private readonly UserManager<IdentityUser> _userManager;


        public AdsController(ApplicationDbContext context, IHostingEnvironment e, UserManager<IdentityUser> userManager)
        {
            _context = context;
            he = e;
            _userManager = userManager;
        }

        // GET: Ads
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Ads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ad
                .Include(a => a.ApplicationUser)
                .FirstOrDefaultAsync(m => m.AdID == id);
            if (ad == null)
            {
                return NotFound();
            }

            return View(ad);
        }

        // GET: Ads/Create
        public IActionResult Create()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Ads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdID,AdPost,Carousel,ApplicationUserId")] Ad ad)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", ad.ApplicationUserId);
            ad.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Add(ad);
            await _context.SaveChangesAsync();

            var errors = ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .Select(x => new { x.Key, x.Value.Errors })
            .ToArray();

            if (ModelState.IsValid)
            {
                //saveChanges

                if (ad.Carousel == true)
                {
                    //create action for carousel payment 
                    return RedirectToAction("Payment");
                }
                if (ad.AdPost == true)
                {
                    //create action for adpost payment
                    return RedirectToAction("Payment");
                }
                await _context.SaveChangesAsync();
            }

            return View(ad);
        }

        // GET: Ads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ad.FindAsync(id);
            if (ad == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", ad.ApplicationUserId);
            _context.Update(ad);
            await _context.SaveChangesAsync();
            if (ad.Carousel == true)
            {
                return View("UploadCarouselImage");
            }

            else
            {
                return View(ad);
            }
        }

        // POST: Ads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdID,AdPost,Carousel,PaymentCollected,ApplicationUserId")] Ad ad)
        {
            if (id != ad.AdID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdExists(ad.AdID))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", ad.ApplicationUserId);
            return View(ad);
        }

        // GET: Ads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ad
                .Include(a => a.ApplicationUser)
                .FirstOrDefaultAsync(m => m.AdID == id);
            if (ad == null)
            {
                return NotFound();
            }

            return View(ad);
        }

        // POST: Ads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ad = await _context.Ad.FindAsync(id);
            _context.Ad.Remove(ad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdExists(int id)
        {
            return _context.Ad.Any(e => e.AdID == id);
        }
        public IActionResult Payment()
        {
            return View();
        }

        [HttpPost]
        //public IActionResult Payment(string stripeEmail, string stripeToken)
        //{
        //    var customers = new StripeCustomerService();
        //    var charges = new StripeChargeService();

        //    var customer = customers.Create(new StripeCustomerCreateOptions
        //    {
        //        Email = stripeEmail,
        //        SourceToken = stripeToken
        //    });

        //    var charge = charges.Create(new StripeChargeCreateOptions
        //    {
        //        Amount = 50,
        //        Description = "Sample Charge",
        //        Currency = "usd",
        //        CustomerId = customer.Id
        //    });

        //    //set payment collected equal to true

        //    return RedirectToAction("UploadCarouselImage");
        //}
        public IActionResult UploadCarouselImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadCarouselImage(string fullName, IFormFile pic, int? id)
        {
            {

                if (pic == null)
                {
                    return View();

                }

                if (pic != null)

                {

                    var fileName = Path.Combine(he.WebRootPath, Path.GetFileName(pic.FileName));
                    pic.CopyTo(new FileStream(fileName, FileMode.Create));



                    var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var ad = _context.Ad
                        .FirstOrDefault(m => m.ApplicationUserId == userid);

                    ad.PaymentCollected = true;
                    ad.CarouselImage = fileName;

                    _context.Update(ad);

                    await _context.SaveChangesAsync();
                    //pic.CopyTo(new FileStream(fileName, FileMode.Create));
                    //ViewData["FileLocation"] = "/" + Path.GetFileName(pic.FileName);

                }
            }
            return View();
        }

        //public IActionResult CreateSponseredAd()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult CreateSponseredAd(int? id)
        //{

        //    return View("Create(BlogPost)");
        //}
    }
}
