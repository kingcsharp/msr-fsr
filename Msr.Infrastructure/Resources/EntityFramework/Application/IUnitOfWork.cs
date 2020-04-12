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
        void SaveChanges();
        Task SaveChangesAsync();
        DbSet<T> Query<T>() where T : class;

        void LoadCollection<TEntity>(TEntity entity, string navSelector) where TEntity : class;
        void LoadReference<TEntity>(TEntity entity, Expression<Func<TEntity, object>> navSelector) where TEntity : class;
        void LoadReference<TEntity>(TEntity entity, string navSelector) where TEntity : class;

        /// <summary>
        /// Reloads entity from the database. See <see cref="DbEntityEntry.Reload"/>
        /// </summary>
        void ReloadEntity<T>(T entity) where T : class;
    }
}
