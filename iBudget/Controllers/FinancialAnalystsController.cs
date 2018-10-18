using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iBudget.Data;
using iBudget.Models;

namespace iBudget.Controllers
{
    public class FinancialAnalystsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FinancialAnalystsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FinancialAnalysts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FinancialAnalysts.Include(f => f.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FinancialAnalysts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financialAnalyst = await _context.FinancialAnalysts
                .Include(f => f.ApplicationUser)
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (financialAnalyst == null)
            {
                return NotFound();
            }

            return View(financialAnalyst);
        }

        // GET: FinancialAnalysts/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: FinancialAnalysts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,Name,Address,ApplicationUserId")] FinancialAnalyst financialAnalyst)
        {
            if (ModelState.IsValid)
            {
                _context.Add(financialAnalyst);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", financialAnalyst.ApplicationUserId);
            return View(financialAnalyst);
        }

        // GET: FinancialAnalysts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financialAnalyst = await _context.FinancialAnalysts.FindAsync(id);
            if (financialAnalyst == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", financialAnalyst.ApplicationUserId);
            return View(financialAnalyst);
        }

        // POST: FinancialAnalysts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerID,Name,Address,ApplicationUserId")] FinancialAnalyst financialAnalyst)
        {
            if (id != financialAnalyst.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(financialAnalyst);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinancialAnalystExists(financialAnalyst.CustomerID))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", financialAnalyst.ApplicationUserId);
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
                .Include(f => f.ApplicationUser)
                .FirstOrDefaultAsync(m => m.CustomerID == id);
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
            return _context.FinancialAnalysts.Any(e => e.CustomerID == id);
        }
    }
}
