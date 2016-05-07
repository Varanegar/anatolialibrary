using Microsoft.AspNet.Identity;
using Anatoli.IdentityServer.Entities;

namespace Anatoli.IdentityServer.Classes
{
    public class RoleManager : RoleManager<Role>
    {
        public RoleManager(RoleStore store): base (store)
        {
        }
    }
}