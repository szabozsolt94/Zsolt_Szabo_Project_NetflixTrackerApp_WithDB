using Microsoft.AspNetCore.Mvc;
using Zsolt_Szabo_Project_NetflixTrackerApp.Data;
using Zsolt_Szabo_Project_NetflixTrackerApp.Models;

namespace Zsolt_Szabo_Project_NetflixTrackerApp.Controllers
{
    [Route("")]  // No base route for Home, using default behavior for Index
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        // Constructor to inject the AppDbContext
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // Home page route (default)
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
