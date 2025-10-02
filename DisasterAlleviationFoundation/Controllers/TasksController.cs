using DisasterAlleviationFoundation.Data;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlleviationFoundation.Controllers
{
    public class TasksController : Controller
    {
        private readonly GiftOfTheGiversDbContext _context;

        public TasksController(GiftOfTheGiversDbContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tasks
                .Include(t => t.Crisis)
                .Include(t => t.Volunteer)
                .ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.Tasks
                .Include(t => t.Crisis)
                .Include(t => t.Volunteer)
                .FirstOrDefaultAsync(t => t.TaskID == id);

            if (task == null) return NotFound();

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CrisisID,VolunteerID,Description,Status")] TaskEntity task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();
            return View(task);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskID,CrisisID,VolunteerID,Description,Status")] TaskEntity task)
        {
            if (id != task.TaskID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.TaskID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.Tasks
                .Include(t => t.Crisis)
                .Include(t => t.Volunteer)
                .FirstOrDefaultAsync(t => t.TaskID == id);

            if (task == null) return NotFound();

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(t => t.TaskID == id);
        }
    }
}
