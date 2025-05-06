using Microsoft.AspNetCore.Mvc;
using Zsolt_Szabo_Project_NetflixTrackerApp.Data;
using Zsolt_Szabo_Project_NetflixTrackerApp.Models;

namespace Zsolt_Szabo_Project_NetflixTrackerApp.Controllers
{
    [Route("watchlater")]
    public class WatchLaterController : Controller
    {
        private readonly AppDbContext _context;

        public WatchLaterController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("add")]  // Use 'add' route for consistency with WatchedController
        public IActionResult AddToWatchLater(int movieID, int userID)
        {
            // Check if the movie already exists in Watch Later
            var exists = _context.WatchLater
                .FirstOrDefault(w => w.MovieID == movieID && w.UserID == userID);

            if (exists != null)
            {
                // If it exists, remove it from Watch Later
                _context.WatchLater.Remove(exists);
                _context.SaveChanges();
                return Json(new { message = "Movie removed from Watch Later!" });
            }

            // If it doesn't exist, add it to Watch Later
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
