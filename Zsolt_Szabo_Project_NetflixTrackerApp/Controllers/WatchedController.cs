using Microsoft.AspNetCore.Mvc;
using Zsolt_Szabo_Project_NetflixTrackerApp.Data;
using Zsolt_Szabo_Project_NetflixTrackerApp.Models;
using System.Linq;

namespace Zsolt_Szabo_Project_NetflixTrackerApp.Controllers
{
    [Route("Watched")]  // Explicit base route for Watched actions
    public class WatchedController : Controller
    {
        private readonly AppDbContext _context;

        public WatchedController(AppDbContext context)
        {
            _context = context;
        }

        // GET /Watched - Display the Watched page
        [HttpGet("")]
        public IActionResult Index()
        {
            // Fetch the list of movies marked as "Watched" for the user
            var userID = 1;

            // Fetch watched movie IDs for the current user
            var moviesInWatched = _context.Watched
                .Where(w => w.UserID == userID)
                .Select(w => w.MovieID)  // Only getting movie IDs for now (expand as needed)
                .ToList();

            // Pass the list of movie IDs to the view (you can expand this to include more details like title, etc.)
            return View(moviesInWatched);
        }

        // POST /Watched/add - Add or remove movie from the Watched list
        [HttpPost("add")]
        public IActionResult AddToWatched(int movieID, int userID)
        {
            var existing = _context.Watched
                .FirstOrDefault(w => w.MovieID == movieID && w.UserID == userID);

            if (existing != null)
            {
                // If the movie is already in the Watched list, remove it
                _context.Watched.Remove(existing);
                _context.SaveChanges();
                return Json(new { message = "Movie removed from Watched!" });
            }

            // If the movie is not in the Watched list, add it
            var newWatched = new Watched
            {
                MovieID = movieID,
                UserID = userID
            };

            _context.Watched.Add(newWatched);
            _context.SaveChanges();

            return Json(new { message = "Movie marked as Watched!" });
        }
    }
}
