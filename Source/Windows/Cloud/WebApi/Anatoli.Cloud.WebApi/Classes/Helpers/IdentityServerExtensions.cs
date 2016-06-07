using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace Anatoli.Cloud.WebApi.Classes.Helpers
{
    public static class IdentityServerExtensions
    {
        public static string GetAnatoliUserId(this IPrincipal User)
        {
            if (User == null)
                return string.Empty;

            if (!string.IsNullOrEmpty(User.Identity.GetUserId()))
                return User.Identity.GetUserId();

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