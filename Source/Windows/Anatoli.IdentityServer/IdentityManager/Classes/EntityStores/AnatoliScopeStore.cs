using IdentityServer3.EntityFramework;

namespace Anatoli.IdentityServer.Classes
{
    public class AnatoliScopeStore : ScopeStore
    {
        public AnatoliScopeStore(IScopeConfigurationDbContext ctx): base (ctx)
        {
        }
    }
}