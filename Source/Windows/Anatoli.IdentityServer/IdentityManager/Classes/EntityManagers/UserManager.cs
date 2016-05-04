using Anatoli.IdentityServer.Entities;
using Microsoft.AspNet.Identity;

namespace Anatoli.IdentityServer.Classes
{
    public class UserManager : UserManager<User, string>
    {
        public UserManager(UserStore store): base (store)
        {
            ClaimsIdentityFactory = new ClaimsFactory();
        }
    }
}