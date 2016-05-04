using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Security.Claims;
using System.Linq;
using IdentityServer3.Core;
using IdentityServer3.Core.Configuration;
using IdentityModel.Client;
using System.Threading.Tasks;
using Serilog;
using IdentityManager.Logging;
using Anatoli.IdentityServer.Classes;

[assembly: OwinStartup(typeof(Anatoli.IdentityServer.Startup))]
namespace Anatoli.IdentityServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            IdentityManager.Core.Logging.LogProvider.SetCurrentLogProvider(new DiagnosticsTraceLogProvider());
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.Trace()
               .CreateLogger();

            Log.Logger.Debug("Getting claims for identity token");


            app.Map("/core", core =>
            {
                var idSvrFactory = Factory.Config("IdSvr3Config");
                idSvrFactory.ConfigureUserService("IdSvr3Config");

                var options = new IdentityServerOptions
                {
                    SiteName = "Anatoli - IdentityServer",
                    SigningCertificate = Certificate.Get(),
                    Factory = idSvrFactory,
                    AuthenticationOptions = new IdentityServer3.Core.Configuration.AuthenticationOptions
                    {
                        //IdentityProviders = ConfigureAdditionalIdentityProviders,                        
                    }
                };

                core.UseIdentityServer(options);
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = "https://localhost:44300/core",

                ClientId = "mvc",
                Scope = "openid profile email read write",
                ResponseType = "id_token token",
                RedirectUri = "https://localhost:44300/",

                SignInAsAuthenticationType = "Cookies",
                UseTokenLifetime = false,

                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = async n =>
                    {
                        var nid = new ClaimsIdentity(
                            n.AuthenticationTicket.Identity.AuthenticationType,
                            Constants.ClaimTypes.GivenName,
                            Constants.ClaimTypes.Role);

                        // get userinfo data
                        var userInfoClient = new UserInfoClient(
                            new Uri(n.Options.Authority + "/connect/userinfo"),
                            n.ProtocolMessage.AccessToken);

                        var userInfo = await userInfoClient.GetAsync();
                        userInfo.Claims.ToList().ForEach(ui => nid.AddClaim(new Claim(ui.Item1, ui.Item2)));

                        // keep the id_token for logout
                        nid.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));

                        // add access token for sample API
                        nid.AddClaim(new Claim("access_token", n.ProtocolMessage.AccessToken));

                        // keep track of access token expiration
                        nid.AddClaim(new Claim("expires_at", DateTimeOffset.Now.AddSeconds(int.Parse(n.ProtocolMessage.ExpiresIn)).ToString()));

                        // add some other app specific claim
                        nid.AddClaim(new Claim("app_specific", "some data"));

                        n.AuthenticationTicket = new AuthenticationTicket(
                            nid,
                            n.AuthenticationTicket.Properties);
                    },

                    RedirectToIdentityProvider = n =>
                    {
                        if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                        {
                            var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");

                            if (idTokenHint != null)
                            {
                                n.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                            }
                        }

                        return Task.FromResult(0);
                    }
                }
            });


        }

        //static void ConfigureAdditionalIdentityProviders(IAppBuilder app, string signInAsType)
        //{
        //    var google = new GoogleOAuth2AuthenticationOptions
        //    {
        //        AuthenticationType = "Google",
        //        SignInAsAuthenticationType = signInAsType,
        //        ClientId = "767400843187-8boio83mb57ruogr9af9ut09fkg56b27.apps.googleusercontent.com",
        //        ClientSecret = "5fWcBT0udKY7_b6E3gEiJlze"
        //    };
        //    app.UseGoogleAuthentication(google);

        //    var fb = new FacebookAuthenticationOptions
        //    {
        //        AuthenticationType = "Facebook",
        //        SignInAsAuthenticationType = signInAsType,
        //        AppId = "676607329068058",
        //        AppSecret = "9d6ab75f921942e61fb43a9b1fc25c63"
        //    };
        //    app.UseFacebookAuthentication(fb);

        //    var twitter = new TwitterAuthenticationOptions
        //    {
        //        AuthenticationType = "Twitter",
        //        SignInAsAuthenticationType = signInAsType,
        //        ConsumerKey = "N8r8w7PIepwtZZwtH066kMlmq",
        //        ConsumerSecret = "df15L2x6kNI50E4PYcHS0ImBQlcGIt6huET8gQN41VFpUCwNjM"
        //    };
        //    app.UseTwitterAuthentication(twitter);
        //}
    }
}
