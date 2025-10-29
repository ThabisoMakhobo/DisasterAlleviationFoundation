using DisasterAlleviationFoundation.Data;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace DisasterAlleviationFoundation.Controllers
{
    public class DonationsController : Controller
    {
        private readonly GiftOfTheGiversDbContext _context;
        private readonly UserManager<User> _userManager;

        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Donations
        public async Task<IActionResult> Index()
        {
            var donations = await _context.Donations
                .Include(d => d.User)
                .Include(d => d.Resource)
                .ToListAsync();

            return View(donations);
        }

        // GET: Donations/Create
        public IActionResult Create()
        {
            ViewBag.Resources = _context.Resources.ToList();
            return View();
        }

        // POST: Donations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Donation donation)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Resources = _context.Resources.ToList();
                return View(donation);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                donation.UserID = user.Id;
            }

            donation.Date = System.DateTime.Now;

            _context.Add(donation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Donations/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var donation = await _context.Donations.FindAsync(id);
            if (donation == null)
                return NotFound();

            ViewBag.Resources = _context.Resources.ToList();
            return View(donation);
        }

        // POST: Donations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Donation donation)
        {
            if (id != donation.DonationID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donation);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Donations.Any(d => d.DonationID == id))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewBag.Resources = _context.Resources.ToList();
            return View(donation);
        }

        // GET: Donations/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var donation = await _context.Donations
                .Include(d => d.User)
                .Include(d => d.Resource)
                .FirstOrDefaultAsync(d => d.DonationID == id);

            if (donation == null)
                return NotFound();

            return View(donation);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donation = await _context.Donations.FindAsync(id);
            if (donation != null)
            {
                _context.Donations.Remove(donation);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
