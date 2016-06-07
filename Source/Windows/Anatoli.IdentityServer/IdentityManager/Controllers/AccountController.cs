using System;
using System.Linq;
using System.Web.Http;
using IdentityModel.Client;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using Microsoft.Owin.Security.Jwt;
using Anatoli.IdentityServer.Classes;

namespace Anatoli.IdentityServer.Controllers
{
    [Authorize, RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        UserManager _userManager;
        public UserManager UserManager
        {
            get
            {
                if (_userManager == null)
                    _userManager = new UserManager(new UserStore(new Context()));

                return _userManager;
            }
        }

        public class RequestModel
        {
            public TokenResponse token { get; set; }

            public string username { get; set; }
            public string password { get; set; }
            public string fullname { get; set; }
            public string email { get; set; }
            public bool emailConfirmed { get; set; }
            public string phonenumber { get; set; }
            public bool phonenumberConfirmed { get; set; }
            public string roles { get; set; }
        }

        [Authorize, HttpPost, Route("ValidateToken")]
        public async Task<IHttpActionResult> ValidateToken([FromBody]RequestModel model)
        {
            var claims = await ValidateIdentityTokenAsync(model.token);

            var claimsId = new ClaimsIdentity(claims, "Cookies");

            claimsId.AddClaim(new Claim("access_token", model.token.AccessToken));

            claimsId.AddClaim(new Claim("expires_at", DateTime.Now.AddSeconds(model.token.ExpiresIn).ToLocalTime().ToString()));

            claimsId.AddClaim(new Claim("refresh_token", model.token.RefreshToken));

            return Ok(new { claimsId });
        }

        [AllowAnonymous, HttpPost, Route("signupUserofCloud")]
        public async Task<IHttpActionResult> SignupUserofCloud([FromBody]RequestModel model)
        {
            try
            {
                if (UserManager.FindByNameOrEmailOrPhoneAsync(model.username) != null ||
                    UserManager.FindByNameOrEmailOrPhoneAsync(model.email) != null ||
                    UserManager.FindByNameOrEmailOrPhoneAsync(model.phonenumber) != null)
                    return Ok(new { });

                var user = new Entities.User
                {
                    Email = model.email,
                    EmailConfirmed = model.emailConfirmed,
                    FirstName = model.fullname,
                    LastName = model.fullname,
                    UserName = model.username,
                    PhoneNumber = model.phonenumber,
                    PhoneNumberConfirmed = model.phonenumberConfirmed,
                };

                await UserManager.CreateAsync(user, model.password);

                await UserManager.AddToRolesAsync(user.Id, model.roles.Split(','));

                foreach (var role in model.roles.Split(','))
                    await UserManager.AddClaimAsync(user.Id, new Claim(IdentityServer3.Core.Constants.ClaimTypes.Role, role));

                if (!string.IsNullOrEmpty(user.FirstName))
                    await UserManager.AddClaimAsync(user.Id, new Claim(IdentityServer3.Core.Constants.ClaimTypes.GivenName, user.FirstName));

                if (!string.IsNullOrEmpty(user.LastName))
                    await UserManager.AddClaimAsync(user.Id, new Claim(IdentityServer3.Core.Constants.ClaimTypes.FamilyName, user.LastName));

                if (!string.IsNullOrEmpty(user.Id))
                    await UserManager.AddClaimAsync(user.Id, new Claim(IdentityServer3.Core.Constants.ClaimTypes.Id, user.Id));

                if (!string.IsNullOrEmpty(user.Email))
                {
                    await UserManager.AddClaimAsync(user.Id, new Claim(IdentityServer3.Core.Constants.ClaimTypes.Email, user.Email));
                    await UserManager.AddClaimAsync(user.Id, new Claim(IdentityServer3.Core.Constants.ClaimTypes.EmailVerified, user.EmailConfirmed.ToString()));
                }

                if (!string.IsNullOrEmpty(user.PhoneNumber))
                {
                    await UserManager.AddClaimAsync(user.Id, new Claim(IdentityServer3.Core.Constants.ClaimTypes.PhoneNumber, user.PhoneNumber));
                    await UserManager.AddClaimAsync(user.Id, new Claim(IdentityServer3.Core.Constants.ClaimTypes.PhoneNumberVerified, user.PhoneNumberConfirmed.ToString()));
                }

                return Ok(new { });
            }
            catch
            {
                return BadRequest();
            }
        }

        private async Task<List<Claim>> ValidateIdentityTokenAsync(TokenResponse token)
        {
            return await Task.Run(() =>
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var validationParameters = new TokenValidationParameters
                {
                    ValidAudience = "https://localhost:44300/core/resources",
                    ValidIssuer = "https://localhost:44300/core",
                    NameClaimType = "name",
                    RoleClaimType = "role",

                    IssuerSigningTokens = new X509CertificateSecurityTokenProvider("https://localhost:44300/core", Certificate.Get()).SecurityTokens
                };

                SecurityToken t;
                var id = tokenHandler.ValidateToken(token.AccessToken, validationParameters, out t);

                var claimList = id.Claims.ToList();

                //claimList.Add(new Claim(ClaimTypes.Name, id.Identity.Name));

                return claimList;
            });
        }
    }
}
