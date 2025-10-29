using DisasterAlleviationFoundation.Data;
using DisasterAlleviationFoundation.Models;
=========
using Microsoft.AspNetCore.Identity;
>>>>>>>>> Temporary merge branch 2
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace DisasterAlleviationFoundation.Controllers
{
    public class CrisesController : Controller
    {
        private readonly GiftOfTheGiversDbContext _context;
        private readonly UserManager<User> _userManager;

        public CrisesController(GiftOfTheGiversDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Crises
        public async Task<IActionResult> Index()
        {
            var crises = await _context.Crises.ToListAsync();
            return View(crises);
        }

        // GET: Crises/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var crisis = await _context.Crises
                .Include(c => c.Tasks)
                .Include(c => c.Distributions)
                .FirstOrDefaultAsync(c => c.CrisisID == id);

            if (crisis == null)
                return NotFound();

            return View(crisis);
        }

        // GET: Crises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Crises/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Crisis crisis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(crisis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(crisis);
        }

        // GET: Crises/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var crisis = await _context.Crises.FindAsync(id);
            if (crisis == null)
                return NotFound();

            return View(crisis);
        }

        // POST: Crises/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Crisis crisis)
        {
            if (id != crisis.CrisisID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(crisis);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Crises.Any(c => c.CrisisID == id))
                        return NotFound();
                    else
                        throw;
                }
            }

            return View(crisis);
        }

        // GET: Crises/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var crisis = await _context.Crises.FirstOrDefaultAsync(c => c.CrisisID == id);
            if (crisis == null)
                return NotFound();

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
