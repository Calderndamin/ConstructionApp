namespace BlazorAppAttempt.Models
{
    public class WorkAspect
    {
        public int WorkAspectID { get; set; } // Primary Key
        public int ContractID { get; set; } // Foreign Key
        public int? ChangeOrderID { get; set; } // Foreign key for ChangeOrder (optional)
        public required string Name { get; set; } // E.g., "Windows", "Floors"
        public decimal Weight { get; set; } // Weight
        public decimal Progress { get; set; } // Percentage
        public bool IsExtra { get; set; } // Extra work

        // Navigation Property
        public Contract Contract { get; set; } // Make non-nullable if required
        public ChangeOrder? ChangeOrder { get; set; }
    }

}
