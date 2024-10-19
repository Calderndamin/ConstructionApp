namespace BlazorAppAttempt.Components.Interfaces
{
    using BlazorAppAttempt.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRevisionService
    {
        Task<List<Revision>> GetRevisionsAsync(int contractId);
        Task<List<WorkAspectChange>> GetWorkAspectChangesAsync(int contractId);
        Task<int> GetCurrentMaxRevisionNumberAsync(int contractId);
        Task AddRevisionAsync(Revision newRevision, List<WorkAspectChange> workAspectChanges, string username, int contractId);
        Task DeleteRevisionAsync(int contractId);
    }

}
