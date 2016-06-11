using System;
using System.Web.Http;
using IdentityModel.Client;
using System.Threading.Tasks;
using Anatoli.Business.Domain;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.DataAccess.Models.Identity;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/identityAccounts")]
    public class IdentityAccountsController : AnatoliApiController
    {
        public class RequestModel
        {
            public string Username
            {
                get;
                set;
            }
            public string Password
            {
                get;
                set;
            }

            public string Scope { get; set; }
        }

        [AllowAnonymous, HttpPost, Route("login")]
        public async Task<IHttpActionResult> Login([FromBody] RequestModel model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
                    return StatusCode(System.Net.HttpStatusCode.NotFound);

                if (string.IsNullOrEmpty(model.Scope))
                    return BadRequest("نرم افزار کاربر مشخص نشده است");

                var additionalData = model.Scope.Split(',');
                var appOwner = Guid.Empty;
                var dataOwner = Guid.Empty;

                if (!Guid.TryParse(additionalData[0].Trim(), out appOwner))
                    return BadRequest("نرم افزار کاربر مشخص نشده است");

                if (!Guid.TryParse(additionalData[1].Trim(), out dataOwner))
                    return BadRequest("شرکت کاربر مشخص نشده است");


                //To be sure the user data is exist & correct.
                var user = await CheckUserDataInCloud(model, appOwner, dataOwner);
                if (user == null)
                    return BadRequest("اطلاعات کاربری یافت نشد، لطفا ابتدا ثبت نام نمایید.");

                var _userRoleIds = user.Roles.Select(s => s.RoleId).ToList();

                var roles = AppRoleManager.Roles
                                          .Where(p => _userRoleIds.Contains(p.Id))
                                          .Select(s => s.Name)
                                          .ToArray();

                await userNotExistInIdentityThenRegisterIt(user, model, string.Join(",", roles));


                var token = await GetToken(model.Username, model.Password);

                if (!string.IsNullOrEmpty(token.Error))
                    return Unauthorized();

                return Ok(new
                {
                    access_token = token.AccessToken,
                    userName = model.Username,
                    token = token
                });
            }
            catch (Exception ex)
            {
                return BadRequest("لطفا دوباره تلاش نمایید");
            }
        }

        private async Task userNotExistInIdentityThenRegisterIt(User user, RequestModel data, string roles)
        {
            //check user data exist in identity server, if not register it.
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["IdentityServerUrl"]);

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", data.Username),
                    new KeyValuePair<string, string>("password", data.Password),
                    new KeyValuePair<string, string>("fullname", user.FullName),
                    new KeyValuePair<string, string>("email", user.Email),
                    new KeyValuePair<string, string>("EmailConfirmed", user.EmailConfirmed.ToString()),
                    new KeyValuePair<string, string>("phonenumber", user.PhoneNumber),
                    new KeyValuePair<string, string>("phonenumberConfirmed", user.PhoneNumberConfirmed.ToString()),
                    new KeyValuePair<string, string>("roles", roles),
                });

                await client.PostAsync("/api/account/signupUserofCloud", formContent);
            }
        }

        private async Task<TokenResponse> GetToken(string user, string password)
        {
            var client = new TokenClient("https://localhost:44300/core/connect/token", "anatoliCloudClient", "anatoliCloudClient");

            var result = await client.RequestResourceOwnerPasswordAsync(user, password, "read write webapis offline_access openid email roles profile");

            return result;
        }

        private async Task<User> CheckUserDataInCloud(RequestModel data, Guid ownerKey, Guid dataOwnerKey)
        {
            var user = await new UserDomain(ownerKey, dataOwnerKey).FindByNameOrEmailOrPhoneAsync(data.Username);

            if (user == null)
                return null;

            return await AppUserManager.FindAsync(user.UserName, data.Password);
        }
    }
}