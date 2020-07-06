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
        private IRepository<WorkflowActivityMap> _workflowActivityMap;
        private IRepository<WorkflowActivity> _workflowActivity;
        private IRepository<WorkflowGroupRoleMap> _workflowGroupRoleMap;
        private IRepository<WorkflowStageMap> _workflowStageMap;
        private IRepository<Workflow> _workflow;
        private IRepository<WorkflowGroupUserMap> _workflowGroupUserMap;
        private IRepository<WorkflowGroupStageMap> _workflowGroupStageMaps;

        public IRepository<User> Users { get { return _users ?? (_users = new EFRepository<User>(Context)); } }
        public IRepository<Customer> Customers { get { return _customers ?? (_customers = new EFRepository<Customer>(Context)); } }
        public IRepository<CustomerApproval> CustomerApprovals { get { return _customerApprovals ?? (_customerApprovals = new EFRepository<CustomerApproval>(Context)); } }
        public IRepository<Location> Locations { get { return _locations ?? (_locations = new EFRepository<Location>(Context)); } }
        public IRepository<LocationApproval> LocationApprovals { get { return _locationApprovals ?? (_locationApprovals = new EFRepository<LocationApproval>(Context)); } }
        public IRepository<MenuGroup> MenuGroups { get { return _menuGroups ?? (_menuGroups = new EFRepository<MenuGroup>(Context)); } }
        public IRepository<MenuItem> MenuItems { get { return _menuItems ?? (_menuItems = new EFRepository<MenuItem>(Context)); } }
        public IRepository<MenuRole> MenuRoles { get { return _menuRoles ?? (_menuRoles = new EFRepository<MenuRole>(Context)); } }
        public IRepository<MenuRolePermission> MenuRolePermissions { get { return _menuRolePermissions ?? (_menuRolePermissions = new EFRepository<MenuRolePermission>(Context)); } }
        public IRepository<Role> Roles { get { return _roles ?? (_roles = new EFRepository<Role>(Context)); } }
        public IRepository<Status> Status { get { return _status ?? (_status = new EFRepository<Status>(Context)); } }
        public IRepository<UserRole> UserRoles { get { return _userRoles ?? (_userRoles = new EFRepository<UserRole>(Context)); } }
        public IRepository<PartApproval> PartApprovals { get { return _partApproval ?? (_partApproval = new EFRepository<PartApproval>(Context)); } }
        public IRepository<ProcedureApproval> ProcedureApprovals { get { return _procedureApproval ?? (_procedureApproval = new EFRepository<ProcedureApproval>(Context)); } }
        public IRepository<ProcedureStepApproval> ProcedureStepApprovals { get { return _procedureStepApproval ?? (_procedureStepApproval = new EFRepository<ProcedureStepApproval>(Context)); } }
        public IRepository<ProcedureStepDocumentApproval> ProcedureStepDocumentApprovals { get { return _procedureStepDocumentApproval ?? (_procedureStepDocumentApproval = new EFRepository<ProcedureStepDocumentApproval>(Context)); } }
        public IRepository<ProcedureStepMonitorApproval> ProcedureStepMonitorApprovals { get { return _procedureStepMonitorApproval ?? (_procedureStepMonitorApproval = new EFRepository<ProcedureStepMonitorApproval>(Context)); } }
        public IRepository<PurchaseOrderApproval> PurchaseOrderApprovals { get { return _purchaseOrderApproval ?? (_purchaseOrderApproval = new EFRepository<PurchaseOrderApproval>(Context)); } }
        public IRepository<PurchaseOrderProductApproval> PurchaseOrderProductApprovals { get { return _purchaseOrderProductApproval ?? (_purchaseOrderProductApproval = new EFRepository<PurchaseOrderProductApproval>(Context)); } }
        public IRepository<UserApproval> UserApprovals { get { return _userApproval ?? (_userApproval = new EFRepository<UserApproval>(Context)); } }
        public IRepository<UserRoleApproval> UserRoleApprovals { get { return _userRoleApproval ?? (_userRoleApproval = new EFRepository<UserRoleApproval>(Context)); } }
        public IRepository<WorkflowGroup> WorkflowGroups { get { return _workflowGroup ?? (_workflowGroup = new EFRepository<WorkflowGroup>(Context)); } }
        public IRepository<WorkflowStage> WorkflowStages { get { return _workflowStage ?? (_workflowStage = new EFRepository<WorkflowStage>(Context)); } }
        public IRepository<Workflow> Workflows { get { return _workflow ?? (_workflow = new EFRepository<Workflow>(Context)); } }
        public IRepository<WorkflowStageMap> WorkflowStagesMap { get { return _workflowStageMap ?? (_workflowStageMap = new EFRepository<WorkflowStageMap>(Context)); } }
        public IRepository<WorkflowGroupRoleMap> WorkflowGroupRoleMaps { get { return _workflowGroupRoleMap ?? (_workflowGroupRoleMap = new EFRepository<WorkflowGroupRoleMap>(Context)); } }
        public IRepository<WorkflowActivityMap> WorkflowActivityMaps { get { return _workflowActivityMap ?? (_workflowActivityMap = new EFRepository<WorkflowActivityMap>(Context)); } }
        public IRepository<WorkflowActivity> WorkflowActivities { get { return _workflowActivity ?? (_workflowActivity = new EFRepository<WorkflowActivity>(Context)); } }
        public IRepository<WorkflowGroupUserMap> WorkflowGroupUserMaps { get { return _workflowGroupUserMap ?? (_workflowGroupUserMap = new EFRepository<WorkflowGroupUserMap>(Context)); } }
        public IRepository<WorkflowGroupStageMap> WorkflowGroupStageMaps { get { return _workflowGroupStageMaps ?? (_workflowGroupStageMaps = new EFRepository<WorkflowGroupStageMap>(Context)); } }

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
