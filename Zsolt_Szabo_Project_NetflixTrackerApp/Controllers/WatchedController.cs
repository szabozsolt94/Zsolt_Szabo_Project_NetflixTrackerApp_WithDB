using Microsoft.AspNetCore.Mvc;
using Zsolt_Szabo_Project_NetflixTrackerApp.Data;
using Zsolt_Szabo_Project_NetflixTrackerApp.Models;
using System.Linq;

namespace Zsolt_Szabo_Project_NetflixTrackerApp.Controllers
{
    [Route("Watched")] 
    public class WatchedController : Controller
    {
        private readonly AppDbContext _context;

        public WatchedController(AppDbContext context)
        {
            _context = context;
        }

        // Display the user's watched movies from DB (Watched table) on Watched page
        [HttpGet("")]
        public IActionResult Index()
        {
            var userID = 1;

            // Fetch the list of movies marked as watched
            var moviesInWatched = _context.Watched
                .Where(w => w.UserID == userID)
                .Select(w => w.MovieID)
                .ToList();

            // Pass the list of MovieID's to the view, where we are passing them into MovieListRenderer.js
            return View(moviesInWatched);
        }

        // Add or remove movie from the Watched list
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
