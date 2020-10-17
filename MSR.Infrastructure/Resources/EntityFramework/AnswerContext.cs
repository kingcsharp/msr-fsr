using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using MSR.Domain.Helpers;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using TimeZone = MSR.Infrastructure.Resources.EntityFramework.Entities.TimeZone;

namespace MSR.Infrastructure.Resources.EntityFramework
{
    public class AnswerContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerApproval> CustomerApproval { get; set; }
        public DbSet<CycleCountHistory> CycleCountHistory { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<LocationApproval> LocationApproval { get; set; }
        public DbSet<MenuGroup> MenuGroup { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<MenuRole> MenuRole { get; set; }
        public DbSet<MenuRolePermission> MenuRolePermission { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<PartApproval> PartApproval { get; set; }
        public DbSet<ProductApproval> ProductApproval { get; set; }
        public DbSet<ProcedureApproval> ProcedureApproval { get; set; }
        public DbSet<ProcedureStepApproval> ProcedureStepApproval { get; set; }
        public DbSet<ProcedureStepDocumentApproval> ProcedureStepDocumentApproval { get; set; }
        public DbSet<ProcedureStepMonitorApproval> ProcedureStepMonitorApproval { get; set; }
        public DbSet<PurchaseOrderApproval> PurchaseOrderApproval { get; set; }
        public DbSet<PurchaseOrderProductApproval> PurchaseOrderProductApproval { get; set; }
        public DbSet<UserApproval> UserApproval { get; set; }
        public DbSet<UserRoleApproval> UserRoleApproval { get; set; }
        public DbSet<MonitorInputType> MonitorInputType { get; set; }
        public DbSet<MonitorListItem> MonitorListItem { get; set; }
        public DbSet<Workflow> Workflow { get; set; }
        public DbSet<WorkflowStageMap> WorkflowStageMap { get; set; }
        public DbSet<WorkflowGroupUserMap> WorkflowGroupUserMap { get; set; }
        public DbSet<WorkflowStage> WorkflowStage { get; set; }
        public DbSet<WorkflowGroup> WorkflowGroup { get; set; }
        public DbSet<WorkflowGroupRoleMap> WorkflowGroupRoleMap { get; set; }
        public DbSet<HelpPage> HelpPage { get; set; }
        public DbSet<HelpPageRoleMap> HelpPageRoleMap { get; set; }
        public DbSet<WorkflowGroupStageMap> WorkflowGroupStageMap { get; set; }
        public DbSet<ApprovalTransactionLog> ApprovalTransactionLog { get; set; }
        public DbSet<ProcedureStepTemplate> ProcedureStepTemplate { get; set; }
        public DbSet<ProcedureType> ProcedureType { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<DocumentApproval> DocumentApproval { get; set; }
        public DbSet<DocumentRoleMap> DocumentRoleMap { get; set; }
        public DbSet<DocumentEntityMap> DocumentEntityMap { get; set; }
        public DbSet<Part> Part { get; set; }
        public DbSet<Procedure> Procedure { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public DbSet<ProductStep> ProductStep { get; set; }
        public DbSet<PartSubPartMap> PartSubPartMap { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<File> File { get; set; }
        public DbSet<FileEntityMap> FileEntityMap { get; set; }
        public DbSet<Sensor> SensorItem { get; set; }
        public DbSet<SensorValue> SensorValue { get; set; }
        public DbSet<Quote> Quote { get; set; }
        public DbSet<TimeZone> Timezone { get; set; }
        public DbSet<AdminCostSetting> AdminCostSetting { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<ReportCategory> ReportCategory { get; set; }
        public DbSet<ReportCategoryMap> ReportCategoryMap { get; set; }
        public DbSet<ReportDashboard> ReportDashboard { get; set; }
        public DbSet<ReportDashboardMap> ReportDashboardMap { get; set; }

        public DbSet<EquipmentMaintenance> EquipmentMaintenance { get; set; }
        public DbSet<WorkOrderMessage> WorkOrderMessage { get; set; }

        public AnswerContext() : base()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public AnswerContext(DbContextOptions<AnswerContext> options)
        : base(options)
        { }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
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
                    }
                }

                int result = await base.SaveChangesAsync(cancellationToken);

                return result;
            }
            catch (Exception)
            {
                throw;
            }

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
                }
            }

            int result = base.SaveChanges();

            return result;

        }

        private void HandleTrackableEntity(EntityEntry entry, DateTime now)
        {
            CreatableEntity creatable;
            if ((creatable = entry.Entity as CreatableEntity) != null)
            {
                int? answerUserId = CurrentUser.GetId();
                User user = null;
                if (answerUserId.HasValue)
                {
                    user = User.FirstOrDefault(i => i.Id == answerUserId.Value);
                }

                if (entry.State == EntityState.Added)
                {
                    creatable.CreatedOn = now;
                    creatable.Created = user;
                }
                TrackableEntity trackable;
                if ((trackable = entry.Entity as TrackableEntity) != null)
                {
                    trackable.LastUpdatedOn = now;
                    trackable.LastUpdated = user;
                }
            }
        }

        public static readonly ILoggerFactory ConsoleLoggerFactory = LoggerFactory.Create(builder =>
        {
        });

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetInterfaces()
                    .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .ToList();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}
