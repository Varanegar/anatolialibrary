using Anatoli.IdentityServer.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Anatoli.IdentityServer.Classes
{
    public class UserStore : UserStore<User, Role, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public UserStore(Context ctx): base (ctx)
        {
        }
    }
}