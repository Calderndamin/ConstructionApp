using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BlazorAppAttempt.Models;

namespace BlazorAppAttempt.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet properties for your models
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Subcontractor> Subcontractors { get; set; }
        public DbSet<WorkAspect> WorkAspects { get; set; }
        public DbSet<Revision> Revisions { get; set; }
        public DbSet<WorkAspectChange> WorkAspectChanges { get; set; }

        // Configure the entity mappings
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Table mappings
            modelBuilder.Entity<Contract>().ToTable("Contract");
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<Subcontractor>().ToTable("Subcontractor");
            modelBuilder.Entity<WorkAspect>().ToTable("WorkAspect");
            modelBuilder.Entity<Revision>().ToTable("Revision");
            modelBuilder.Entity<WorkAspectChange>().ToTable("WorkAspectChange");

            // Configure WorkAspectChange relationships
            modelBuilder.Entity<WorkAspectChange>()
                .HasOne(wac => wac.WorkAspect)
                .WithMany()  // No navigation property in WorkAspect back to WorkAspectChange
                .HasForeignKey(wac => wac.WorkAspectID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<WorkAspectChange>()
                .HasOne(wac => wac.Revision)
                .WithMany(r => r.Changes)
                .HasForeignKey(wac => wac.RevisionID)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
