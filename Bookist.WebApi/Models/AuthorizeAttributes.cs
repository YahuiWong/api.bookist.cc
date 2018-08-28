using Microsoft.AspNetCore.Authorization;

namespace Bookist.WebApi
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        public AdminAuthorizeAttribute() : base()
        {
            Policy = "Admin";
        }
    }

    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        public UserAuthorizeAttribute() : base()
        {
            Policy = "User";
        }
    }
}
