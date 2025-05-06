using Microsoft.AspNetCore.Mvc;
using Zsolt_Szabo_Project_NetflixTrackerApp.Data;
using Zsolt_Szabo_Project_NetflixTrackerApp.Models;

namespace Zsolt_Szabo_Project_NetflixTrackerApp.Controllers
{
    [Route("watched")] // This is crucial for the route to match
    public class WatchedController : Controller
    {
        private readonly AppDbContext _context;

        public WatchedController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        public IActionResult AddToWatched(int movieID, int userID)
        {
            var existing = _context.Watched
                .FirstOrDefault(w => w.MovieID == movieID && w.UserID == userID);

            if (existing != null)
            {
                _context.Watched.Remove(existing);
                _context.SaveChanges();
                return Json(new { message = "Movie removed from Watched!" });
            }

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

