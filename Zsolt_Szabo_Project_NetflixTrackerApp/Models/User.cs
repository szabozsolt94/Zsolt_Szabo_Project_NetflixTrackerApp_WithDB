namespace Zsolt_Szabo_Project_NetflixTrackerApp.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
}
