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
using log4net;

namespace Anatoli.Cloud.WebApi.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
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
                if (context.Scope.Count < 1)
                {
                    context.SetError("خطا در ورود به سیستم", "نرم افزار کاربر مشخص نشده است");
                    return;
                }

                var additionalData = context.Scope[0].Split(',');
                var appOwner = Guid.Empty; var dataOwner = Guid.Empty;

                if (!Guid.TryParse(additionalData[0].Trim(), out appOwner))
                {
                    context.SetError("خطا در ورود به سیستم", "نرم افزار کاربر مشخص نشده است");
                    return;
                }

                if (!Guid.TryParse(additionalData[1].Trim(), out dataOwner))
                {
                    context.SetError("خطا در ورود به سیستم", "شرکت کاربر مشخص نشده است");
                    return;
                }
                var user = await userManager.FindByNameOrEmailOrPhoneAsync(context.UserName, context.Password, appOwner, dataOwner);
                //var user = await userManager.FindAsync(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("خطا در ورود به سیستم", "نام کاربری یا کلمه عبور اشتباه است");
                    return;
                }

                if (!user.EmailConfirmed)
                {
                    context.SetError("خطا در ورود به سیستم", "تایید نام کاربری دریافت نشده است");
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
                logger.ErrorFormat("GrantResourceOwnerCredentials", ex);

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