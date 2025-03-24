namespace Zsolt_Szabo_Project_NetflixTrackerApp.Models
{
    public class UpdateAccountRequest
    {
        public int UserId { get; set; }
        public string FieldName { get; set; }
        public string NewValue { get; set; }
    }

}
