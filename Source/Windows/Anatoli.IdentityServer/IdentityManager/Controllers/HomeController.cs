using System.Web.Mvc;

namespace Anatoli.IdentityServer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            //var ctx = new Context();

            //var scopeModel = new Scope
            //{
            //    Name = "webapis",
            //    DisplayName = "Wep Api",
            //    Type = ScopeType.Resource,
            //    Emphasize = false,
            //};

            //ctx.Scopes.Add(scopeModel.ToEntity());

            //var clientModel = new Client
            //{
            //    ClientName = "Anatoli Cloud Client",
            //    Enabled = true,
            //    ClientId = "anatoliCloudClient",
            //    ClientSecrets = new List<Secret>
            //     {
            //         new Secret("anatoliCloudClient".Sha256()) {  }
            //     },

            //    Flow = Flows.ResourceOwner,

            //    AllowedScopes = new List<string>
            //    {
            //        Constants.StandardScopes.OpenId,
            //        Constants.StandardScopes.Profile,
            //        Constants.StandardScopes.Email,
            //        Constants.StandardScopes.Roles,
            //        Constants.StandardScopes.OfflineAccess,
            //        "read",
            //        "write",
            //        "webapis"
            //    },

            //    AccessTokenType = AccessTokenType.Jwt,
            //    AccessTokenLifetime = 3600,
            //    AbsoluteRefreshTokenLifetime = 86400,
            //    SlidingRefreshTokenLifetime = 43200,

            //    RefreshTokenUsage = TokenUsage.OneTimeOnly,
            //    RefreshTokenExpiration = TokenExpiration.Sliding
            //};

            //ctx.Clients.Add(clientModel.ToEntity());

            //ctx.SaveChanges();

            return View();
        }
    }
}
