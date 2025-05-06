using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zsolt_Szabo_Project_NetflixTrackerApp.Data;
using Zsolt_Szabo_Project_NetflixTrackerApp.Models;
using System.Threading.Tasks;

namespace Zsolt_Szabo_Project_NetflixTrackerApp.Controllers
{
    [Route("Account")] // Ensure this route matches the URL structure
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        // Constructor to inject the AppDbContext
        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // Account page - retrieves user data
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userID = 1; // Assuming UserID is always 1 for now

            var user = await _context.Users
                .Where(u => u.UserID == userID)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return View(user); // Returns the correct view
        }

        // Update account details - Handle the update form submission
        [HttpPost("UpdateAccount")]
        public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.NewValue) || string.IsNullOrEmpty(request.FieldName))
            {
                return BadRequest("Invalid data.");
            }

            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Updating fields based on the request data
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

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Account updated successfully." });
        }
    }
}
