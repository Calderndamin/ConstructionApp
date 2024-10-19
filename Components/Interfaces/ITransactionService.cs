namespace BlazorAppAttempt.Components.Interfaces
{
    using BlazorAppAttempt.Models;

    public interface ITransactionService
    {
        Task<string?> GetUsernameAsync();
        Contract LoadContract(int contractId);
        List<Transaction> LoadTransactions(int contractId);
        void AddTransaction(Transaction newTransaction, string username, int contractId);
        void DeleteTransaction(int transactionId, int contractId);
    }

}