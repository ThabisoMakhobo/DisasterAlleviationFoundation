using DisasterAlleviationFoundation.Data;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlleviationFoundation.Controllers
{
    public class ResourcesController : Controller
    {
        private readonly GiftOfTheGiversDbContext _context;

        public ResourcesController(GiftOfTheGiversDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Resources.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

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

        public async Task<IActionResult> Edit(int id)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource == null) return NotFound();
            return View(resource);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Resource resource)
        {
            if (id != resource.ResourceID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(resource);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resource);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource == null) return NotFound();
            return View(resource);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resource = await _context.Resources.FindAsync(id);
            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
