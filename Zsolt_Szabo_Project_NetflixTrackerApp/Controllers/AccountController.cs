using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zsolt_Szabo_Project_NetflixTrackerApp.Data;
using Zsolt_Szabo_Project_NetflixTrackerApp.Models;
using System.Threading.Tasks;

namespace Zsolt_Szabo_Project_NetflixTrackerApp.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // Query the Users table in DB to get the user details
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userID = 1; // UserID is always 1 for simplicity

            var user = await _context.Users
                .Where(u => u.UserID == userID)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return View(user); // If user is found, return the view with user details
        }

        // Handle updating the user account details on Account page
        [HttpPost("UpdateAccount")]
        public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountRequest request)
        {
            // Check if the request is valid
            if (request == null || string.IsNullOrEmpty(request.NewValue) || string.IsNullOrEmpty(request.FieldName))
            {
                return BadRequest("Invalid data.");
            }

            // Get the user from DB based on the provided UserID
            var user = await _context.Users.FindAsync(request.UserId);

            // If no user if found return message
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Updating user details based on the field name
            switch (request.FieldName)
            {
                case "First Name":
                    user.FirstName = request.NewValue;
                    break;
                case "Last Name":
                    user.LastName = request.NewValue;
                    break;
                case "Username":
                    user.Username = request.NewValue;
                    break;
                case "Email":
                    user.Email = request.NewValue;
                    break;
            }

            // Save the updated user data in the Users table
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Account updated successfully." });
        }
    }
}
