namespace BlazorAppAttempt.Models
{
    public class Revision
    {
        public int RevisionID { get; set; } // Primary Key
        public int RevisionNumber { get; set; }
        public int ContractID { get; set; } // Foreign Key
        public DateTime DateCreated { get; set; }
        public string? Remarks { get; set; }
        public string CreatedBy { get; set; }
        public decimal AmountDue { get; set; }

        // Navigation Properties
        public Contract Contract { get; set; } // Make non-nullable if always linked

        public List<WorkAspectChange> Changes { get; set; } = new List<WorkAspectChange>();



    }

}
