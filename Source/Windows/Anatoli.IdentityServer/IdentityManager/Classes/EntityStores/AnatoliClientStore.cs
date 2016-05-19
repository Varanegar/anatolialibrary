using IdentityServer3.EntityFramework;

namespace Anatoli.IdentityServer.Classes
{
    public class AnatoliClientStore : ClientStore
    {
        public AnatoliClientStore(IClientConfigurationDbContext ctx): base (ctx)
        {
        }
    }
}