using BlazorAppAttempt.Data;
using BlazorAppAttempt.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAppAttempt.Components.Interfaces;

public class RevisionService : IRevisionService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

    public RevisionService(IDbContextFactory<ApplicationDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<Revision>> GetRevisionsAsync(int contractId)
    {
        await using var db = _dbFactory.CreateDbContext();
        return await db.Revisions
            .AsNoTracking()
            .Include(r => r.Changes)
            .ThenInclude(c => c.WorkAspect)
            .Where(r => r.ContractID == contractId)
            .ToListAsync();
    }

    public async Task<List<WorkAspectChange>> GetWorkAspectChangesAsync(int contractId)
    {
        await using var db = _dbFactory.CreateDbContext();
        return await db.WorkAspects
            .AsNoTracking()
            .Where(wa => wa.ContractID == contractId) // Filter by ContractID
            .Select(wa => new WorkAspectChange
            {
                WorkAspect = wa,
                OldProgress = wa.Progress,
                NewProgress = wa.Progress
            })
            .ToListAsync();
    }

    public async Task<int> GetCurrentMaxRevisionNumberAsync(int contractId)
    {
        await using var db = _dbFactory.CreateDbContext();
        return await db.Revisions
            .Where(r => r.ContractID == contractId)
            .MaxAsync(r => (int?)r.RevisionNumber) ?? 0;
    }

    public async Task AddRevisionAsync(Revision newRevision, List<WorkAspectChange> workAspectChanges, string username, int contractId)
    {
        await using var db = _dbFactory.CreateDbContext();
        var contract = await db.Contracts
            .Include(c => c.Revisions)
            .FirstOrDefaultAsync(c => c.ContractID == contractId);

        if (contract == null) return;

        newRevision.ContractID = contractId;
        newRevision.CreatedBy = username;
        newRevision.Contract = contract;
        newRevision.Changes = workAspectChanges
            .Where(wac => wac.NewProgress != 0)
            .Select(wac => new WorkAspectChange
            {
                WorkAspectID = wac.WorkAspect.WorkAspectID,
                OldProgress = wac.OldProgress,
                NewProgress = wac.NewProgress
            })
            .ToList();

        db.Revisions.Add(newRevision);

        foreach (var change in newRevision.Changes)
        {
            var workAspect = await db.WorkAspects.FindAsync(change.WorkAspectID);
            if (workAspect != null)
            {
                workAspect.Progress = change.NewProgress;
            }
        }

        contract.Revisions.Add(newRevision);
        contract.UpdateOverallProgress(); // Update overall progress in the contract
        newRevision.CalculateAmountDue();
        contract.UpdateTotalPaid(); // Update total paid in the contract
        contract.TotalPaid -= newRevision.AmountDue;
        contract.DueBalance = contract.Amount - contract.TotalPaid;

        db.Entry(contract).State = EntityState.Modified;
        await db.SaveChangesAsync();
    }

    public async Task DeleteRevisionAsync(int contractId)
    {
        await using var db = _dbFactory.CreateDbContext();
        var latestRevision = await db.Revisions
            .Where(r => r.ContractID == contractId)
            .OrderByDescending(r => r.RevisionNumber)
            .FirstOrDefaultAsync();

        if (latestRevision == null) return;

        var revision = await db.Revisions
            .Include(r => r.Changes)
            .ThenInclude(c => c.WorkAspect)
            .FirstOrDefaultAsync(r => r.RevisionID == latestRevision.RevisionID);

        if (revision != null)
        {
            foreach (var change in revision.Changes)
            {
                var workAspect = await db.WorkAspects.FindAsync(change.WorkAspectID);
                if (workAspect != null)
                {
                    workAspect.Progress = change.OldProgress;
                }
            }

            db.WorkAspectChanges.RemoveRange(revision.Changes);
            db.Revisions.Remove(revision);

            var contract = await db.Contracts
                .Include(c => c.Revisions)
                .FirstOrDefaultAsync(c => c.ContractID == contractId);
            if (contract != null)
            {
                contract.UpdateOverallProgress();
                contract.Revisions.Remove(revision);
                contract.UpdateTotalPaid();
                contract.DueBalance = contract.Amount - contract.TotalPaid;
                db.Entry(contract).State = EntityState.Modified;
            }

            await db.SaveChangesAsync();
        }
    }
}
