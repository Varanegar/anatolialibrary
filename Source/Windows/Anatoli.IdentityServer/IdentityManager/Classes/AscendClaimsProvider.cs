using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Validation;
using IdentityServer3.Core.Services.Default;

namespace Anatoli.IdentityServer.Classes
{
    public class AscendClaimsProvider : DefaultClaimsProvider, IClaimsProvider
    {
        public AscendClaimsProvider(IUserService users) : base(users)
        {
        }

        public override async Task<IEnumerable<Claim>> GetAccessTokenClaimsAsync(
                                                        ClaimsPrincipal subject,
                                                        Client client,
                                                        IEnumerable<Scope> scopes,
                                                        ValidatedRequest request)
        {
            var claimsTask = await base.GetAccessTokenClaimsAsync(subject, client, scopes, request);

            var profileDataRequestContext = new ProfileDataRequestContext(subject, client, "", new string[] { "role","Name","GivenName","Email" });

            await _users.GetProfileDataAsync(profileDataRequestContext);

            var claims = claimsTask.Union(profileDataRequestContext.IssuedClaims);

            return claims;
        }
    }
}