using Anet;
using System.Threading.Tasks;

namespace Bookist.Core
{
    public abstract class BaseService<TEntity> where TEntity : class, IEntity
    {
        protected DefaultDbContext Context { get; }

        public BaseService(DefaultDbContext context)
        {
            Context = context;
        }
    }
}
