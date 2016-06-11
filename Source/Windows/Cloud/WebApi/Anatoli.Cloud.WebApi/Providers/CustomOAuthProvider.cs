using Anatoli.Cloud.WebApi.Infrastructure;
using Anatoli.DataAccess.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using NLog;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Anatoli.Cloud.WebApi.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        protected static readonly Logger logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());
        
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

                //var user = userManager.FindByName(context.UserName);
                var user = userManager.FindByNameOrEmailOrPhoneAsync(context.UserName, context.Password, appOwner, dataOwner).Result;
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
                logger.Error("GrantResourceOwnerCredentials", ex);

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