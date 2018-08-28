using Bookist.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookist.Core
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
           : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Book>(b => { });

            builder.Entity<BookTag>(b =>
            {
                b.HasKey(x => new { x.BookId, x.TagId });
            });

            builder.Entity<Link>(b => { });

            builder.Entity<Tag>(b => { });

            builder.Entity<User>(b => { });
        }
    }
}
