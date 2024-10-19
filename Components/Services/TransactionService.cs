using BlazorAppAttempt.Data;
using BlazorAppAttempt.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using BlazorAppAttempt.Components.Interfaces;

public class TransactionService : ITransactionService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly CultureInfo _crCulture = new CultureInfo("es-CR");

    public TransactionService(IDbContextFactory<ApplicationDbContext> dbFactory, AuthenticationStateProvider authenticationStateProvider)
    {
        _dbFactory = dbFactory;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<string?> GetUsernameAsync()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        return user.Identity.IsAuthenticated ? user.Identity.Name : "Not Found";
    }

    public Contract? LoadContract(int contractId)
    {
        using var context = _dbFactory.CreateDbContext();
        return context.Contracts
            .Include(c => c.Transactions)
            .FirstOrDefault(c => c.ContractID == contractId);
    }

    public List<Transaction> LoadTransactions(int contractId)
    {
        using var context = _dbFactory.CreateDbContext();
        return context.Transactions
            .Where(t => t.ContractID == contractId)
            .ToList();
    }

    public void AddTransaction(Transaction newTransaction, string username, int contractId)
    {
        using var context = _dbFactory.CreateDbContext();
        newTransaction.TransactionID = 0;
        newTransaction.DateCreated = DateTime.Now;
        newTransaction.ContractID = contractId;
        newTransaction.CreatedBy = username;

        if (newTransaction.Type == TransactionType.Debit)
        {
            newTransaction.Amount = -newTransaction.Amount;
        }

        var contract = context.Contracts
            .Include(c => c.Transactions)
            .FirstOrDefault(c => c.ContractID == contractId);

        contract.Transactions.Add(newTransaction);
        context.Contracts.Update(contract);
        context.SaveChanges();
    }

    public void DeleteTransaction(int transactionId, int contractId)
    {
        using var context = _dbFactory.CreateDbContext();
        var transaction = context.Transactions.FirstOrDefault(t => t.TransactionID == transactionId);
        if (transaction != null)
        {
            var contract = context.Contracts
                .Include(c => c.Transactions)
                .FirstOrDefault(c => c.ContractID == contractId);

            contract.Transactions.Remove(transaction);
            context.Transactions.Remove(transaction);
            context.SaveChanges();
        }
    }
}
