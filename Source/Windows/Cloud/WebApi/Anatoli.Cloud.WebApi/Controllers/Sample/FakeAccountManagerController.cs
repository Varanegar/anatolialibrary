using System;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using Anatoli.DataAccess.Repositories;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.DataAccess;

namespace Anatoli.Cloud.WebApi.Controllers.Sample
{
    [RoutePrefix("api/v0/sample/FakeAccountManager")]
    public class FakeAccountManagerController : ApiController
    {
        #region Properties
        public UserManager<User, Guid> UserManager { get; private set; }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return Request.GetOwinContext().Authentication;
            }
        }
        AnatoliDbContext _dbc;
        #endregion

        #region Ctors
        public FakeAccountManagerController() : this(new AnatoliDbContext()) { }
        public FakeAccountManagerController(AnatoliDbContext dbc)
            : this(new UserManager<User, Guid>(new AnatoliUserStore(dbc)))
        {
            _dbc = dbc;
        }

        public FakeAccountManagerController(UserManager<User, Guid> userManager)
        {
            UserManager = userManager;
        }
        #endregion
        [Route("Test")]
        [HttpGet]
        public async Task<bool> Test()
        {
            return await Task.FromResult(true);
        }


        [Route("SignUp")]
        [HttpGet]
        public async Task<bool> SignUp(string userName, string pass, string email, string fullName)
        {

            var owner = Guid.Parse("CB11335F-6D14-49C9-9798-AD61D02EDBE1");
            var principalRepository = new PrincipalRepository(_dbc);
            var privateLabelOwner = principalRepository.GetQuery().Where(p => p.Id == owner).FirstOrDefault();

            var _id = Guid.NewGuid();
            var user = new User
            {
                Id = _id,
                PrivateLabelOwner = privateLabelOwner,
                AddedBy = privateLabelOwner,
                Number_ID = new Random(DateTime.Now.Millisecond).Next(),
                CreatedDate = DateTime.Now,
                LastEntry = DateTime.Now,
                LastUpdate = DateTime.Now,
                Principal = new Principal { Id = _id, Title = fullName },
                FullName = fullName,
                UserName = userName,
                Password = pass,
                Email = email,
            };

            // var user = new User() { UserName = userName, Email = email, FullName = fullName };

            var result = await UserManager.CreateAsync(user, pass);

            if (result.Succeeded)
                await SignInAsync(user, isPersistent: true);

            return true;
        }

        private async Task SignInAsync(User user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        [Route("Login")]
        [HttpGet]
        public async Task<bool> Login(string userName, string pass)
        {

            var user = await UserManager.FindAsync(userName, pass);
            if (user != null)
            {
                await SignInAsync(user, true);

                return true;
            }
            return false;
        }

        [Route("GetCurrentUser")]
        [HttpGet]
        [Authorize]
        public async Task<string> GetCurrentUser()
        {
            var user = await UserManager.FindByIdAsync(Guid.Parse(User.Identity.GetUserId()));

            return user.FullName;
        }
    }
}
