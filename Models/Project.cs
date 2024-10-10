namespace BlazorAppAttempt.Models
{
    public class Project
    {
        public int ProjectID { get; set; } // Primary Key
        public required string ProjectName { get; set; }

        // Navigation Property
        public List<Contract> Contracts { get; set; } = new List<Contract>();
    }

}
