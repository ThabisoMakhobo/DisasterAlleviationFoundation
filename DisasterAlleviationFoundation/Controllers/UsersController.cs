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

        {
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
        }

        // GET: Users/Details/5
        {

            if (user == null) return NotFound();

            return View(user);
        }

        // GET: Users/Create

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        {
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(user);
        }

        // GET: Users/Edit/5
        {
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        {

                {
                }
                {
                }

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        {
            if (user == null) return NotFound();

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        {
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(u => u.UserID == id);
        }
    }
}
