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
using RestSharp;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace iBudget.Controllers
{
    public class FinancialAnalystsController : Controller
    {
        private readonly IHostingEnvironment he;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public FinancialAnalystsController(ApplicationDbContext context, IHostingEnvironment e, UserManager<IdentityUser> userManager)
        {
            _context = context;
            he = e;
            _userManager = userManager;
        }

        // GET: FinancialAnalysts
        public async Task<IActionResult> Index()
        {
            return View(await _context.FinancialAnalysts.ToListAsync());
        }

        // GET: FinancialAnalysts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _context.FinancialAnalysts
                .FirstOrDefaultAsync(m => m.ApplicationUserId == userId);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: FinancialAnalysts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FinancialAnalysts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FinancialAnalystID,Name,StreetAddress,CityStateZip,Bio,Promotions,Link")] FinancialAnalyst financialAnalyst)
        {
            if (ModelState.IsValid)
            {
                financialAnalyst.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(financialAnalyst);
                await _context.SaveChangesAsync();
            }
            return View("Details", financialAnalyst);
        }

        // GET: FinancialAnalysts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _context.FinancialAnalysts
                .FirstOrDefaultAsync(m => m.ApplicationUserId == userId);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: FinancialAnalysts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FinancialAnalystID,Name,StreetAddress,CityStateZip,Bio,Promotions,Link,ApplicationUserId,Image")] FinancialAnalyst financialAnalyst)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(financialAnalyst);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinancialAnalystExists(financialAnalyst.FinancialAnalystID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details));
            }
            return View(financialAnalyst);
        }

        // GET: FinancialAnalysts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financialAnalyst = await _context.FinancialAnalysts
                .FirstOrDefaultAsync(m => m.FinancialAnalystID == id);
            if (financialAnalyst == null)
            {
                return NotFound();
            }

            return View(financialAnalyst);
        }

        // POST: FinancialAnalysts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var financialAnalyst = await _context.FinancialAnalysts.FindAsync(id);
            _context.FinancialAnalysts.Remove(financialAnalyst);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FinancialAnalystExists(int id)
        {
            return _context.FinancialAnalysts.Any(e => e.FinancialAnalystID == id);
        }
        public async Task<IActionResult> Map(int? id)
        {
            {
                if (id == null)
                {
                }
                FinancialAnalyst financialAnalyst = _context.FinancialAnalysts.Find(id);
                if (financialAnalyst == null)
                {
                    return NotFound();
                }
                ViewBag.ApplicationUserId = new SelectList(_context.Users, "Id", "UserRole", financialAnalyst.ApplicationUser);
                ViewBag.CustomerAddress = financialAnalyst.StreetAddress;
                ViewBag.CustomerZip = financialAnalyst.CityStateZip;
                return View(financialAnalyst);
            }
        }
        public IActionResult UploadImage(string fullName, IFormFile pic, int? id)
        {

            if (pic == null)
            {
                return View();

            }

            if (pic != null)
            {
                var fullPath = Path.Combine(he.WebRootPath, Path.GetFileName(pic.FileName));

                var fileName = pic.FileName;

                pic.CopyTo(new FileStream(fullPath, FileMode.Create));

                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var financialAnalyst = _context.FinancialAnalysts
                    .FirstOrDefault(m => m.ApplicationUserId == userid);

                financialAnalyst.Image = fileName;
                _context.Update(financialAnalyst);
                _context.SaveChangesAsync();

                ViewBag.ProfileImage = financialAnalyst.Image;

                ViewData["FileLocation"] = "/" + Path.GetFileName(pic.FileName);
            }
            return View();
        }
    }
}
