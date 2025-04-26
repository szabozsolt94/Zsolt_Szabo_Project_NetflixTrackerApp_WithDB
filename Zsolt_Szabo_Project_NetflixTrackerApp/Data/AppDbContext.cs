using Microsoft.EntityFrameworkCore;
using Zsolt_Szabo_Project_NetflixTrackerApp.Models;

namespace Zsolt_Szabo_Project_NetflixTrackerApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}
