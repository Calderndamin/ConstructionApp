namespace BlazorAppAttempt.Models
{
    public class WorkAspectChange
    {
        public int WorkAspectChangeID { get; set; } // Primary Key
        public int WorkAspectID { get; set; } // Foreign Key to WorkAspect
        public WorkAspect WorkAspect { get; set; } // Non-nullable if required
        public decimal OldProgress { get; set; } // From
        public decimal NewProgress { get; set; } // To

        // Link back to the revision for relationships
        public int RevisionID { get; set; } // Foreign Key to Revision
        public Revision Revision { get; set; } // Non-nullable if required
    }

}
