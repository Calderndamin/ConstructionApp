using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorAppAttempt.Models
{
    public class ChangeOrder
    {
        [Key]
        public int ChangeOrderID { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal DueBalance { get; set; }

        public List<WorkAspect> WorkAspects { get; set; } = new();

        // Foreign key for Contract
        public int ContractID { get; set; }

        // Navigation property for Contract
        public Contract Contract { get; set; }
    }
}
