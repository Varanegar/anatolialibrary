using Anatoli.Cloud.WebApi.Infrastructure;
using Anatoli.Cloud.WebApi.Providers;
using Anatoli.DataAccess;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.DataAccess.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            log4net.Config.XmlConfigurator.Configure();

            HttpConfiguration httpConfig = new HttpConfiguration();

            ConfigureOAuthTokenGeneration(app);

            ConfigureOAuthTokenConsumption(app);

            ConfigureWebApi(httpConfig);

            //ConfigureUserinfo();

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(httpConfig);

            httpConfig.EnsureInitialized();

            

        }

        private void ConfigureUserinfo()
        {
            
            //AnatoliDbContext context = new AnatoliDbContext();

            //var manager = new ApplicationUserManager(new AnatoliUserStore(context));
            //if (manager.Users.Count() > 0) return;
            //var roleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(context));


            //var id = Guid.Parse("02D3C1AA-6149-4810-9F83-DF3928BFDF16");
            //var user = new User()
            //{
            //    Id = id.ToString(),
            //    UserName = "anatoli",
            //    Email = "anatoli@varanegar.com",
            //    EmailConfirmed = true,
            //    PhoneNumberConfirmed = true,
            //    CreatedDate = DateTime.Now,
            //    PhoneNumber = "87135000",
            //    PrivateLabelOwner = new Principal { Id = Guid.Parse("02D3C1AA-6149-4810-9F83-DF3928BFDF16") },
            //    Principal = new Principal { Id = id, Title = "anatoli@varanegar.com" }
            //};

            
            //var result = manager.CreateAsync(user, "anatoli@vn@87134").Result;

            //if (roleManager.Roles.Count() == 0)
            //{
            //    result = roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" }).Result;
            //    result = roleManager.CreateAsync(new IdentityRole { Name = "Admin" }).Result;
            //    result = roleManager.CreateAsync(new IdentityRole { Name = "AuthorizedApp" }).Result;
            //    result = roleManager.CreateAsync(new IdentityRole { Name = "User" }).Result;
            //}

            //var adminUser = manager.FindByNameAsync("anatoli").Result;

            //result = manager.AddToRolesAsync(adminUser.Id, new string[] { "SuperAdmin", "Admin", "AuthorizedApp", "User" }).Result;

            //id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C");
            //var user2 = new User()
            //{
            //    Id = id.ToString(),
            //    UserName = "petropay",
            //    Email = "petropay@varanegar.com",
            //    EmailConfirmed = true,
            //    PhoneNumberConfirmed = true,
            //    PhoneNumber = "02100000000",
            //    CreatedDate = DateTime.Now,
            //    PrivateLabelOwner = new Principal { Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C") },
            //    Principal = new Principal { Id = id, Title = "petropay@varanegar.com" }
            //};

            //result = manager.CreateAsync(user2, "petropay@webapp").Result;

            //var userInfo2 = manager.FindByNameAsync("petropay").Result;

            //result = manager.AddToRolesAsync(userInfo2.Id, new string[] { "AuthorizedApp", "User" }).Result;

            //id = Guid.Parse("0DAB1636-AE22-4ABE-A18D-6EC7B8E9C544");
            //var user3 = new User()
            //{
            //    Id = id.ToString(),
            //    UserName = "AnatoliMobileApp",
            //    Email = "anatoli-mobile-app@varanegar.com",
            //    EmailConfirmed = true,
            //    PhoneNumberConfirmed = true,
            //    PhoneNumber = "09125793221",
            //    CreatedDate = DateTime.Now,
            //    PrivateLabelOwner = new Principal { Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C") },
            //    Principal = new Principal { Id = id, Title = "anatoli-mobile-app@varanegar.com" }
            //};

            //result = manager.CreateAsync(user3, "Anatoli@App@Vn").Result;

            //var userInfo3 = manager.FindByNameAsync("AnatoliMobileApp").Result;

            //result = manager.AddToRolesAsync(userInfo3.Id, new string[] { "AuthorizedApp", "User" }).Result;
        }

        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(AnatoliDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat(ConfigurationManager.AppSettings["server:URI"])
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
        }

        private void ConfigureOAuthTokenConsumption(IAppBuilder app) {

            var issuer = ConfigurationManager.AppSettings["server:URI"];
            string audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
            byte[] audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["as:AudienceSecret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audienceId },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
                    }
                });
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}