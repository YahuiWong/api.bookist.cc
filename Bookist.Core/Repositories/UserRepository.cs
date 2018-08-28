using Bookist.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bookist.Core.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(DefaultDbContext context) : base(context)
        {
        }

        public Task<User> GetByUserName(string userName)
        {
            return DbSet.FirstOrDefaultAsync(x => x.UserName == userName);
        }
    }
}
