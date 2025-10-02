using DisasterAlleviationFoundation.Data;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviationFoundation.Controllers
{
    [Authorize]
    public class IncidentReportsController : Controller
    {
        private readonly GiftOfTheGiversDbContext _context;
        private readonly UserManager<User> _userManager;

        public IncidentReportsController(GiftOfTheGiversDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: IncidentReports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IncidentReports/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IncidentReport model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            model.UserId = user.Id;
            model.ReportedBy = user.Name;
            model.ReporterEmail = user.Email;

            _context.Add(model);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Incident report submitted successfully.";
            return RedirectToAction("MyReports");
        }

        // GET: My Reports
        public async Task<IActionResult> MyReports()
        {
            var user = await _userManager.GetUserAsync(User);
            var reports = await _context.IncidentReports
                .Where(r => r.UserId == user.Id)
                .ToListAsync();

            return View(reports);
        }

        // GET: Admin View All Reports
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllReports()
        {
            var reports = await _context.IncidentReports.Include(r => r.User).ToListAsync();
            return View(reports);
        }

        // POST: Approve report (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var report = await _context.IncidentReports.FindAsync(id);
            if (report != null)
            {
                report.IsApproved = true;
                _context.Update(report);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("AllReports");
        }

        // POST: Delete report (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var report = await _context.IncidentReports.FindAsync(id);
            if (report != null)
            {
                _context.IncidentReports.Remove(report);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("AllReports");
        }
    }
}
