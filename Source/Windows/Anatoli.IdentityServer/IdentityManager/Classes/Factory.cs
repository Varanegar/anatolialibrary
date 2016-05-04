using IdentityServer3.Core.Services;
using IdentityServer3.EntityFramework;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services.Default;

namespace Anatoli.IdentityServer.Classes
{
    public class Factory
    {
        public static IdentityServerServiceFactory Config(string connString)
        {
            var factory = new IdentityServerServiceFactory();

            var efConfig = new EntityFrameworkServiceOptions
            {
                ConnectionString = connString,
            };

            factory.RegisterConfigurationServices(efConfig);
            factory.RegisterOperationalServices(efConfig);
           
            factory.CorsPolicyService = new Registration<ICorsPolicyService>(new DefaultCorsPolicyService { AllowAll = true });

            return factory;
        }
    }
}