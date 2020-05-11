using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using MSR.Domain.Helpers;
using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Infrastructure.Resources.EntityFramework
{
    public class AnswerContext : DbContext
    {
        private const string ConnectionString_ = "server=bang.msr-fsr.com;Initial Catalog=Answer3_Dev;User Id=msrfsr;Password=snRvf2rFVG7rGAVE;";
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerApproval> CustomerApproval { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<LocationApproval> LocationApproval { get; set; }
        public DbSet<MenuGroup> MenuGroup { get; set; }
        public DbSet<MenuItem> MenuItem{ get; set; }
        public DbSet<MenuRole> MenuRole { get; set; }
        public DbSet<MenuRolePermission> MenuRolePermission { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<PartApproval> PartApproval { get; set; }
        public DbSet<ProcedureApproval> ProcedureApproval { get; set; }
        public DbSet<ProcedureStepApproval> ProcedureStepApproval { get; set; }
        public DbSet<ProcedureStepDocumentApproval> ProcedureStepDocumentApproval { get; set; }
        public DbSet<ProcedureStepMonitorApproval> ProcedureStepMonitorApproval { get; set; }
        public DbSet<PurchaseOrderApproval> PurchaseOrderApproval { get; set; }
        public DbSet<PurchaseOrderProductApproval> PurchaseOrderProductApproval { get; set; }
        public DbSet<UserApproval> UserApproval { get; set; }
        public DbSet<UserRoleApproval> UserRoleApproval { get; set; }
        
        public AnswerContext() : base()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(ConsoleLoggerFactory).UseSqlServer(ConnectionString_);
            base.OnConfiguring(optionsBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;

            foreach (EntityEntry entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        HandleTrackableEntity(entry, now);
                        break;
                        //case EntityState.Deleted:
                        //    HandleDeletedEntry(entry);
                        //    HandleTrackableEntity(entry, now);
                        //    break;
                }
            }

            int result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        public override int SaveChanges()
        {
            var now = DateTime.UtcNow;

            foreach (EntityEntry entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        HandleTrackableEntity(entry, now);
                        break;
                        //case EntityState.Deleted:
                        //    HandleDeletedEntry(entry);
                        //    HandleTrackableEntity(entry, now);
                        //    break;
                }
            }

            int result = base.SaveChanges();

            return result;

        }

        private void HandleTrackableEntity(EntityEntry entry, DateTime now)
        {
            TrackableEntity trackable;
            if ((trackable = entry.Entity as TrackableEntity) != null)
            {
                int? answerUserId = DelegateHandler.GetCurrentUserId();
                if (entry.State == EntityState.Added)
                {
                    trackable.CreatedOn = now;
                    trackable.CreatedBy = answerUserId;
                }
                trackable.LastUpdatedOn = now;
                trackable.LastUpdatedBy = answerUserId;
            }
        }

        public static readonly ILoggerFactory ConsoleLoggerFactory = LoggerFactory.Create(builder =>
        {
            //builder.AddFilter((category, level) =>
            //category == DbLoggerCategory.Database.Command.Name
            //&& level == LogLevel.Information)
            //.AddConsole();
        });
    }
}
