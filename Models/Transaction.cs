namespace BlazorAppAttempt.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int ContractID { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public TransactionType Type { get; set; }
    }

    public enum TransactionType
    {
        Credit,
        Debit
    }

}
