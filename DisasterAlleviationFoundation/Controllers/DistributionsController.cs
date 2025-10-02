using DisasterAlleviationFoundation.Data;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlleviationFoundation.Controllers
{

    public class DistributionsController : Controller
    {
        private readonly GiftOfTheGiversDbContext _context;

        public DistributionsController(GiftOfTheGiversDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Distributions
                .Include(d => d.ResourceID)
                .Include(d => d.CrisisID)
                .ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Distribution distribution)
        {
            if (ModelState.IsValid)
            {
                _context.Add(distribution);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(distribution);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var distribution = await _context.Distributions.FindAsync(id);
            if (distribution == null) return NotFound();
            return View(distribution);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Distribution distribution)
        {
            if (id != distribution.DistributionID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(distribution);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(distribution);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var distribution = await _context.Distributions.FindAsync(id);
            if (distribution == null) return NotFound();
            return View(distribution);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var distribution = await _context.Distributions.FindAsync(id);
            _context.Distributions.Remove(distribution);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
