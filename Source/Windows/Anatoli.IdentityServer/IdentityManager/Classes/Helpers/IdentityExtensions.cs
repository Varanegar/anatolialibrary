using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Anatoli.IdentityServer.Classes.Helpers
{
    public static class IdentityExtensions
    {
        public static string GetClaimUserId(this IPrincipal User)
        {
            var principal = User as ClaimsPrincipal;
            return principal.Identities.First().Claims.Where(c => c.Type == "sub").Select(s => s.Value).FirstOrDefault();
        }

        public static string GetClaim(this IPrincipal User, string claim)
        {
            var principal = User as ClaimsPrincipal;
            return principal.Identities.First().Claims.Where(c => c.Type == claim).Select(s => s.Value).FirstOrDefault();
        }
    }
}