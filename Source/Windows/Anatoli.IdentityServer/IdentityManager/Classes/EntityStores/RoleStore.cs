using Anatoli.IdentityServer.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Anatoli.IdentityServer.Classes
{
    public class RoleStore : RoleStore<Role>
    {
        public RoleStore(Context ctx): base (ctx)
        {
        }
    }
}