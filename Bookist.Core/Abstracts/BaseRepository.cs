using Anet;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookist.Core
{
    public abstract class BaseRepository<TEntity> where TEntity : class, IEntity
    {
        public BaseRepository(DefaultDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        protected DefaultDbContext Context { get; }

        protected DbSet<TEntity> DbSet { get; }

        protected string LikeString(string keyword)
        {
            return $"%{keyword.Trim().Replace(' ', '%')}%";
        }

        public Task<TEntity> GetByIdAsync(long id, Func<IQueryable<TEntity>, IQueryable<TEntity>> setup = null)
        {
            var query = DbSet.AsQueryable();
            if (setup != null)
            {
                query = setup(query);
            }
            return query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<TEntity>> PageAsync(IQueryable<TEntity> query, PageQuery pageQuery)
        {
            return query.Page(pageQuery.Page, pageQuery.Size).ToListAsync();
        }
    }
}
