using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Anatoli.IdentityServer.Entities;

namespace Anatoli.IdentityServer.Classes
{
    public class ClaimsFactory : ClaimsIdentityFactory<User, string>
    {
        public ClaimsFactory()
        {
            UserIdClaimType = IdentityServer3.Core.Constants.ClaimTypes.Subject;
            UserNameClaimType = IdentityServer3.Core.Constants.ClaimTypes.PreferredUserName;
            RoleClaimType = IdentityServer3.Core.Constants.ClaimTypes.Role;
        }

        public override async Task<ClaimsIdentity> CreateAsync(UserManager<User, string> manager,
                                                               User user, string authenticationType)
        {
            var ci = await base.CreateAsync(manager, user, authenticationType);

            if (!string.IsNullOrWhiteSpace(user.FirstName))
                ci.AddClaim(new Claim("given_name", user.FirstName));

            if (!string.IsNullOrWhiteSpace(user.LastName))
                ci.AddClaim(new Claim("family_name", user.LastName));

            return ci;
        }
    }
}