namespace BlazorAppAttempt.Models
{
    public class Revision
    {
        public int RevisionID { get; set; } // Primary Key
        public int RevisionNumber { get; set; }
        public int ContractID { get; set; } // Foreign Key
        public DateTime DateCreated { get; set; }
        public string? Remarks { get; set; }

        public decimal AmountDue { get; set; }

        // Navigation Properties
        public Contract Contract { get; set; } // Make non-nullable if always linked

        public List<WorkAspectChange> Changes { get; set; } = new List<WorkAspectChange>();

        // New method to calculate the amount due based on progress made in this revision
        public decimal CalculateAmountDue()
        {
            if (Contract != null && Changes != null)
            {
                // Calculate the total progress change across all work aspects in this revision
                decimal totalProgressChange = Changes.Sum(change => (change.NewProgress - change.OldProgress ) * change.WorkAspect.Weight);

                // Calculate the amount due based on the contract's total amount and the total progress change
                AmountDue = Contract.Amount * (totalProgressChange / 10000);
            }
            return decimal.Round(AmountDue, 2);
        }
    }

}
