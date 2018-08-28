using Bookist.Core.Entities;

namespace Bookist.Core.Services
{
    public class UserService : BaseService<User>
    {
        public UserService(DefaultDbContext context) : base(context)
        {
        }
    }
}
