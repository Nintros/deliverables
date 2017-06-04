using Deliverables.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Deliverables.Business.Abstraction
{
    public interface IBaseService<TEntity> where TEntity : class, IBaseEntity
    {
        Task<int> AddAsync(TEntity entity);

        Task<int> UpdateAsync(TEntity entity);

        Task<TEntity> GetAsync();

        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> wherePredicate,
                                         params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includes);

        Task<List<TEntity>> GetAllAsync();

        Task<List<TEntity>> GetAllWithIncludesAsync(params Expression<Func<TEntity, object>>[] includes);

        Task<List<TEntity>> GetManyWithIncludesAsync(Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includes);

        Task DeleteAsync(params TEntity[] entity);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> wherePredicate);

        Task<int> GetCount(Expression<Func<TEntity, bool>> wherePredicate);
    }
}
