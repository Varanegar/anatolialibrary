using IdentityServer3.Core;
using System.Collections.Generic;
using Anatoli.IdentityServer.Classes;
using IdentityServer3.EntityFramework.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnatoliIdentityUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //var ctx = new Context();
            
            //var scopeModel = new Scope
            //{
            //    Name = "webapis",
            //    DisplayName = "Wep Api",
            //    Type = (int)IdentityServer3.Core.Models.ScopeType.Resource,
            //    Emphasize = false,
            //};

            //ctx.Scopes.Add(scopeModel);
            //ctx.SaveChanges();

           //ctx.Scopes.
           // var clientModel = new Client
           // {
           //     ClientName = "Anatoli Cloud Client",
           //     Enabled = true,
           //     ClientId = "anatoliCloudClient",
           //     ClientSecrets = new List<IdentityServer3.Core.Models.Secret>
           //     {
           //         new IdentityServer3.Core.Models.Secret("anatoliCloudClient".Sha256())
           //     },

           //     Flow = IdentityServer3.Core.Models.Flows.ResourceOwner,

           //     //AllowedScopes = new List<string>
           //     //{
           //     //    Constants.StandardScopes.OpenId,
           //     //    Constants.StandardScopes.Profile,
           //     //    Constants.StandardScopes.Email,
           //     //    Constants.StandardScopes.Roles,
           //     //    Constants.StandardScopes.OfflineAccess,
           //     //    "read",
           //     //    "write",
           //     //    "webapis"
           //     //},

           //     AccessTokenType = IdentityServer3.Core.Models.AccessTokenType.Jwt,
           //     AccessTokenLifetime = 3600,
           //     AbsoluteRefreshTokenLifetime = 86400,
           //     SlidingRefreshTokenLifetime = 43200,

           //     RefreshTokenUsage = IdentityServer3.Core.Models.TokenUsage.OneTimeOnly,
           //     RefreshTokenExpiration = IdentityServer3.Core.Models.TokenExpiration.Sliding
           // };
        }
    }
}
