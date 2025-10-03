using DisasterAlleviationFoundation.Data;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviationFoundation.Controllers
{
    public class UsersController : Controller
    {
        private readonly GiftOfTheGiversDbContext _context;
        private readonly UserManager<User> _userManager;

        public UsersController(GiftOfTheGiversDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var model = new RegisterViewModel
            {
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Skills = user.Skills
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, RegisterViewModel model)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.Name = model.Name;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.Skills = model.Skills;
            user.Role = model.Role;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
