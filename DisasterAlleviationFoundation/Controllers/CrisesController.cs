using DisasterAlleviationFoundation.Data;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlleviationFoundation.Controllers
{
    public class CrisesController : Controller
    {
        private readonly GiftOfTheGiversDbContext _context;
        private readonly UserManager<User> _userManager;

        public CrisesController(GiftOfTheGiversDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var crises = await _context.Crises.Include(c => c.Tasks).ToListAsync();
            return View(crises);
        }

        [HttpPost]
        public async Task<IActionResult> Report(Crisis crisis)
        {
            if (ModelState.IsValid)
            {
                _context.Crises.Add(crisis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(crisis);
        }
    }
}
