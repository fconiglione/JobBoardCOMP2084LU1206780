using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobBoardCOMP2084LU1206780.Data;
using JobBoardCOMP2084LU1206780.Models;

namespace JobBoardCOMP2084LU1206780.Controllers
{
    public class JobListingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobListingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobListings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.JobListings.Include(j => j.Company);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: JobListings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.JobListings == null)
            {
                return NotFound();
            }

            var jobListing = await _context.JobListings
                .Include(j => j.Company)
                .FirstOrDefaultAsync(m => m.JobListingId == id);
            if (jobListing == null)
            {
                return NotFound();
            }

            return View(jobListing);
        }

        // GET: JobListings/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyId");
            return View();
        }

        // POST: JobListings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobListingId,Title,Description,Salary,CompanyId")] JobListing jobListing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobListing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies.OrderBy(c => c.Name), "CompanyId", "Name");
            return View(jobListing);
        }

        // GET: JobListings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.JobListings == null)
            {
                return NotFound();
            }

            var jobListing = await _context.JobListings.FindAsync(id);
            if (jobListing == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies.OrderBy(c => c.Name), "CompanyId", "Name", jobListing.CompanyId);
            return View(jobListing);
        }

        // POST: JobListings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobListingId,Title,Description,Salary,CompanyId")] JobListing jobListing)
        {
            if (id != jobListing.JobListingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobListing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobListingExists(jobListing.JobListingId))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyId", jobListing.CompanyId);
            return View(jobListing);
        }

        // GET: JobListings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.JobListings == null)
            {
                return NotFound();
            }

            var jobListing = await _context.JobListings
                .Include(j => j.Company)
                .FirstOrDefaultAsync(m => m.JobListingId == id);
            if (jobListing == null)
            {
                return NotFound();
            }

            return View(jobListing);
        }

        // POST: JobListings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JobListings == null)
            {
                return Problem("Entity set 'ApplicationDbContext.JobListings'  is null.");
            }
            var jobListing = await _context.JobListings.FindAsync(id);
            if (jobListing != null)
            {
                _context.JobListings.Remove(jobListing);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobListingExists(int id)
        {
          return (_context.JobListings?.Any(e => e.JobListingId == id)).GetValueOrDefault();
        }
    }
}
