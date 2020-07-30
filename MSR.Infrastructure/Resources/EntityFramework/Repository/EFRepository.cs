using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MSR.Domain.Exceptions;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MSR.Domain.Helpers;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.EntityFramework.Repository
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AnswerContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EFRepository(AnswerContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public void LoadCollection(TEntity entity, string navSelector)
        {
            _context.Entry(entity).Collection(navSelector).Load();
        }

        public void LoadReference(TEntity entity, Expression<Func<TEntity, object>> navSelector)
        {
            _context.Entry(entity).Reference(navSelector).Load();
        }

        public void LoadReference(TEntity entity, string navSelector)
        {
            _context.Entry(entity).Reference(navSelector).Load();
        }

        public virtual bool Exist(object id)
        {
            return Find(false, id) != null;
        }
        
        public virtual EntityEntry<TEntity> Add(TEntity entity)
        {
            //return _dbSet.Add(entity);
            return _dbSet.Add(entity);
        }

        public virtual async Task<EntityEntry<TEntity>> AddAsync(TEntity entity)
        {
            return await _dbSet.AddAsync(entity);
        }

        public virtual EntityEntry<TEntity> AddAndSaveChanges(TEntity entity)
        {
            EntityEntry<TEntity> addedEntity = Add(entity);
            SaveChanges();
            return addedEntity;
        }

        public virtual async Task<EntityEntry<TEntity>> AddAndSaveChangesAsync(TEntity entity)
        {
            EntityEntry<TEntity> addedEntity = await AddAsync(entity);
            await SaveChangesAsync();
            return addedEntity;
        }

        public virtual TEntity AttachAndInsert(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Add(entity);
            }
        }

        public virtual async Task InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            var taskList = new List<Task>();

            foreach(var entity in entities)
            {
                taskList.Add(AddAsync(entity));
            }

            await Task.WhenAll(taskList);
        }

        public void DeleteAndSaveChanges(bool validateOwnership, int id, bool force = false)
        {
            Delete(validateOwnership, id, force);
            SaveChanges();
        }
        public virtual void Delete(bool validateOwnership, int id, bool force = false)
        {
            TEntity entityToDelete = Find(validateOwnership, id);
            //if (entityToDelete != null)
            Delete(validateOwnership, entityToDelete, force);
        }

        public virtual void Delete(bool validateOwnership, TEntity entityToDelete, bool force = false)
        {
            if (entityToDelete == null)
                throw new DomainException($"{nameof(entityToDelete)} cannot be null");

            if (validateOwnership)
                ValidateOwnership(entityToDelete);

            DeletableEntity deletable;
            if (!force && (deletable = entityToDelete as DeletableEntity) != null)
            {
                deletable.IsActive = true;
            }
            else
            {
                if (_context.Entry(entityToDelete).State == EntityState.Detached)
                    _dbSet.Attach(entityToDelete);

                _dbSet.Remove(entityToDelete);
            }
        }

        public void DeleteAndSaveChanges(bool validateOwnership, TEntity entity, bool force = false)
        {
            Delete(validateOwnership, entity, force);
            SaveChanges();
        }

        public virtual void Detach(TEntity entityToDetach)
        {
            _context.Entry(entityToDetach).State = EntityState.Detached;
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void ApplyCurrentValues(TEntity entityToUpdate, TEntity updatedEntity)
        {
            _context.Entry(entityToUpdate).CurrentValues.SetValues(updatedEntity);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual IQueryable<TEntity> Query()
        {
            return _dbSet;
        }

        public virtual IQueryable<TEntity> QueryAsNoTracking()
        {
            return _dbSet.AsNoTracking();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.Count(filter);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbSet.CountAsync(filter);
        }

        public virtual int Count()
        {
            return _dbSet.Count();
        }

        public virtual async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public virtual bool Exists(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.Any(filter);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbSet.AnyAsync(filter);
        }

        public virtual TEntity FirstOrDefault(bool validateOwnership, Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Query();

            if (includes != null && includes.Length > 0)
                foreach (Expression<Func<TEntity, object>> include in includes)
                    query = query.Include(include);

            TEntity entity = query.FirstOrDefault(filter);

            if (validateOwnership)
                ValidateOwnership(entity);

            return entity;
        }

        public virtual TEntity FirstOrDefaultAsNoTracking(bool validateOwnership, Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Query().AsNoTracking();

            if (includes != null && includes.Length > 0)
                foreach (Expression<Func<TEntity, object>> include in includes)
                    query = query.Include(include);

            TEntity entity = query.FirstOrDefault(filter);

            if (validateOwnership)
                ValidateOwnership(entity);

            return entity;
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(bool validateOwnership, Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Query();

            if (includes != null && includes.Length > 0)
                foreach (Expression<Func<TEntity, object>> include in includes)
                    query = query.Include(include);

            TEntity entity = await query.FirstOrDefaultAsync(filter);

            if (validateOwnership)
                ValidateOwnership(entity);

            return entity;
        }

        public virtual async Task<TEntity> FirstOrDefaultAsNoTrackingAsync(bool validateOwnership, Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Query().AsNoTracking();

            if (includes != null && includes.Length > 0)
                foreach (Expression<Func<TEntity, object>> include in includes)
                    query = query.Include(include);

            TEntity entity = await query.FirstOrDefaultAsync(filter);

            if (validateOwnership)
                ValidateOwnership(entity);

            return entity;
        }


        public void Attach(TEntity entity)
        {
            _dbSet.Attach(entity);
        }

        public void ValidateOwnership(TEntity entity)
        {
            if (entity != null)
            {
                //TODO: define a way to get current userId from here.
                TrackableEntity trackableEntity = entity as TrackableEntity;
                if (trackableEntity == null)
                    throw new Exception("To be able to validate the ownership of the record, " +
                                        "the entity must implement the ITrackableEntity interface");

                if (trackableEntity.CreatedBy != DelegateHandler.GetCurrentUserId())
                    throw new Exception("You are not the owner of this record");
            }
        }

        public virtual TEntity Find(bool validateOwnership, params object[] keyValues)
        {
            TEntity entity = _dbSet.Find(keyValues);

            if (validateOwnership)
                ValidateOwnership(entity);

            return entity;
        }

        public virtual async Task<TEntity> FindAsync(bool validateOwnership, params object[] keyValues)
        {
            TEntity entity = await _dbSet.FindAsync(keyValues);

            if (validateOwnership)
                ValidateOwnership(entity);

            return entity;
        }

        public virtual IEnumerable<TEntity> SqlQuery(string query, params object[] parameters)
        {
            return _dbSet.FromSqlRaw(query, parameters).AsQueryable().ToList();
        }

        // http://www.codeproject.com/Articles/576330/Attaching-detached-POCO-to-EF-DbContext-simple-and
        public void AttachOrAddEntities(TEntity rootEntity)
        {
            // attach object graph in the Unchanged state
            _context.Set<TEntity>().Attach(rootEntity);

            // traverse all entities in context (hopefully they are all part of graph we just attached)
            foreach (var entry in _context.ChangeTracker.Entries<Entity>())
            {
                if (entry.Entity.Id == 0)
                    entry.State = EntityState.Added;
            }
        }

        /// <summary>
        /// CAUTION: By using this method, all entities tracked by this context that are in the Unchanged
        /// state and implement the IEntity interface, will end up in the Added or Modified state
        /// </summary>
        public void UpdateOrAddEntities(TEntity rootEntity)
        {
            // attach object graph in the Unchanged state
            _context.Set<TEntity>().Attach(rootEntity);

            foreach (var entry in _context.ChangeTracker.Entries<Entity>())
            {
                if (entry.State == EntityState.Unchanged)
                    entry.State = entry.Entity.Id == 0 ? EntityState.Added : EntityState.Modified;
            }
        }


        #region PRIVATE

        private void SaveChanges()
        {
            _context.SaveChanges();
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        #endregion

    }
}
