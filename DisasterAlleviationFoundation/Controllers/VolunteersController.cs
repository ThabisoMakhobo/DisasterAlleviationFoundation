using DisasterAlleviationFoundation.Data;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviationFoundation.Controllers
{
    public class VolunteersController : Controller
    {
        private readonly GiftOfTheGiversDbContext _context;
        private readonly UserManager<User> _userManager;

        public VolunteersController(GiftOfTheGiversDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Volunteers
        public async Task<IActionResult> Index()
        {
            /*var volunteers = await _context.Volunteers.Include(v => v.User).ToListAsync();
            return View(volunteers);*/
            var users = _userManager.Users.ToList();
            return View(users);
        }

        // GET: Volunteers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Volunteers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,Availability,Location,HoursServed")] Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(volunteer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(volunteer);
        }

        // GET: Volunteers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer == null) return NotFound();
            return View(volunteer);
        }

        // POST: Volunteers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VolunteerID,UserID,Availability,Location,HoursServed")] Volunteer volunteer)
        {
            if (id != volunteer.VolunteerID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(volunteer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolunteerExists(volunteer.VolunteerID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(volunteer);
        }

        // GET: Volunteers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var volunteer = await _context.Volunteers
                .Include(v => v.User)
                .FirstOrDefaultAsync(v => v.VolunteerID == id);

            if (volunteer == null) return NotFound();

            return View(volunteer);
        }

        // POST: Volunteers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volunteer = await _context.Volunteers.FindAsync(id);
            _context.Volunteers.Remove(volunteer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolunteerExists(int id)
        {
            return _context.Volunteers.Any(v => v.VolunteerID == id);
        }
    }
}
