using Microsoft.EntityFrameworkCore;
using MSR.App.Interfaces;
using MSR.App.Repository;
using MSR.Domain.Models;
using MSR.EFContext;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain.Application
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        #region Repositories
        private IRepository<User> _users;
        public IRepository<User> Users { get { return _users ?? (_users = new EFRepository<User>(Context)); } }
        #endregion Repositories
        public UnitOfWork()
        {
            _contex = new AnswerContext();
        }

        private readonly AnswerContext _contex;

        public AnswerContext Context
        {
            get { return _contex; }
        }

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
