﻿using Scorpio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
namespace Scorpio.Domain.Repositories
{
    public abstract class RepositoryBase<TEntity> : BasicRepositoryBase<TEntity>, IRepository<TEntity>
        where TEntity : class, IEntity
    {

        public virtual Type ElementType => GetQueryable().ElementType;

        public virtual Expression Expression => GetQueryable().Expression;

        public virtual IQueryProvider Provider => GetQueryable().Provider;

        public virtual IQueryable<TEntity> WithDetails()
        {
            return GetQueryable();
        }

        public virtual IQueryable<TEntity> WithDetails(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return GetQueryable();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return GetQueryable().GetEnumerator();
        }

        protected abstract IQueryable<TEntity> GetQueryable();

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate, bool autoSave = false)
        {
            foreach (var entity in GetQueryable().Where(predicate).ToList())
            {
                Delete(entity, autoSave);
            }
        }

        public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            Delete(predicate, autoSave);
            return Task.CompletedTask;
        }

    }

    public abstract class RepositoryBase<TEntity, TKey> : RepositoryBase<TEntity>, IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        public virtual TEntity Find(TKey id, bool includeDetails = true)
        {
            return includeDetails
                ? WithDetails().FirstOrDefault( EntityHelper.CreateEqualityExpressionForId<TEntity, TKey>(id))
                : GetQueryable().FirstOrDefault(EntityHelper.CreateEqualityExpressionForId<TEntity, TKey>(id));
        }

        public virtual TEntity Get(TKey id, bool includeDetails = true)
        {
            var entity = Find(id, includeDetails);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        public virtual Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Get(id, includeDetails));
        }

        public virtual Task<TEntity> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Find(id, includeDetails));
        }

        public virtual void Delete(TKey id, bool autoSave = false)
        {
            var entity = Find(id, includeDetails: false);
            if (entity == null)
            {
                return;
            }

            Delete(entity, autoSave);
        }

        public virtual Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            Delete(id, autoSave);
            return Task.CompletedTask;
        }
    }
}
