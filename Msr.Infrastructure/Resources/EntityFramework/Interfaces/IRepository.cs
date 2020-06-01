using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.EntityFramework.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Find(bool validateOwnership, params object[] keyValues);
        Task<TEntity> FindAsync(bool validateOwnership, params object[] keyValues);
        bool Exist(object id);
        EntityEntry<TEntity> Add(TEntity entity);
        Task<EntityEntry<TEntity>> AddAsync(TEntity entity);
        EntityEntry<TEntity> AddAndSaveChanges(TEntity entity);
        Task<EntityEntry<TEntity>> AddAndSaveChangesAsync(TEntity entity);
        TEntity AttachAndInsert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        Task InsertRangeAsync(IEnumerable<TEntity> entities);
        void Delete(bool validateOwnership, int id, bool force = false);
        void DeleteAndSaveChanges(bool validateOwnership, int id, bool force = false);
        void Delete(bool validateOwnership, TEntity entity, bool force = false);
        void DeleteAndSaveChanges(bool validateOwnership, TEntity entity, bool force = false);
        void Detach(TEntity entity);
        void ApplyCurrentValues(TEntity entityToUpdate, TEntity updatedEntity);
        IEnumerable<TEntity> SqlQuery(string query, params object[] parameters);
        int Count(Expression<Func<TEntity, bool>> filter);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter);
        int Count();
        Task<int> CountAsync();
        bool Exists(Expression<Func<TEntity, bool>> filter);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter);
        IQueryable<TEntity> Query();
        IQueryable<TEntity> QueryAsNoTracking();
        void LoadReference(TEntity entity, Expression<Func<TEntity, object>> navSelector);
        void LoadCollection(TEntity entity, string navSelector);
        void LoadReference(TEntity entity, string navSelector);
        void Attach(TEntity entity);
        void AttachOrAddEntities(TEntity rootEntity);
        void Update(TEntity entityToUpdate);

        TEntity FirstOrDefault(bool validateOwnership, Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includes);

        TEntity FirstOrDefaultAsNoTracking(bool validateOwnership, Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> FirstOrDefaultAsync(bool validateOwnership, Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includes);
    }
}
