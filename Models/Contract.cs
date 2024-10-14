using Microsoft.CodeAnalysis;

namespace BlazorAppAttempt.Models
{
    public class Contract
    {
        public int ContractID { get; set; } // Primary Key
        public int ProjectID { get; set; } // Foreign Key
        public int SubcontractorID { get; set; } // Foreign Key
        public decimal Amount { get; set; }
        public decimal DueBalance { get; set; }
        public decimal Progress { get; set; } // Progress Percentage
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? OtherContractDetails { get; set; }

        // Navigation Properties
        public Subcontractor Subcontractor { get; set; }
        public Project Project { get; set; }

        // Initialize lists to prevent null references
        public List<WorkAspect> WorkAspects { get; set; } = new();
        public List<Revision> Revisions { get; set; } = new();

        public List<Transaction> Transactions { get; set; } = new();

        // Method to calculate and update the overall progress based on work aspects
        public void UpdateOverallProgress()
        {
            decimal totalWeightedProgress = 0;
            decimal totalWeight = 0;

            foreach (var aspect in WorkAspects)
            {
                totalWeightedProgress += aspect.Progress * aspect.Weight;
                totalWeight += aspect.Weight;
            }

            Progress = totalWeight > 0 ? totalWeightedProgress / totalWeight : 0;
        }

        public void UpdateDueBalance()
        {
            decimal totalAmountDue = 0;

            foreach (var revision in Revisions)
            {
                totalAmountDue += revision.CalculateAmountDue();
            }

            foreach (var transaction in Transactions)
            {
                if (transaction.Type == TransactionType.Credit)
                {
                    totalAmountDue -= transaction.Amount;
                }
                else
                {
                    totalAmountDue += transaction.Amount;
                }
            }

            DueBalance = totalAmountDue;
        }
    }
}
