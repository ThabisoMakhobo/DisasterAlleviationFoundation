using DisasterAlleviationFoundation.Data;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace DisasterAlleviationFoundation.Controllers
{
    public class ResourcesController : Controller
    {
        private readonly GiftOfTheGiversDbContext _context;

        public ResourcesController(GiftOfTheGiversDbContext context)
        {
            _context = context;
        }

        // ✅ GET: Resources
        public async Task<IActionResult> Index()
        {
            var resources = await _context.Resources.ToListAsync();
            return View(resources);
        }

        // ✅ GET: Resources/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var resource = await _context.Resources
                .FirstOrDefaultAsync(r => r.ResourceID == id);

            if (resource == null)
                return NotFound();

            return View(resource);
        }

        // ✅ GET: Resources/Create
        public IActionResult Create()
        {
            return View();
        }

        // ✅ POST: Resources/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Resource resource)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resource);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(resource);
        }

        // ✅ GET: Resources/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource == null)
                return NotFound();

            return View(resource);
        }

        // ✅ POST: Resources/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Resource resource)
        {
            if (id != resource.ResourceID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resource);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResourceExists(resource.ResourceID))
                        return NotFound();
                    else
                        throw;
                }
            }

            return View(resource);
        }

        // ✅ GET: Resources/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var resource = await _context.Resources
                .FirstOrDefaultAsync(r => r.ResourceID == id);

            if (resource == null)
                return NotFound();

            return View(resource);
        }

        // ✅ POST: Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource != null)
            {
                _context.Resources.Remove(resource);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // ✅ Helper method
        private bool ResourceExists(int id)
        {
            return _context.Resources.Any(e => e.ResourceID == id);
        }
    }
}
