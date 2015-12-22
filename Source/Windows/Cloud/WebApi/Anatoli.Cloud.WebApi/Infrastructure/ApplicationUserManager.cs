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

namespace Anatoli.Cloud.WebApi.Infrastructure
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store)
            : base(store)
        {
        }

         public async Task<User> FindByNameOrEmailOrPhoneAsync(string usernameOrEmailOrPhone, string password)
        {
            var username = usernameOrEmailOrPhone;
            if (usernameOrEmailOrPhone.Contains("@"))
            {
                var userForEmail = await FindByEmailAsync(usernameOrEmailOrPhone);
                if (userForEmail != null)
                {
                    username = userForEmail.UserName;
                }
            }
            else if (usernameOrEmailOrPhone.Contains("09"))
            {
                var context = AnatoliDbContext.Create();
                var userForPhone = await new AnatoliUserStore(context).FindByPhoneAsync(usernameOrEmailOrPhone);
                if (userForPhone != null)
                {
                    username = userForPhone.UserName;
                }
            }
            return await FindAsync(username, password);
        }
    
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var appDbContext = context.Get<AnatoliDbContext>();
            var appUserManager = new ApplicationUserManager(new AnatoliUserStore(appDbContext));

            // Configure validation logic for usernames
            appUserManager.UserValidator = new UserValidator<User>(appUserManager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            appUserManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true,
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