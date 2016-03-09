using Anatoli.Cloud.WebApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Anatoli.Cloud.WebApi.Models;
using Anatoli.DataAccess.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data;
using System.Web.Http;
using Anatoli.DataAccess;
using Anatoli.Business.Domain;
using Anatoli.ViewModels.CustomerModels;
using Anatoli.ViewModels.BaseModels;
using Anatoli.Business.Domain.Authorization;
using Anatoli.Cloud.WebApi.Classes;
using Newtonsoft.Json;
using Anatoli.Business.Proxy.Concretes.AuthorizationProxies;
using Anatoli.ViewModels.User;
using Anatoli.Cloud.WebApi.Services;
using Anatoli.DataAccess.Repositories;
using Anatoli.ViewModels;
using Anatoli.Rastak.ViewModels;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : AnatoliApiController
    {
        [AnatoliAuthorize(Roles = "Admin,AuthorizedApp")] //Resource = "Pages", Action = "List"
        [Route("myWebpages"), HttpPost]
        public async Task<IHttpActionResult> GetPages()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();

            var data = await new AuthorizationDomain(OwnerKey).GetPermissionsForPrincipal(Guid.Parse(userId));

            var model = data.Where(p => p.Permission.Resource == "Pages").Select(s=>s.Permission).ToList();

            return Ok(new PermissionProxy().Convert(model.ToList()));
        }

        [Authorize(Roles = "Admin")]
        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            //Only SuperAdmin or Admin can delete users (Later when implement roles)
            var identity = User.Identity as System.Security.Claims.ClaimsIdentity;

            var model = this.AppUserManager.Users.ToList().Select(u => this.TheModelFactory.Create(u));

            return Ok(model);
        }

        [Authorize(Roles = "Admin")]
        [Route("permissions"), HttpPost]
        public async Task<IHttpActionResult> GetPersmissions()
        {
            var model = await new AuthorizationDomain(OwnerKey).GetAllPermissions();

            return Ok(new PermissionProxy().Convert(model.ToList()));
        }

        [Authorize(Roles = "Admin")]
        [Route("getPersmissionsOfUser"), HttpPost]
        public async Task<IHttpActionResult> GetPersmissionsOfUser([FromBody] RequestModel data)
        {
            var model = await new AuthorizationDomain(OwnerKey).GetPermissionsForPrincipal(Guid.Parse(data.userId));

            return Ok(new PrincipalPermissionProxy().Convert(model.ToList()));
        }

        [Authorize(Roles = "Admin")]
        [Route("savePermissions"), HttpPost]
        public async Task<IHttpActionResult> SavePersmissions([FromBody] RequestModel data)
        {
            var model = JsonConvert.DeserializeObject<dynamic>(data.data);

            var pp = new List<PrincipalPermission>();
            foreach (var itm in model.permissions)
                pp.Add(new PrincipalPermission
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    Grant = itm.grant.Value,
                    Permission_Id = Guid.Parse(itm.id.Value),
                    Principal_Id = Guid.Parse(model.userId.Value),
                    PrivateLabelOwner_Id = OwnerKey
                });

            await new AuthorizationDomain(OwnerKey).SavePermissions(pp, Guid.Parse(model.userId.Value));

            return Ok(new { });
        }


        [Authorize(Roles = "AuthorizedApp")]
        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string Id)
        {
            //Only SuperAdmin or Admin can delete users (Later when implement roles)
            var user = await this.AppUserManager.FindByIdAsync(Id);

            if (user != null)
            {
                return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();

        }

        [Authorize(Roles = "AuthorizedApp,User")]
        [Route("user/{username}")]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            //Only SuperAdmin or Admin can delete users (Later when implement roles)
            var user = await this.AppUserManager.FindByNameAsync(username);

            if (user != null)
            {
                return Ok(this.TheModelFactory.Create(user));
            }

            return Ok();

        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("createbybackoffice")]
        public async Task<IHttpActionResult> CreateUserByBackoffice(CreateUserBindingModel createUserModel)
        {
            try
            {
                Uri locationHeader = null;
                if (!ModelState.IsValid)
                    return GetErrorResult(ModelState);

                var id = Guid.NewGuid();
                var user = new User()
                {
                    Id = id.ToString(),
                    UserName = createUserModel.Username,
                    Email = createUserModel.Email,
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    CreatedDate = DateTime.Now,
                    PhoneNumber = createUserModel.Mobile,
                    PrivateLabelOwner = new Principal { Id = createUserModel.PrivateOwnerId },
                    Principal = new Principal { Id = id, Title = createUserModel.FullName }
                };

                if (createUserModel.Email != null)
                {
                    var emailUser = await this.AppUserManager.FindByEmailAsync(createUserModel.Email);
                    if (emailUser != null)
                        return GetErrorResult("ایمیل شما قبلا استفاده شده است");
                }
                using (var transaction = Request.GetOwinContext().Get<AnatoliDbContext>().Database.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        var addUserResult = await this.AppUserManager.CreateAsync(user, createUserModel.Password);

                        if (!addUserResult.Succeeded)
                            return GetErrorResult(addUserResult);

                        if (this.AppRoleManager.Roles.Where(p => p.Name == "User").FirstOrDefault() == null)
                        {
                            this.AppRoleManager.Create(new IdentityRole
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = "User"
                            });

                            this.AppRoleManager.Create(new IdentityRole
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = createUserModel.RoleName
                            });
                        }

                        this.AppUserManager.AddToRoles(user.Id, new string[] { "User" });



                        var customerDomain = new CustomerDomain(createUserModel.PrivateOwnerId, Request.GetOwinContext().Get<AnatoliDbContext>());
                        var customer = new CustomerViewModel()
                        {
                            Mobile = createUserModel.Mobile,
                            UniqueId = Guid.Parse(user.Id),
                            Email = createUserModel.Email,
                        };


                        List<CustomerViewModel> customerList = new List<CustomerViewModel>();
                        customerList.Add(customer);
                        await customerDomain.PublishAsync(customerList);

                        List<BasketViewModel> basketList = new List<BasketViewModel>();
                        basketList.Add(new BasketViewModel(BasketViewModel.CheckOutBasketTypeId, customer.UniqueId));
                        basketList.Add(new BasketViewModel(BasketViewModel.FavoriteBasketTypeId, customer.UniqueId));

                        var basketDomain = new BasketDomain(createUserModel.PrivateOwnerId, Request.GetOwinContext().Get<AnatoliDbContext>());
                        await basketDomain.PublishAsync(basketList);

                        locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

                        if(createUserModel.SendPassSMS)
                        {
                            String hashedNewPassword = this.AppUserManager.PasswordHasher.HashPassword(createUserModel.Password);
                            await SendResetPasswordSMS(user, Request.GetOwinContext().Get<AnatoliDbContext>(), hashedNewPassword);
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return GetErrorResult(ex);
                    }
                }

                return Created(locationHeader, TheModelFactory.Create(user));
            }
            catch (Exception ex)
            {

                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("create")]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
        {
            createUserModel.SendPassSMS = true;
            var result = await CreateUserByBackoffice(createUserModel);
            return result;
            
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
        public async Task<IHttpActionResult> ConfirmEmail(string userId = "", string code = "")
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                ModelState.AddModelError("", "User Id and Code are required");
                return BadRequest(ModelState);
            }

            IdentityResult result = await this.AppUserManager.ConfirmEmailAsync(userId, code);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return GetErrorResult(result);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ConfirmMobile", Name = "ConfirmMobileRoute")]
        public async Task<IHttpActionResult> ConfirmPhoneNumber(string username = "", string code = "")
        {
            var userStore = new AnatoliUserStore(Request.GetOwinContext().Get<AnatoliDbContext>());
            var user = await userStore.FindByNameAsync(username);
            bool result = await userStore.VerifySMSCodeAsync(user, code);

            if(result)
                return Ok(new BaseViewModel());
            else
                return BadRequest("کد نامعتبر است یا زمان مجاز آن به پایان رسیده است");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ResendPassCode", Name = "ResendPassCodeRoute")]
        public async Task<IHttpActionResult> ResendPassCode(string username = "")
        {
            var userStore = new AnatoliUserStore(Request.GetOwinContext().Get<AnatoliDbContext>());
            var user = await userStore.FindByNameAsync(username);
            await SendResetPasswordSMS(user, Request.GetOwinContext().Get<AnatoliDbContext>(), null);
            return Ok(new BaseViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("SendPassCode", Name = "SendPassCodeRoute")]
        public async Task<IHttpActionResult> SendPassCode(string username = "")
        {
            var userStore = new AnatoliUserStore(Request.GetOwinContext().Get<AnatoliDbContext>());
            var user = await userStore.FindByNameAsync(username);
            await SendResetPasswordSMS(user, Request.GetOwinContext().Get<AnatoliDbContext>(), null);
            return Ok(new BaseViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ResetPassword", Name = "ResetPasswordRoute")]
        public async Task<IHttpActionResult> ResetPassword(string username = "", string password = "")
        {
            var userStore = new AnatoliUserStore(Request.GetOwinContext().Get<AnatoliDbContext>());
            var user = await userStore.FindByNameAsync(username);
            if (user == null)
                return GetErrorResult("کاربر یافت نشد");
            //this.AppUserManager.AddPassword()
            String hashedNewPassword = this.AppUserManager.PasswordHasher.HashPassword(password);
            await SendResetPasswordSMS(user, Request.GetOwinContext().Get<AnatoliDbContext>(), hashedNewPassword);
            return Ok(new BaseViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ResetPasswordByCode", Name = "ResetPasswordByCodeRoute")]
        public async Task<IHttpActionResult> ResetPassword(string username = "", string password = "", string code="")
        {
            var userStore = new AnatoliUserStore(Request.GetOwinContext().Get<AnatoliDbContext>());
            var user = await userStore.FindByNameAsync(username);
            if (user == null)
                return GetErrorResult("کاربر یافت نشد");

            String hashedNewPassword = this.AppUserManager.PasswordHasher.HashPassword(password);
            bool result = await userStore.ResetPasswordByCodeAsync(user, hashedNewPassword, code);

            if (result)
                return Ok(new BaseViewModel());
            else
                return BadRequest("کد نامعتبر است یا زمان مجاز آن به پایان رسیده است");
        }

        [Authorize(Roles = "AuthorizedApp,User")]
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await this.AppUserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            model.NewPassword = "****";
            model.OldPassword = "****";
            model.ConfirmPassword = "****";
            return Ok(model);
        }

        [Authorize(Roles = "Admin")]
        [Route("user/delete/{id:guid}")]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {

            //Only SuperAdmin or Admin can delete users (Later when implement roles)

            var appUser = await this.AppUserManager.FindByIdAsync(id);

            if (appUser != null)
            {
                IdentityResult result = await this.AppUserManager.DeleteAsync(appUser);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                return Ok();

            }

            return NotFound();

        }

        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}/roles")]
        [HttpPut]
        public async Task<IHttpActionResult> AssignRolesToUser([FromUri] string id, [FromBody] string[] rolesToAssign)
        {

            var appUser = await this.AppUserManager.FindByIdAsync(id);

            if (appUser == null)
            {
                return NotFound();
            }

            var currentRoles = await this.AppUserManager.GetRolesAsync(appUser.Id);

            var rolesNotExists = rolesToAssign.Except(this.AppRoleManager.Roles.Select(x => x.Name)).ToArray();

            if (rolesNotExists.Count() > 0)
            {

                ModelState.AddModelError("", string.Format("Roles '{0}' does not exixts in the system", string.Join(",", rolesNotExists)));
                return BadRequest(ModelState);
            }

            IdentityResult removeResult = await this.AppUserManager.RemoveFromRolesAsync(appUser.Id, currentRoles.ToArray());

            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove user roles");
                return BadRequest(ModelState);
            }

            IdentityResult addResult = await this.AppUserManager.AddToRolesAsync(appUser.Id, rolesToAssign);

            if (!addResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to add user roles");
                return BadRequest(ModelState);
            }

            return Ok();

        }

        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}/assignclaims")]
        [HttpPut]
        public async Task<IHttpActionResult> AssignClaimsToUser([FromUri] string id, [FromBody] List<ClaimBindingModel> claimsToAssign)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appUser = await this.AppUserManager.FindByIdAsync(id);

            if (appUser == null)
            {
                return NotFound();
            }

            foreach (ClaimBindingModel claimModel in claimsToAssign)
            {
                if (appUser.Claims.Any(c => c.ClaimType == claimModel.Type))
                {

                    await this.AppUserManager.RemoveClaimAsync(id, ExtendedClaimsProvider.CreateClaim(claimModel.Type, claimModel.Value));
                }

                await this.AppUserManager.AddClaimAsync(id, ExtendedClaimsProvider.CreateClaim(claimModel.Type, claimModel.Value));
            }

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}/removeclaims")]
        [HttpPut]
        public async Task<IHttpActionResult> RemoveClaimsFromUser([FromUri] string id, [FromBody] List<ClaimBindingModel> claimsToRemove)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appUser = await this.AppUserManager.FindByIdAsync(id);

            if (appUser == null)
            {
                return NotFound();
            }

            foreach (ClaimBindingModel claimModel in claimsToRemove)
            {
                if (appUser.Claims.Any(c => c.ClaimType == claimModel.Type))
                {
                    await this.AppUserManager.RemoveClaimAsync(id, ExtendedClaimsProvider.CreateClaim(claimModel.Type, claimModel.Value));
                }
            }

            return Ok();
        }

        private async Task SendResetPasswordSMS(User user, AnatoliDbContext dbContext, string pass)
        {
            string message = "";
            string phoneNumber = "";
            phoneNumber = user.PhoneNumber;
            //phoneNumber = "09122039700";
            if (phoneNumber.Length >= 10)
            {
                phoneNumber = phoneNumber.Substring(phoneNumber.Length - 10);
                phoneNumber = "98" + phoneNumber;
                
                Random rnd = new Random();
                int rndValue = rnd.Next(111111, 999999);
                message = "شماره کد رمز" + rndValue;
                
                var smsManager = new SMSManager();
                var userStore = new AnatoliUserStore(dbContext);
                await userStore.SetResetSMSCodeAsync(user, rndValue.ToString(), pass);
                await smsManager.SendSMS(phoneNumber, message);
            }
        }
    }
}