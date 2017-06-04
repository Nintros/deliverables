using Deliverables.Business.Abstraction;
using Deliverables.Data.Abstraction;
using Deliverables.Data.Base;
using SourceScrub.DataAccess.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Deliverables.Business.Implementation
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, IBaseEntity
    {
        #region Private fields

        protected readonly IDataContext _context;
        protected readonly IRepository<TEntity> Repository;

        #endregion

        #region .ctor

        public BaseService(IDataContext context, IRepository<TEntity> repository)
        {
            _context = context;
            Repository = repository;
        }

        #endregion

        #region IDomainEntityService implementation

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            Repository.Add(entity);

            return await _context.SaveChangesAsync();
        }

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            Repository.MarkAsModified(entity);

            return await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(params TEntity[] entity)
        {
            if (entity == null)
            {
                throw new InvalidOperationException(string.Format("Can\'t delete not existing entity ({0}).", typeof(TEntity)));
            }

            Repository.Delete(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> GetAsync()
        {
            return await Repository.SingleOrDefaultAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> wherePredicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            return await Repository.SingleOrDefaultAsync(wherePredicate, includes);
        }

        public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> wherePredicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            return await Repository.FirstOrDefaultAsync(wherePredicate, includes);
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await Repository.GetListAsync(null, includes: null);
        }

        public async Task<List<TEntity>> GetAllWithIncludesAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            return await Repository.GetListAsync(null, includes);
        }

        public async Task<List<TEntity>> GetManyWithIncludesAsync(Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return await Repository.GetListAsync(wherePredicate, includes);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> wherePredicate)
        {
            return await Repository.AnyAsync(wherePredicate);
        }

        public async Task<int> GetCount(Expression<Func<TEntity, bool>> wherePredicate)
        {
            return await Repository.CountAsync(wherePredicate);
        }

        #endregion
    }
}
