using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zsolt_Szabo_Project_NetflixTrackerApp.Data;
using Zsolt_Szabo_Project_NetflixTrackerApp.Models;
using System.Threading.Tasks;

namespace Zsolt_Szabo_Project_NetflixTrackerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        // Constructor to inject the AppDbContext
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Add this action to retrieve user data for the account page
        public async Task<IActionResult> Account()
        {
            // For now, we're assuming a logged-in user with UserID 1
            var userID = 1;  // Replace with actual logged-in user's ID in real-world scenario

            var user = await _context.Users
                .Where(u => u.UserID == userID)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        public IActionResult Favorites()
        {
            return View();
        }

        public IActionResult WatchLater()
        {
            return View();
        }

        public IActionResult Watched()
        {
            return View();
        }
    }
}
