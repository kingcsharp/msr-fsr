using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Interfaces;
using MSR.Infrastructure.Resources.EntityFramework.Repository;

namespace MSR.Infrastructure.Resources.EntityFramework.Application
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        #region Repositories
        private IRepository<User> _users;
        private IRepository<Customer> _customers;
        private IRepository<CustomerApproval> _customerApprovals;
        private IRepository<Location> _locations;
        private IRepository<LocationApproval> _locationApprovals;
        private IRepository<MenuGroup> _menuGroups;
        private IRepository<MenuItem> _menuItems;
        private IRepository<MenuRole> _menuRoles;
        private IRepository<MenuRolePermission> _menuRolePermissions;
        private IRepository<Role> _roles;
        private IRepository<Part> _parts;
        private IRepository<Procedure> _procedures;
        private IRepository<Status> _status;
        private IRepository<UserRole> _userRoles;
        private IRepository<PartApproval> _partApproval;
        private IRepository<ProcedureApproval> _procedureApproval;
        private IRepository<ProcedureStepApproval> _procedureStepApproval;
        private IRepository<ProcedureStepDocumentApproval> _procedureStepDocumentApproval;
        private IRepository<ProcedureStepMonitorApproval> _procedureStepMonitorApproval;
        private IRepository<PurchaseOrderApproval> _purchaseOrderApproval;
        private IRepository<PurchaseOrderProductApproval> _purchaseOrderProductApproval;
        private IRepository<UserApproval> _userApproval;
        private IRepository<UserRoleApproval> _userRoleApproval;

        private IRepository<WorkflowGroup> _workflowGroup;
        private IRepository<WorkflowStage> _workflowStage;
        private IRepository<WorkflowGroupRoleMap> _workflowGroupRoleMap;
        private IRepository<ApprovalTransactionLog> _approvalTransactionLog;
        private IRepository<HelpPage> _helpPage;
        private IRepository<HelpPageRoleMap> _helpPageRoleMap;

        public IRepository<User> Users { get { return _users ??= new EFRepository<User>(Context); } }
        public IRepository<Customer> Customers { get { return _customers ??= new EFRepository<Customer>(Context); } }
        public IRepository<CustomerApproval> CustomerApprovals { get { return _customerApprovals ??= new EFRepository<CustomerApproval>(Context); } }
        public IRepository<Location> Locations { get { return _locations ??= new EFRepository<Location>(Context); } }
        public IRepository<LocationApproval> LocationApprovals { get { return _locationApprovals ??= new EFRepository<LocationApproval>(Context); } }
        public IRepository<MenuGroup> MenuGroups { get { return _menuGroups ??= new EFRepository<MenuGroup>(Context); } }
        public IRepository<MenuItem> MenuItems { get { return _menuItems ??= new EFRepository<MenuItem>(Context); } }
        public IRepository<MenuRole> MenuRoles { get { return _menuRoles ??= new EFRepository<MenuRole>(Context); } }
        public IRepository<MenuRolePermission> MenuRolePermissions { get { return _menuRolePermissions ??= new EFRepository<MenuRolePermission>(Context); } }
        public IRepository<Role> Roles { get { return _roles ??= new EFRepository<Role>(Context); } }
        public IRepository<Part> Parts { get { return _parts ??= new EFRepository<Part>(Context); } }
        public IRepository<Procedure> Procedures { get { return _procedures ??= new EFRepository<Procedure>(Context); } }
        public IRepository<Status> Status { get { return _status ??= new EFRepository<Status>(Context); } }
        public IRepository<UserRole> UserRoles { get { return _userRoles ??= new EFRepository<UserRole>(Context); } }
        public IRepository<PartApproval> PartApprovals { get { return _partApproval ??= new EFRepository<PartApproval>(Context); } }
        public IRepository<ProcedureApproval> ProcedureApprovals { get { return _procedureApproval ??= new EFRepository<ProcedureApproval>(Context); } }
        public IRepository<ProcedureStepApproval> ProcedureStepApprovals { get { return _procedureStepApproval ??= new EFRepository<ProcedureStepApproval>(Context); } }
        public IRepository<ProcedureStepDocumentApproval> ProcedureStepDocumentApprovals { get { return _procedureStepDocumentApproval ??= new EFRepository<ProcedureStepDocumentApproval>(Context); } }
        public IRepository<ProcedureStepMonitorApproval> ProcedureStepMonitorApprovals { get { return _procedureStepMonitorApproval ??= new EFRepository<ProcedureStepMonitorApproval>(Context); } }
        public IRepository<PurchaseOrderApproval> PurchaseOrderApprovals { get { return _purchaseOrderApproval ??= new EFRepository<PurchaseOrderApproval>(Context); } }
        public IRepository<PurchaseOrderProductApproval> PurchaseOrderProductApprovals { get { return _purchaseOrderProductApproval ??= new EFRepository<PurchaseOrderProductApproval>(Context); } }
        public IRepository<UserApproval> UserApprovals { get { return _userApproval ??= new EFRepository<UserApproval>(Context); } }
        public IRepository<UserRoleApproval> UserRoleApprovals { get { return _userRoleApproval ??= new EFRepository<UserRoleApproval>(Context); } }
        public IRepository<WorkflowGroup> WorkflowGroups { get { return _workflowGroup ??= new EFRepository<WorkflowGroup>(Context); } }
        public IRepository<WorkflowGroupRoleMap> WorkflowGroupRoleMaps { get { return _workflowGroupRoleMap ??= new EFRepository<WorkflowGroupRoleMap>(Context); } }
        public IRepository<ApprovalTransactionLog> ApprovalTransactionLogs { get { return _approvalTransactionLog ??= new EFRepository<ApprovalTransactionLog>(Context); } }
        public IRepository<HelpPage> HelpPages { get { return _helpPage ??= new EFRepository<HelpPage>(Context); } }
        public IRepository<HelpPageRoleMap> HelpPageRoles { get { return _helpPageRoleMap ??= new EFRepository<HelpPageRoleMap>(Context); } }
        public IRepository<WorkflowStage> WorkflowStages { get { return _workflowStage ??= new EFRepository<WorkflowStage>(Context); } }
        
        #endregion Repositories

        public UnitOfWork(AnswerContext context)
        {
            Context = context;
        }

        public AnswerContext Context { get; }

        public void Dispose()
        {
            Context.Dispose();
        }

        public async Task DisposeAsync()
        {
            await Context.DisposeAsync();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }

        public DbSet<T> Query<T>() where T : class
        {
            return Context.Set<T>();
        }

        public void LoadCollection<TEntity>(TEntity entity, string navSelector) where TEntity : class
        {
            Context.Entry(entity).Collection(navSelector).Load();
        }

        public async Task LoadCollectionAsync<TEntity>(TEntity entity, string navSelector) where TEntity : class
        {
            await Context.Entry(entity).Collection(navSelector).LoadAsync();
        }

        public void LoadReference<TEntity>(TEntity entity, Expression<Func<TEntity, object>> navSelector) where TEntity : class
        {
            Context.Entry(entity).Reference(navSelector).Load();
        }

        public void LoadReference<TEntity>(TEntity entity, string navSelector) where TEntity : class
        {
            Context.Entry(entity).Reference(navSelector).Load();
        }

        public async Task LoadReferenceAsync<TEntity>(TEntity entity, Expression<Func<TEntity, object>> navSelector) where TEntity : class
        {
            await Context.Entry(entity).Reference(navSelector).LoadAsync();
        }

        public async Task LoadReferenceAsync<TEntity>(TEntity entity, string navSelector) where TEntity : class
        {
            await Context.Entry(entity).Reference(navSelector).LoadAsync();
        }

        /// <summary>
        /// Reloads entity from the database. See <see cref="DbEntityEntry.Reload"/>
        /// </summary>
        public void ReloadEntity<T>(T entity) where T : class
        {
            Context.Entry(entity).Reload();
        }

        /// <summary>
        /// Reloads entity from the database. See <see cref="DbEntityEntry.ReloadAsync"/>
        /// </summary>
        public async Task ReloadEntityAsync<T>(T entity) where T : class
        {
            await Context.Entry(entity).ReloadAsync();
        }
    }
}
