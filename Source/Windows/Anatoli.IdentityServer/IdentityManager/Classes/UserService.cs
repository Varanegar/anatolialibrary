
using Anatoli.IdentityServer.Entities;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;

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
            factory.Register(new Registration<Context>(resolver => new Context(connString)));
        }
    }

    public class UserService : AspNetIdentityUserService<User, string>
    {
        public UserService(UserManager userMgr)
            : base(userMgr)
        {
        }
    }
}