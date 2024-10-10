namespace BlazorAppAttempt.Models
{
    public class Subcontractor
    {
        public int SubcontractorID { get; set; } // Primary Key
        public required string Name { get; set; }
        public int ContactInfo { get; set; }

        // Navigation Property
        public List<Contract> Contracts { get; set; } = new List<Contract>();
    }
}
