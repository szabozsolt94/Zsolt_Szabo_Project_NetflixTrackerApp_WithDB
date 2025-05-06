using Microsoft.AspNetCore.Mvc;
using Zsolt_Szabo_Project_NetflixTrackerApp.Models;
using Zsolt_Szabo_Project_NetflixTrackerApp.Data;
using System.Linq;

namespace Zsolt_Szabo_Project_NetflixTrackerApp.Controllers
{
    [Route("Favorites")]  // Explicit base route for Favorites actions
    public class FavoritesController : Controller
    {
        private readonly AppDbContext _context;

        public FavoritesController(AppDbContext context)
        {
            _context = context;
        }

        // GET /Favorites - Display the Favorites page
        [HttpGet("")]
        public IActionResult Index()
        {
            // Fetch the list of movies marked as "Favorites" for the user
            var userID = 1;

            // Fetch favorite movie IDs for the current user
            var moviesInFavorites = _context.Favorites
                .Where(f => f.UserID == userID)
                .Select(f => f.MovieID)  // Only getting movie IDs for now (expand as needed)
                .ToList();

            // Pass the list of movie IDs to the view (you can expand this to include more details like title, etc.)
            return View(moviesInFavorites);
        }

        // POST /Favorites/add - Add or remove movie from the Favorites list
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
