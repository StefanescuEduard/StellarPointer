using CouchDB.Driver.Types;
using System.Threading.Tasks;

namespace StellarPointer.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity, string>
        where TEntity : CouchDocument
    {
        protected StellarPointerContext StellarPointerContext { get; }

        public Repository(StellarPointerContext stellarPointerContext)
        {
            StellarPointerContext = stellarPointerContext;
        }

        public Task<TEntity> GetAsync(string id)
        {
            return StellarPointerContext.GetDatabase<TEntity>().FindAsync(id);
        }

        public Task AddAsync(TEntity entity)
        {
            return StellarPointerContext.GetDatabase<TEntity>().AddOrUpdateAsync(entity);
        }
    }
}