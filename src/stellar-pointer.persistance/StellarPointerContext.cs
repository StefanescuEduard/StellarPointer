using CouchDB.Driver;
using CouchDB.Driver.Options;
using CouchDB.Driver.Types;
using System.Linq;

namespace StellarPointer.Persistence
{
    public class StellarPointerContext : CouchContext
    {
        public CouchDatabase<User> Users { get; set; }

        public StellarPointerContext(CouchOptions<StellarPointerContext> couchOptions) : base(couchOptions)
        {
        }

        public CouchDatabase<TSource> GetDatabase<TSource>()
            where TSource : CouchDocument
        {
            return GetType()
                .GetProperties()
                .Single(p => p.PropertyType == typeof(CouchDatabase<TSource>))
                .GetValue(this, null) as CouchDatabase<TSource>;
        }
    }
}
