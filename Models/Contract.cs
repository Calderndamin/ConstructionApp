using Microsoft.CodeAnalysis;
using Microsoft.IdentityModel.Tokens;

namespace BlazorAppAttempt.Models
{
    public class Contract
    {
        public int ContractID { get; set; } // Primary Key
        public int ProjectID { get; set; } // Foreign Key
        public int SubcontractorID { get; set; } // Foreign Key
        public decimal Amount { get; set; }
        public decimal DueBalance { get; set; }
        public decimal TotalPaid { get; set; }
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

            foreach (var aspect in WorkAspects)
            {
                totalWeightedProgress += aspect.Progress * aspect.Weight;
            }
            Progress = totalWeightedProgress / 100;
        }

        public void UpdateTotalPaid()
        {
            if (Revisions != null)
            {
                TotalPaid = Revisions.Sum(r => r.AmountDue);
            }
            else
            {
                TotalPaid = 0;
            }



        }

    }
}
