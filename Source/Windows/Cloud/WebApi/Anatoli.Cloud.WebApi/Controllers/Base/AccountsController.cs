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

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : AnatoliApiController
    {
        [Authorize(Roles = "Admin")]
        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            //Only SuperAdmin or Admin can delete users (Later when implement roles)
            var identity = User.Identity as System.Security.Claims.ClaimsIdentity;

            return Ok(this.AppUserManager.Users.ToList().Select(u => this.TheModelFactory.Create(u)));
        }

        [Authorize(Roles = "Admin")]
        [Route("permissions"), HttpPost]
        public async Task<IHttpActionResult> GetPersmissions()
        {
            var model = await new PermissionDomain(OwnerKey).GetAll();

            return Ok(model);
        }

        [Authorize(Roles = "Admin")]
        [Route("getPersmissionsOfUser"), HttpPost]
        public IHttpActionResult GetPersmissionsOfUser([FromBody] RequestModel data)
        {
            var model = new AuthorizationDomain(OwnerKey).GetPermissionsForPrincipal(Guid.Parse(data.userId));

            return Ok(model);
        }

        [Authorize(Roles = "Admin")]
        [Route("savePermissions"), HttpPost]
        public async Task<IHttpActionResult> SavePersmissions([FromBody] RequestModel data)
        {
            var model = JsonConvert.DeserializeObject<dynamic>(data.data);

            var pp = new List<PrincipalPermission>();
            foreach (var itm in model.permissions)
            {
                pp.Add(new PrincipalPermission
                {
                    CreatedDate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    Grant = itm.grant.Value,
                    Permission = new Permission { Id = Guid.Parse(itm.id.Value) },
                    Principal = new Principal { Id = Guid.Parse(model.userId.Value) }
                });
            }

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

            return NotFound();

        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("create")]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
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
                    EmailConfirmed = true,
                    CreatedDate = DateTime.Now,
                    PhoneNumber = createUserModel.Mobile,
                    PrivateLabelOwner = new Principal { Id = createUserModel.PrivateOwnerId },
                    Principal = new Principal { Id = id, Title = createUserModel.FullName }
                };

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

        [Authorize]
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

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}")]
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

    }
}