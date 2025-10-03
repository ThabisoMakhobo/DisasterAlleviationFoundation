using DisasterAlleviationFoundation.Data;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace DisasterAlleviationFoundation.Controllers
{
    public class DistributionsController : Controller
    {
        private readonly GiftOfTheGiversDbContext _context;
        private readonly UserManager<User> _userManager;

        // Proper constructor
        public DistributionsController(GiftOfTheGiversDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Distributions
        public async Task<IActionResult> Index()
        {
            var distributions = await _context.Distributions
                .Include(d => d.Crisis)
                .Include(d => d.Resource)
                .ToListAsync();

            return View(distributions);
        }

        // GET: Distributions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Distributions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Distribution distribution)
        {
            if (!ModelState.IsValid) return View(distribution);

            _context.Add(distribution);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Distributions/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var distribution = await _context.Distributions.FindAsync(id);
            if (distribution == null) return NotFound();
            return View(distribution);
        }

        // POST: Distributions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Distribution distribution)
        {
            if (id != distribution.DistributionID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(distribution);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DistributionExists(distribution.DistributionID)) return NotFound();
                    else throw;
                }
            }
            return View(distribution);
        }

        // GET: Distributions/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var distribution = await _context.Distributions
                .Include(d => d.Crisis)
                .Include(d => d.Resource)
                .FirstOrDefaultAsync(d => d.DistributionID == id);

            if (distribution == null) return NotFound();
            return View(distribution);
        }

        // POST: Distributions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var distribution = await _context.Distributions.FindAsync(id);
            if (distribution != null)
            {
                _context.Distributions.Remove(distribution);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DistributionExists(int id)
        {
            return _context.Distributions.Any(d => d.DistributionID == id);
        }
    }
}
