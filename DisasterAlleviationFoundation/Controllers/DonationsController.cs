
using DisasterAlleviationFoundation.Models;
using DisasterAlleviationFoundation.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlleviationFoundation.Controllers
{
    public class DonationsController : Controller
    {
        private readonly GiftOfTheGiversDbContext _context;

        public DonationsController(GiftOfTheGiversDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Donations
                .Include(d => d.UserID)
                .Include(d => d.ResourceID)
                .ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Donation donation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(donation);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var donation = await _context.Donations.FindAsync(id);
            if (donation == null) return NotFound();
            return View(donation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Donation donation)
        {
            if (id != donation.DonationID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(donation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(donation);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var donation = await _context.Donations.FindAsync(id);
            if (donation == null) return NotFound();
            return View(donation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donation = await _context.Donations.FindAsync(id);
            _context.Donations.Remove(donation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
