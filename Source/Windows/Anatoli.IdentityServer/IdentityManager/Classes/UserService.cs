using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using Anatoli.IdentityServer.Entities;
using IdentityServer3.Core.Configuration;

namespace Anatoli.IdentityServer.Classes
{
    public static class UserServiceExtensions
    {
        public static void ConfigureUserService(this IdentityServerServiceFactory factory, string connString)
        {
            factory.UserService = new Registration<IUserService, UserService>();
            factory.Register(new Registration<UserManager>());
            factory.Register(new Registration<UserStore>());
            factory.Register(new Registration<AnatoliScopeStore>());
            factory.Register(new Registration<AnatoliClientStore>());

            factory.Register(new Registration<AscendClaimsProvider>());

            factory.Register(new Registration<Context>(resolver => new Context(connString)));
        }
    }

    public class UserService : AspNetIdentityUserService<User, string>
    {
        public UserService(UserManager userMgr)
            : base(userMgr)
        {
        }

        //Microsoft.Owin.OwinContext ctx;
        //public UserService(OwinEnvironmentService owinEnv)
        //{
        //    ctx = new Microsoft.Owin.OwinContext(owinEnv.Environment);
        //}
        public override Task PreAuthenticateAsync(PreAuthenticationContext context)
        {
            //var id = ctx.Request.Query.Get("signin");
            //context.AuthenticateResult = new AuthenticateResult("~/custom/login?id=" + id, (System.Collections.Generic.IEnumerable<Claim>)null);
            //return Task.FromResult(0);
            
            return base.PreAuthenticateAsync(context);
        }
    }
}