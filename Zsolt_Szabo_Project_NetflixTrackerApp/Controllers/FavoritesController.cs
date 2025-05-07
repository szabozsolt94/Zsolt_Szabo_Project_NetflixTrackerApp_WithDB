using Microsoft.AspNetCore.Mvc;
using Zsolt_Szabo_Project_NetflixTrackerApp.Models;
using Zsolt_Szabo_Project_NetflixTrackerApp.Data;
using System.Linq;

namespace Zsolt_Szabo_Project_NetflixTrackerApp.Controllers
{
    [Route("Favorites")] 
    public class FavoritesController : Controller
    {
        private readonly AppDbContext _context;

        public FavoritesController(AppDbContext context)
        {
            _context = context;
        }

        // Display the user's favorite movies from DB (Favorites table) on Favorites page
        [HttpGet("")]
        public IActionResult Index()
        {
            var userID = 1;

            // Fetch the list of movies marked as favorites
            var moviesInFavorites = _context.Favorites
                .Where(f => f.UserID == userID)
                .Select(f => f.MovieID)
                .ToList();

            // Pass the list of MovieID's to the view, where we are passing them into MovieListRenderer.js
            return View(moviesInFavorites);
        }

        // Add or remove movie from the Favorites list
        [HttpPost("add")]
        public IActionResult AddToFavorites(int movieID, int userID)
        {
            var existingFavorite = _context.Favorites
                .FirstOrDefault(f => f.MovieID == movieID && f.UserID == userID);

            if (existingFavorite != null)
            {
                // If the movie is in favorites, remove it
                _context.Favorites.Remove(existingFavorite);
                _context.SaveChanges();
                return Json(new { message = "Movie removed from Favorites!" });
            }

            // If the movie is not in favorites, add it
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
