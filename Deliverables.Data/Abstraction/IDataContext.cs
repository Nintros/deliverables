using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace Deliverables.Data.Abstraction
{
    public interface IDataContext
    {
        Task<int> SaveChangesAsync();

        void Dispose();

        DbSet<T> Set<T>() where T : class;
            
        DbContextTransaction BeginTransation();

        DbEntityEntry Entry(object entity);
    }
}
