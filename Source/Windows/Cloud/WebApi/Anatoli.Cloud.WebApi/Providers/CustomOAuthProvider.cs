using Anatoli.Cloud.WebApi.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Anatoli.DataAccess.Models.Identity;
using Microsoft.AspNet.Identity;

namespace Anatoli.Cloud.WebApi.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            try
            {
                var allowedOrigin = "*";

                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

                var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

                var user = await userManager.FindAsync(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }

                if (!user.EmailConfirmed)
                {
                    context.SetError("invalid_grant", "User did not confirm email.");
                    return;
                }

                ClaimsIdentity oAuthIdentity = await GenerateUserIdentityAsync(user, userManager, "JWT");
                oAuthIdentity.AddClaims(ExtendedClaimsProvider.GetClaims(user));
                oAuthIdentity.AddClaims(RolesFromClaims.CreateRolesBasedOnClaims(oAuthIdentity));

                var ticket = new AuthenticationTicket(oAuthIdentity, null);

                context.Validated(ticket);
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(User user, UserManager<User> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(user, authenticationType);
            // Add custom user claims here

            return userIdentity;
        }
    }
}