using System.Threading.Tasks;

namespace StellarPointer.Persistence
{
    public interface IRepository<TEntity, in TId>
    {
        Task<TEntity> GetAsync(TId id);
        Task AddAsync(TEntity entity);
    }
}
