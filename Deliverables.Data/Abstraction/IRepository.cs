using Deliverables.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SourceScrub.DataAccess.Abstraction
{
    public interface IRepository<TEntity> where TEntity : IBaseEntity 
    {
        Task<TEntity> GetByIdAsync(int id);

        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> wherePredicate = null,
                                         Expression<Func<TEntity, int>> orderBy = null,
                                         params Expression<Func<TEntity, object>>[] includes);


        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> wherePredicate,
                                         params Expression<Func<TEntity, object>>[] includes);

        Task<List<TResult>> SelectAsync<TResult>(Expression<Func<TEntity, TResult>> selectExpression,
            Expression<Func<TEntity, bool>> wherePredicate = null,
            Expression<Func<TEntity, int>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includes);

        void Add(params TEntity[] entities);

        void MarkAsModified(params TEntity[] entities);

        bool IsAdded(TEntity entity);

        void AttachIfDetached(params TEntity[] entities);

        void AddOrUpdate(Expression<Func<TEntity, object>> identifierProperty, params TEntity[] entities);

        void Delete(params TEntity[] entities);

        void Delete(IEnumerable<TEntity> entities);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> wherePredicate = null);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> wherePredicate,
                                         params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> wherePredicate = null);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> wherePredicate,
                                         params Expression<Func<TEntity, object>>[] includes);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> wherePredicate);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression);
    }
}
