using Microsoft.AspNetCore.Mvc;
using Zsolt_Szabo_Project_NetflixTrackerApp.Data;
using Zsolt_Szabo_Project_NetflixTrackerApp.Models;
using System.Linq;

namespace Zsolt_Szabo_Project_NetflixTrackerApp.Controllers
{
    [Route("WatchLater")]
    public class WatchLaterController : Controller
    {
        private readonly AppDbContext _context;

        public WatchLaterController(AppDbContext context)
        {
            _context = context;
        }

        // Display the user's watch later movies from DB (WatchLater table) on Watch Later page
        [HttpGet("")]
        public IActionResult Index()
        {
            var userID = 1;

            // Fetch the list of movies marked as watched
            var moviesToWatchLater = _context.WatchLater
                .Where(w => w.UserID == userID)
                .Select(w => w.MovieID) 
                .ToList();

            // Pass the list of MovieID's to the view, where we are passing them into MovieListRenderer.js
            return View(moviesToWatchLater);
        }

        // Add or remove movie from the Watch Later list
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

