using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using SourceScrub.DataAccess.Abstraction;
using Deliverables.Data.Base;
using Deliverables.Data.Abstraction;

namespace Deliverables.Data.Implementation
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        #region Private fields

        protected readonly IDataContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        #endregion

        #region .ctor

        public BaseRepository(IDataContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        #endregion

        protected IQueryable<TEntity> Query
        {
            get
            {
                if (!IsDeletable())
                {
                    return _dbSet;
                }

                return _dbSet.Where(e => !e.Deleted);
            }
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _dbSet.CountAsync(whereExpression);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity != null && entity.Deleted)
            {
                return null;
            }

            return entity;
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> wherePredicate = null, Expression<Func<TEntity, int>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Query;

            if (wherePredicate != null)
            {
                query = query.Where(wherePredicate);
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }

            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return await GetListAsync(wherePredicate, null, includes);
        }

        public async Task<List<TResult>> SelectAsync<TResult>(Expression<Func<TEntity, TResult>> selectExpression,
            Expression<Func<TEntity, bool>> wherePredicate = null,
            Expression<Func<TEntity, int>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Query;

            if (wherePredicate != null)
            {
                query = query.Where(wherePredicate);
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }

            return await query.Select(selectExpression).ToListAsync();
        }
      
        public void Add(params TEntity[] entities)
        {
            _dbSet.AddRange(entities);
        }

        public void MarkAsModified(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public bool IsAdded(TEntity entity)
        {
            return _dbContext.Entry(entity).State == EntityState.Added;
        }

        public void AttachIfDetached(params TEntity[] entities)
        {
            foreach (TEntity entity in entities)
            {
                if (entity != null && _dbContext.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
            }
        }

        public void AddOrUpdate(Expression<Func<TEntity, object>> identifierProperty, params TEntity[] entities)
        {
            _dbSet.AddOrUpdate(identifierProperty, entities);
        }

        public void Delete(params TEntity[] entities)
        {
            if (!IsDeletable())
            {
                //delete physically
                DeletePermanently(entities);
                return;
            }

            //delete logically
            DeleteLogically(entities);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            if (!IsDeletable())
            {
                //delete physically
                DeletePermanently(entities);
                return;
            }

            //delete logically
            DeleteLogically(entities);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> wherePredicate = null)
        {
            if (wherePredicate != null)
                return await Query.FirstOrDefaultAsync(wherePredicate);

            return await Query.FirstOrDefaultAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> wherePredicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var query = includes.Aggregate(Query, (current, include) => current.Include(include));

            return await query.FirstOrDefaultAsync(wherePredicate);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> wherePredicate = null)
        {
            if (wherePredicate != null)
                return await Query.SingleOrDefaultAsync(wherePredicate);

            return await Query.SingleOrDefaultAsync();
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = includes.Aggregate(Query, (current, include) => current.Include(include));
            return await query.SingleOrDefaultAsync(wherePredicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> wherePredicate)
        {
            return await Query.AnyAsync(wherePredicate);
        }

        #region Private implementation

        private IQueryable<TEntity> FilterQuery(Expression<Func<TEntity, bool>> wherePredicate)
        {
            return wherePredicate == null ? Query : Query.Where(wherePredicate);
        }

        private void DeleteLogically(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.Deleted = true;
            }
        }

        private void DeletePermanently(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        private bool IsDeletable()
        {
            return typeof(IBaseEntity).IsAssignableFrom(typeof(TEntity));
        }

        #endregion
    }
}
