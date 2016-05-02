﻿using Anatoli.Cloud.WebApi.Infrastructure;
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
//using Anatoli.Business.Domain.Authorization;
using Anatoli.Cloud.WebApi.Classes;
using Newtonsoft.Json;
//using Anatoli.Business.Proxy.Concretes.AuthorizationProxies;
using Anatoli.ViewModels.User;
using Anatoli.DataAccess.Repositories;
using Anatoli.ViewModels;
using Anatoli.Business.Proxy.CustomerConcretes;
using Anatoli.Business.Proxy.Concretes.ProductConcretes;
using Anatoli.Business.Domain.Authorization;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : AnatoliApiController
    {
        [AnatoliAuthorize(Roles = "Admin,AuthorizedApp,User")] //Resource = "Pages", Action = "List"
        [Route("myWebpages"), HttpPost]
        public async Task<IHttpActionResult> GetPages()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var data = await new AuthorizationDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetPermissionsForPrincipal(userId);

            var result = data.Where(p => p.Action == "Page").ToList();

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            //Only SuperAdmin or Admin can delete users (Later when implement roles)
            var identity = User.Identity as System.Security.Claims.ClaimsIdentity;

            var model = this.AppUserManager.Users.ToList().Where(f => f.DataOwnerId == DataOwnerKey ).Select(u => this.TheModelFactory.Create(u));

            return Ok(model);
        }

        [Authorize(Roles = "Admin")]
        [Route("permissions"), HttpPost]
        public async Task<IHttpActionResult> GetPersmissions()
        {
            var model = await new AuthorizationDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetAllPermissions();
            return Ok(model);
        }

        [Authorize(Roles = "Admin")]
        [Route("permissionCatalogs"), HttpPost]
        public async Task<IHttpActionResult> GetPersmissionCatalogs()
        {
            var model = await new AuthorizationDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetAllPermissionCatalogs();

            return Ok(model);
        }

        [Authorize(Roles = "Admin")]
        [Route("getPersmissionCatalogsOfUser"), HttpPost]
        public async Task<IHttpActionResult> GetPersmissionCatalogsOfUser([FromBody] BaseRequestModel data)
        {
            var model = await new AuthorizationDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetPermissionCatalogsForPrincipal(data.userId);

            return Ok(model.ToList());
        }

        [Authorize(Roles = "Admin")]
        [Route("getPersmissionsOfUser"), HttpPost]
        public async Task<IHttpActionResult> GetPersmissionsOfUser([FromBody] BaseRequestModel data)
        {
            var model = await new AuthorizationDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetPermissionsForPrincipal(data.userId);

            return Ok(model.ToList());
        }

        [Authorize(Roles = "Admin")]
        [Route("savePermissions"), HttpPost]
        public async Task<IHttpActionResult> SavePersmissions([FromBody] BaseRequestModel data)
        {
            var model = JsonConvert.DeserializeObject<dynamic>(data.data);

            var pp = new List<PrincipalPermission>();
            foreach (var itm in model.permissions)
                pp.Add(new PrincipalPermission
                {
                    Id = Guid.NewGuid(),
                    Grant = itm.grant.Value,
                    Permission_Id = Guid.Parse(itm.id.Value),
                    PrincipalId = Guid.Parse(model.userId.Value),
                });

            await new AuthorizationDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).SavePermissions(pp, Guid.Parse(model.userId.Value));

            return Ok(new { });
        }

        [Authorize(Roles = "Admin")]
        [Route("savePermissionCatalogs"), HttpPost]
        public async Task<IHttpActionResult> SavePersmissionCatalogs([FromBody] BaseRequestModel data)
        {
            var model = JsonConvert.DeserializeObject<dynamic>(data.data);

            var ppc = new List<PrincipalPermissionCatalog>();
            foreach (var itm in model.permissionCatalogs)
                ppc.Add(new PrincipalPermissionCatalog
                {
                    Id = Guid.NewGuid(),
                    Grant = itm.grant.Value == true ? 1 : 0,
                    PermissionCatalog_Id = Guid.Parse(itm.id.Value),
                    PrincipalId = Guid.Parse(model.userId.Value),
                });

            await new AuthorizationDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).SavePermissionCatalogs(ppc, Guid.Parse(model.userId.Value));

            return Ok(new { });
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("user/{id:guid}", Name = "GetUserById"), HttpPost]
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

        [Authorize(Roles = "AuthorizedApp")]
        [Route("user/{username}"), HttpPost, HttpGet]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            try
            {
                var user = await GetUserByUserName(username);
                //Only SuperAdmin or Admin can delete users (Later when implement roles)
                if (user != null)
                {
                    return Ok(this.TheModelFactory.Create(user));
                }
                return BadRequest("کاربر یافت نشد");
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }

        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("createbybackoffice"), HttpPost]
        public async Task<IHttpActionResult> CreateUserByBackoffice(CreateUserBindingModel createUserModel)
        {
            try
            {
                var userDomain = new UserDomain(OwnerKey, DataOwnerKey, Request.GetOwinContext().Get<AnatoliDbContext>());

                var anatoliKey = Guid.Parse("79a0d598-0bd2-45b1-baaa-0a9cf9eff240");
                Uri locationHeader = null;
                if (!ModelState.IsValid)
                    return GetErrorResult(ModelState);

                var id = Guid.NewGuid();
                var user = new User()
                {
                    Id = id.ToString(),
                    UserName = (OwnerKey == anatoliKey && DataOwnerKey == anatoliKey)?createUserModel.Username:id.ToString(),
                    Email = createUserModel.Email,
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    CreatedDate = DateTime.Now,
                    PhoneNumber = createUserModel.Mobile,
                    ApplicationOwnerId = OwnerKey,
                    DataOwnerId = DataOwnerKey,
                    UserNameStr = createUserModel.Username
                };

                if (createUserModel.Email != null)
                {
                    var emailUser = await userDomain.GetByEmailAsync(createUserModel.Email);
                    if (emailUser != null)
                        return GetErrorResult("ایمیل شما قبلا استفاده شده است");
                }

                if (createUserModel.Password != null)
                {
                    var emailUser = await userDomain.GetByPhoneAsync(createUserModel.Mobile);
                    if (emailUser != null)
                        return GetErrorResult("ایمیل شما قبلا استفاده شده است");
                }

                if (createUserModel.Username != null)
                {
                    var emailUser = await userDomain.GetByUsernameAsync(createUserModel.Username);
                    if (emailUser != null)
                        return GetErrorResult("ایمیل شما قبلا استفاده شده است");
                }

                using (var transaction = Request.GetOwinContext().Get<AnatoliDbContext>().Database.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        Guid currentPrincipleId = Guid.NewGuid();
                        var userPrincipal = new Principal()
                        {
                            Id = currentPrincipleId,
                            Title = user.UserNameStr,
                            ApplicationOwnerId = (Guid)user.ApplicationOwnerId
                        };
                        await userDomain.SavePerincipal(userPrincipal);
                        user.PrincipalId = currentPrincipleId;

                        var addUserResult = await AppUserManager.CreateAsync(user, createUserModel.Password);

                        if (!addUserResult.Succeeded)
                            return GetErrorResult(addUserResult);

                        if (AppRoleManager.Roles.Where(p => p.Name == "User").FirstOrDefault() == null)
                        {
                            AppRoleManager.Create(new IdentityRole
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = "User"
                            });

                            AppRoleManager.Create(new IdentityRole
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = createUserModel.RoleName
                            });
                        }

                        AppUserManager.AddToRoles(user.Id, new string[] { "User" });

                        var customerDomain = new CustomerDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey, Request.GetOwinContext().Get<AnatoliDbContext>());
                        var customer = new CustomerViewModel()
                        {
                            Mobile = createUserModel.Mobile,
                            UniqueId = Guid.Parse(user.Id),
                            Email = createUserModel.Email,
                        };

                        List<CustomerViewModel> customerList = new List<CustomerViewModel>();
                        customer.CompanyId = DataOwnerKey;
                        customerList.Add(customer);
                        await customerDomain.PublishAsync(new CustomerProxy().ReverseConvert(customerList));

                        List<BasketViewModel> basketList = new List<BasketViewModel>();
                        basketList.Add(new BasketViewModel(BasketViewModel.CheckOutBasketTypeId, customer.UniqueId));
                        basketList.Add(new BasketViewModel(BasketViewModel.FavoriteBasketTypeId, customer.UniqueId));

                        var basketDomain = new BasketDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey, Request.GetOwinContext().Get<AnatoliDbContext>());
                        await basketDomain.PublishAsync(new BasketProxy().ReverseConvert(basketList));

                        locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

                        if (createUserModel.SendPassSMS)
                        {
                            var hashedNewPassword = AppUserManager.PasswordHasher.HashPassword(createUserModel.Password);

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
        [Route("create"), HttpPost]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
        {
            createUserModel.SendPassSMS = true;
            var result = await CreateUserByBackoffice(createUserModel);
            return result;
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("checkEmailExist"), HttpPost]
        public async Task<IHttpActionResult> CheckEmailExist([FromBody] AccountRequestModel model)
        {
            var emailUser = await GetUserByEMail(model.email);

            if (emailUser != null && emailUser.Id != model.userId)
                return Ok(false);

            return Ok(true);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("saveUser"), HttpPost]
        public async Task<IHttpActionResult> SaveUser([FromBody] BaseRequestModel model)
        {
            var userModel = JsonConvert.DeserializeObject<CreateUserBindingModel>(model.user);

            if (userModel.UniqueId != Guid.Empty && userModel.UniqueId != null)
                return await UpdateUser(userModel);
            else
                return await AddUser(userModel);
        }

        private async Task<IHttpActionResult> UpdateUser(CreateUserBindingModel model)
        {
            var user = AppUserManager.Users.Where(p => p.Id == model.UniqueId.ToString()).First();

            user.FullName = model.FullName;
            user.PhoneNumber = model.Mobile;

            if (!string.IsNullOrEmpty(model.Password) && model.Password == model.ConfirmPassword)
                user.PasswordHash = AppUserManager.PasswordHasher.HashPassword(model.Password);

            var userStore = new AnatoliUserStore(Request.GetOwinContext().Get<AnatoliDbContext>());
            user.PhoneNumberConfirmed = true;
            user.EmailConfirmed = true;
            await userStore.UpdateAsync(user);

            return Ok(model);
        }

        private async Task<IHttpActionResult> AddUser(CreateUserBindingModel model)
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
                    FullName = model.FullName,
                    UserName = model.Username,
                    Email = model.Email,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    CreatedDate = DateTime.Now,
                    PhoneNumber = model.Mobile,
                    ApplicationOwnerId = OwnerKey,
                };

                if (model.Email != null)
                {
                    var emailUser = await GetUserByEMail(model.Email);
                    //var emailUser = await AppUserManager.FindByEmailAsync(model.Email);
                    if (emailUser != null)
                        return GetErrorResult("ایمیل شما قبلا استفاده شده است");
                }

                using (var transaction = Request.GetOwinContext().Get<AnatoliDbContext>().Database.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        var addUserResult = await AppUserManager.CreateAsync(user, model.Password);

                        if (!addUserResult.Succeeded)
                            return GetErrorResult(addUserResult);

                        AppUserManager.AddToRoles(user.Id, new string[] { "User" });

                        locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

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
        [Route("getUser"), HttpPost]
        public IHttpActionResult GetUser([FromBody] BaseRequestModel model)
        {
            var user = AppUserManager.Users.Where(p => p.Id == model.userId)
                                     .Select(s => new
                                     {
                                         userId = s.Id,
                                         fullName = s.FullName,
                                         userName = s.UserName,
                                         email = s.Email,
                                         mobile = s.PhoneNumber,
                                     })
                                    .FirstOrDefault();

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
        public async Task<IHttpActionResult> ConfirmEmail([FromBody]UserRequestModel data)
        {
            if (string.IsNullOrWhiteSpace(data.userId) || string.IsNullOrWhiteSpace(data.code))
            {
                ModelState.AddModelError("", "User Id and Code are required");
                return BadRequest(ModelState);
            }

            IdentityResult result = await this.AppUserManager.ConfirmEmailAsync(data.userId, data.code);

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
        public async Task<IHttpActionResult> ConfirmPhoneNumber([FromBody]UserRequestModel data)
        {
            var userStore = new AnatoliUserStore(Request.GetOwinContext().Get<AnatoliDbContext>());
            var userInfo = await GetUserByUserName(data.username);
            var user = await userStore.FindByIdAsync(userInfo.Id);
            bool result = await userStore.VerifySMSCodeAsync(user, data.code);

            if (result)
                return Ok(new BaseViewModel());
            else
                return BadRequest("کد نامعتبر است یا زمان مجاز آن به پایان رسیده است");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ResendPassCode", Name = "ResendPassCodeRoute")]
        public async Task<IHttpActionResult> ResendPassCode([FromBody]UserRequestModel data)
        {
            try
            {
                var user = await GetUserByUserName(data.username);
                //var user = await userStore.FindByNameAsync(username);
                await SendResetPasswordSMS(user, Request.GetOwinContext().Get<AnatoliDbContext>(), null);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
            return Ok(new BaseViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("SendPassCode", Name = "SendPassCodeRoute")]
        public async Task<IHttpActionResult> SendPassCode([FromBody]UserRequestModel data)
        {
            var user = await GetUserByUserName(data.username);
            //var user = await userStore.FindByNameAsync(username);
            await SendResetPasswordSMS(user, Request.GetOwinContext().Get<AnatoliDbContext>(), null);
            return Ok(new BaseViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ResetPassword", Name = "ResetPasswordRoute")]
        public async Task<IHttpActionResult> ResetPassword([FromBody]UserRequestModel data)
        {
            var user = await GetUserByUserName(data.username);
            //var user = await userStore.FindByNameAsync(username);
            if (user == null)
                return GetErrorResult("کاربر یافت نشد");
            //this.AppUserManager.AddPassword()
            String hashedNewPassword = this.AppUserManager.PasswordHasher.HashPassword(data.password);
            await SendResetPasswordSMS(user, Request.GetOwinContext().Get<AnatoliDbContext>(), hashedNewPassword);
            return Ok(new BaseViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ResetPasswordByCode", Name = "ResetPasswordByCodeRoute")]
        public async Task<IHttpActionResult> ResetPasswordByCode([FromBody]UserRequestModel data)
        {
            var userStore = new AnatoliUserStore(Request.GetOwinContext().Get<AnatoliDbContext>());
            var user = await GetUserByUserName(data.username);
            //var user = await userStore.FindByNameAsync(username);
            if (user == null)
                return GetErrorResult("کاربر یافت نشد");

            var hashedNewPassword = AppUserManager.PasswordHasher.HashPassword(data.password);
            bool result = await userStore.ResetPasswordByCodeAsync(user, hashedNewPassword, data.code);

            if (result)
                return Ok(new BaseViewModel());
            else
                return BadRequest("کد نامعتبر است یا زمان مجاز آن به پایان رسیده است");
        }

        [Authorize(Roles = "AuthorizedApp,User")]
        [Route("ChangePassword")]
        [HttpPost]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await AppUserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

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
        [HttpPost]
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

        private async Task<User> GetUserByUserName(string username)
        {
            return await new UserDomain(OwnerKey, DataOwnerKey, Request.GetOwinContext().Get<AnatoliDbContext>()).GetByUsernameAsync(username);
        }

        private async Task<User> GetUserByEMail(string email)
        {
            return await new UserDomain(OwnerKey, DataOwnerKey, Request.GetOwinContext().Get<AnatoliDbContext>()).GetByEmailAsync(email);
        }
    }
}