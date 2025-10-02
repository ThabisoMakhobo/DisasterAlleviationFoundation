using DisasterAlleviationFoundation.Data;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlleviationFoundation.Controllers
{
    public class CrisisController : Controller
    {
        private readonly GiftOfTheGiversDbContext _context;

        public CrisisController(GiftOfTheGiversDbContext context)
        {
            _context = context;
        }

        // GET: Crisis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Crises.ToListAsync());
        }

        // GET: Crisis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var crisis = await _context.Crises.FirstOrDefaultAsync(c => c.CrisisID == id);
            if (crisis == null) return NotFound();

            return View(crisis);
        }

        // GET: Crisis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Crisis/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Type,Location,Date,Status")] Crisis crisis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(crisis);
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
