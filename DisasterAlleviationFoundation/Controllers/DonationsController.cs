using DisasterAlleviationFoundation.Data;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace DisasterAlleviationFoundation.Controllers
{
    [Authorize] // Only logged-in users can donate
    public class DonationsController : Controller
    {
        private readonly GiftOfTheGiversDbContext _context;
        private readonly UserManager<User> _userManager;

        public DonationsController(GiftOfTheGiversDbContext context, UserManager<User> userManager)
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
                .OrderByDescending(d => d.Date)
                .ToListAsync();

            return View(donations);
        }

        // GET: Donations/Create
        public IActionResult Create()
        {
            ViewBag.Resources = new SelectList(_context.Resources.ToList(), "ResourceID", "Name");
            return View();
        }

        // POST: Donations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Donation donation)
        {
            // Re-populate dropdown always
            ViewBag.Resources = new SelectList(_context.Resources.ToList(), "ResourceID", "Name");

            // Dump ModelState errors to TempData if invalid so the view can show them
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .Select(ms => new {
                        Key = ms.Key,
                        Errors = ms.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    }).ToList();

                var msg = "ModelState invalid:\n" + string.Join("\n", errors.Select(e => $"{e.Key}: {string.Join(", ", e.Errors)}"));
                TempData["ErrorMessage"] = msg;

                // Also return the model back to the view so fields stay populated
                return View(donation);
            }

            // get user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = "You must be logged in to make a donation.";
                return RedirectToAction("Login", "Account");
            }

            donation.UserID = user.Id;
            donation.Date = DateTime.Now;

            // Optional: show the connection string for quick verification (temporary)
            try
            {
                TempData["DebugInfo"] = $"DB: {_context.Database.GetDbConnection().DataSource} / {_context.Database.GetDbConnection().Database}";
            }
            catch { /* ignore if environment restricts it */ }

            try
            {
                _context.Donations.Add(donation);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Donation saved successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Show full exception to TempData so the view can display it during debugging
                TempData["ErrorMessage"] = "Save failed: " + ex.ToString();
                // keep view so user can see error and correct form
                return View(donation);
            }
        }



        // GET: Donations/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var donation = await _context.Donations.FindAsync(id);
            if (donation == null)
                return NotFound();

            ViewBag.Resources = new SelectList(_context.Resources.ToList(), "ResourceID", "Name");
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
                    TempData["SuccessMessage"] = "Donation updated successfully!";
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

            ViewBag.Resources = new SelectList(_context.Resources.ToList(), "ResourceID", "Name");
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
                TempData["SuccessMessage"] = "Donation deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
