using Microsoft.AspNetCore.Mvc;
using Zsolt_Szabo_Project_NetflixTrackerApp.Data;
using Zsolt_Szabo_Project_NetflixTrackerApp.Models;
using System.Linq;

namespace Zsolt_Szabo_Project_NetflixTrackerApp.Controllers
{
    [Route("WatchLater")]  // Explicit base route for Watch Later actions
    public class WatchLaterController : Controller
    {
        private readonly AppDbContext _context;

        public WatchLaterController(AppDbContext context)
        {
            _context = context;
        }

        // GET /WatchLater - Display the Watch Later page
        [HttpGet("")]
        public IActionResult Index()
        {
            // Fetch the list of movies marked as "Watch Later" for the user
            // For now, assuming UserID is hardcoded to 1 (you might want to retrieve it dynamically from the session or context)
            var userID = 1;

            // Assuming you have a WatchLater table and returning movies for the current user
            var moviesToWatchLater = _context.WatchLater
                .Where(w => w.UserID == userID)
                .Select(w => w.MovieID)  // Get only the movie IDs for now, expand as needed
                .ToList();

            // Pass the list of movies to the view (this could be more detailed)
            return View(moviesToWatchLater);
        }

        // POST /WatchLater/add - Add or remove movie from the Watch Later list
        [HttpPost("add")]
        public IActionResult AddToWatchLater(int movieID, int userID)
        {
            var existing = _context.WatchLater
                .FirstOrDefault(w => w.MovieID == movieID && w.UserID == userID);

            if (existing != null)
            {
                // If movie is already in Watch Later, remove it
                _context.WatchLater.Remove(existing);
                _context.SaveChanges();
                return Json(new { message = "Movie removed from Watch Later!" });
            }

            // Add movie to Watch Later if not already added
            var newWatchLater = new WatchLater
            {
                MovieID = movieID,
                UserID = userID
            };

            _context.WatchLater.Add(newWatchLater);
            _context.SaveChanges();

            return Json(new { message = "Movie added to Watch Later!" });
        }
    }
}

