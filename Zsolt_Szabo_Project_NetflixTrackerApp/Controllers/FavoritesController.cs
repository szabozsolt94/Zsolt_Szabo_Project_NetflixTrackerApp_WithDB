using Microsoft.AspNetCore.Mvc;
using Zsolt_Szabo_Project_NetflixTrackerApp.Models;
using Zsolt_Szabo_Project_NetflixTrackerApp.Data;

namespace Zsolt_Szabo_Project_NetflixTrackerApp.Controllers
{
    [Route("favorites")]  // Explicitly specify the base route
    public class FavoritesController : Controller
    {
        private readonly AppDbContext _context;

        public FavoritesController(AppDbContext context)
        {
            _context = context;
        }

        // Map this route explicitly to /favorites/add
        [HttpPost("add")]
        public IActionResult AddToFavorites(int movieID, int userID)
        {
            // Check if the movie is already in the favorites
            var existingFavorite = _context.Favorites
                .FirstOrDefault(f => f.MovieID == movieID && f.UserID == userID);

            if (existingFavorite != null)
            {
                // Remove the movie from favorites if it's already there
                _context.Favorites.Remove(existingFavorite);
                _context.SaveChanges();
                return Json(new { message = "Movie removed from Favorites!" });
            }

            // Add to favorites if not already present
            var newFavorite = new Favorites
            {
                MovieID = movieID,
                UserID = userID
            };

            _context.Favorites.Add(newFavorite);
            _context.SaveChanges();

            return Json(new { message = "Movie added to Favorites!" });
        }

    }
}

