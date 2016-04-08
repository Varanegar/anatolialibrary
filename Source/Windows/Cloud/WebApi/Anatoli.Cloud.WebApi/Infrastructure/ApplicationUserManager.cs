using System;
using System.Linq;
using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.Owin;
using Anatoli.DataAccess.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Anatoli.DataAccess;
using Anatoli.DataAccess.Repositories;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Anatoli.Business.Domain;

namespace Anatoli.Cloud.WebApi.Infrastructure
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store)
            : base(store)
        {
        }

         public async Task<User> FindByNameOrEmailOrPhoneAsync(string usernameOrEmailOrPhone, string password, Guid applicationOwner, Guid dataOwnerKey)
        {
            var userDomain = new UserDomain(applicationOwner, dataOwnerKey);

            var user = await userDomain.UserExists(usernameOrEmailOrPhone);
            if (user != null)
                return await FindAsync(user.UserName, password);
            else
                return null;

        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            
            var appDbContext = context.Get<AnatoliDbContext>();
            var appUserManager = new ApplicationUserManager(new AnatoliUserStore(appDbContext));

            // Configure validation logic for usernames
            appUserManager.UserValidator = new UserValidator<User>(appUserManager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = false
                
            };

            // Configure validation logic for passwords
            appUserManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 4,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            
            appUserManager.EmailService = new Anatoli.Cloud.WebApi.Services.EmailService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                appUserManager.UserTokenProvider = new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    //Code for email confirmation and reset password life time
                    TokenLifespan = TimeSpan.FromHours(6)
                };
            }
           
            return appUserManager;
        }
    }
}