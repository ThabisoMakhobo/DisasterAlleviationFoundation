using DisasterAlleviationFoundation.Data;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlleviationFoundation.Controllers
{
    {
        private readonly GiftOfTheGiversDbContext _context;
        private readonly UserManager<User> _userManager;

        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Crisis
        public async Task<IActionResult> Index()
        {
        }

        // POST: Crisis/Create
        [HttpPost]
        {
            if (ModelState.IsValid)
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(crisis);
        }

        // GET: Crisis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var crisis = await _context.Crises.FindAsync(id);
            if (crisis == null) return NotFound();
            return View(crisis);
        }

        // POST: Crisis/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CrisisID,Type,Location,Date,Status")] Crisis crisis)
        {
            if (id != crisis.CrisisID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(crisis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CrisisExists(crisis.CrisisID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(crisis);
        }

        // GET: Crisis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var crisis = await _context.Crises.FirstOrDefaultAsync(c => c.CrisisID == id);
            if (crisis == null) return NotFound();

            return View(crisis);
        }

        // POST: Crisis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crisis = await _context.Crises.FindAsync(id);
            _context.Crises.Remove(crisis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CrisisExists(int id)
        {
            return _context.Crises.Any(c => c.CrisisID == id);
        }
    }
}
