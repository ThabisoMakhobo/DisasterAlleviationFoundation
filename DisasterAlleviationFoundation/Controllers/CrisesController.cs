using DisasterAlleviationFoundation.Data;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviationFoundation.Controllers
{
    [Authorize(Roles = "Admin")] // Only admins can access
    public class CrisesController : Controller
    {
        private readonly GiftOfTheGiversDbContext _context;

        public CrisesController(GiftOfTheGiversDbContext context)
        {
            _context = context;
        }

        // GET: Crises
        public async Task<IActionResult> Index()
        {
            var crises = _context.Crises
                .Include(c => c.Resource)
                .Include(c => c.Donation)
                .OrderByDescending(c => c.Date);
            return View(await crises.ToListAsync());
        }

        // GET: Crises/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var crisis = await _context.Crises
                .Include(c => c.Resource)
                .Include(c => c.Donation)
                .Include(c => c.Tasks)
                .Include(c => c.Distributions)
                .FirstOrDefaultAsync(c => c.CrisisID == id);

            if (crisis == null) return NotFound();
            return View(crisis);
        }

        // GET: Crises/Create
        public IActionResult Create()
        {
            ViewBag.Resources = new SelectList(_context.Resources, "ResourceID", "Name");
            ViewBag.Donations = new SelectList(_context.Donations, "DonationID", "DonorName");
            return View();
        }

        // POST: Crises/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Crisis crisis)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Resources = new SelectList(_context.Resources, "ResourceID", "Name");
                ViewBag.Donations = new SelectList(_context.Donations, "DonationID", "DonorName");
                return View(crisis);
            }

            _context.Add(crisis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Crises/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var crisis = await _context.Crises.FindAsync(id);
            if (crisis == null) return NotFound();

            ViewBag.Resources = new SelectList(_context.Resources, "ResourceID", "Name", crisis.ResourceID);
            ViewBag.Donations = new SelectList(_context.Donations, "DonationID", "DonorName", crisis.DonationID);
            return View(crisis);
        }

        // POST: Crises/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Crisis crisis)
        {
            if (id != crisis.CrisisID) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Resources = new SelectList(_context.Resources, "ResourceID", "Name", crisis.ResourceID);
                ViewBag.Donations = new SelectList(_context.Donations, "DonationID", "DonorName", crisis.DonationID);
                return View(crisis);
            }

            _context.Update(crisis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Crises/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var crisis = await _context.Crises
                .Include(c => c.Resource)
                .Include(c => c.Donation)
                .FirstOrDefaultAsync(c => c.CrisisID == id);

            if (crisis == null) return NotFound();
            return View(crisis);
        }

        // POST: Crises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crisis = await _context.Crises.FindAsync(id);
            if (crisis != null)
            {
                _context.Crises.Remove(crisis);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
