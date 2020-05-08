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
        #endregion Repositories
        public UnitOfWork()
        {
            Context = new AnswerContext();
        }

        public AnswerContext Context { get; }

        public void Dispose()
        {
            Context.Dispose();
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
            return Context.Set<T>();//.AsNoTracking();
        }

        public void LoadCollection<TEntity>(TEntity entity, string navSelector) where TEntity : class
        {
            Context.Entry(entity).Collection(navSelector).Load();
        }

        public void LoadReference<TEntity>(TEntity entity, Expression<Func<TEntity, object>> navSelector) where TEntity : class
        {
            Context.Entry(entity).Reference(navSelector).Load();
        }

        public void LoadReference<TEntity>(TEntity entity, string navSelector) where TEntity : class
        {
            Context.Entry(entity).Reference(navSelector).Load();
        }

        /// <summary>
        /// Reloads entity from the database. See <see cref="DbEntityEntry.Reload"/>
        /// </summary>
        public void ReloadEntity<T>(T entity) where T : class
        {
            Context.Entry(entity).Reload();
        }
    }
}
