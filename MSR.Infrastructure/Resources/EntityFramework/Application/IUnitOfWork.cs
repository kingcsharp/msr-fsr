using Microsoft.EntityFrameworkCore;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.EntityFramework.Application
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Customer> Customers { get; }
        IRepository<CustomerApproval> CustomerApprovals { get; }
        IRepository<Location> Locations { get; }
        IRepository<LocationApproval> LocationApprovals { get; }
        IRepository<MenuGroup> MenuGroups { get; }
        IRepository<MenuItem> MenuItems { get; }
        IRepository<MenuRole> MenuRoles { get; }
        IRepository<MenuRolePermission> MenuRolePermissions { get; }
        IRepository<Role> Roles { get; }
        IRepository<Part> Parts { get; }
        IRepository<Procedure> Procedures { get; }
        IRepository<ProcedureStep> ProcedureSteps { get; }
        IRepository<ProcedureStepMonitor> ProcedureStepMonitors { get; }
        IRepository<ProcedureStepTemplate> ProcedureStepTemplates { get; }
        IRepository<ProcedureType> ProcedureTypes { get; }
        IRepository<Status> Status { get; }
        IRepository<UserRole> UserRoles { get; }
        IRepository<PartApproval> PartApprovals { get; }
        IRepository<ProcedureApproval> ProcedureApprovals { get; }
        IRepository<ProcedureStepApproval> ProcedureStepApprovals { get; }
        IRepository<ProcedureStepDocumentApproval> ProcedureStepDocumentApprovals { get; }
        IRepository<DocumentApproval> DocumentApprovals { get; }
        IRepository<ProductApproval> ProductApprovals { get; }

        IRepository<ProcedureStepMonitorApproval> ProcedureStepMonitorApprovals { get; }
        IRepository<PurchaseOrderApproval> PurchaseOrderApprovals { get; }
        IRepository<PurchaseOrderProductApproval> PurchaseOrderProductApprovals { get; }
        IRepository<UserApproval> UserApprovals { get; }
        IRepository<UserRoleApproval> UserRoleApprovals { get; }
        IRepository<Workflow> Workflows { get; }
        IRepository<WorkflowStageMap> WorkflowStageMaps { get; }
        IRepository<WorkflowGroupUserMap> WorkflowGroupUserMaps { get; }
        IRepository<WorkflowGroup> WorkflowGroups { get; }
        IRepository<WorkflowStage> WorkflowStages { get; }
        IRepository<WorkflowGroupRoleMap> WorkflowGroupRoleMaps { get; }
        IRepository<WorkflowGroupStageMap> WorkflowGroupStageMaps { get; }
        IRepository<WorkflowActivityMap> WorkflowActivityMaps { get; }
        IRepository<WorkflowActivity> WorkflowActivities { get; }
        IRepository<ApprovalTransactionLog> ApprovalTransactionLogs { get; }
        IRepository<HelpPage> HelpPages { get; }
        IRepository<HelpPageRoleMap> HelpPageRoles { get; }
        IRepository<MonitorInputType> MonitorInputTypes { get; }
        IRepository<MonitorListItem> MonitorListItems { get; }
        IRepository<Document> Documents { get; }
        IRepository<Product> Products { get; }
        IRepository<PurchaseOrder> PurchaseOrders { get; }
        IRepository<PartSubPartMap> PartSubPartMaps { get ; }
        IRepository<Invoice> Invoices { get; }
        IRepository<InvoiceItem> InvoiceItems { get; }
        IRepository<WorkOrder> WorkOrders { get; }
        IRepository<File> Files { get; }
        IRepository<FileEntityMap> FileEntityMap { get; }

        IRepository<RoleChildRoleMap> RoleChildRoleMaps { get; }
        IRepository<Sensor> Sensors { get; }


        void SaveChanges();
        Task SaveChangesAsync();
        DbSet<T> Query<T>() where T : class;

        void LoadCollection<TEntity>(TEntity entity, string navSelector) where TEntity : class;
        Task LoadCollectionAsync<TEntity>(TEntity entity, string navSelector) where TEntity : class;
        void LoadReference<TEntity>(TEntity entity, Expression<Func<TEntity, object>> navSelector) where TEntity : class;
        void LoadReference<TEntity>(TEntity entity, string navSelector) where TEntity : class;
        Task LoadReferenceAsync<TEntity>(TEntity entity, Expression<Func<TEntity, object>> navSelector) where TEntity : class;
        Task LoadReferenceAsync<TEntity>(TEntity entity, string navSelector) where TEntity : class;
        /// <summary>
        /// Reloads entity from the database. See <see cref="DbEntityEntry.Reload"/>
        /// </summary>
        void ReloadEntity<T>(T entity) where T : class;
        /// <summary>
        /// Reloads entity from the database. See <see cref="DbEntityEntry.ReloadAsync"/>
        /// </summary>
        Task ReloadEntityAsync<T>(T entity) where T : class;
    }
}
