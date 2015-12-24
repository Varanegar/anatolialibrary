using Anatoli.Cloud.WebApi.Infrastructure;
using Anatoli.Cloud.WebApi.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;

namespace Anatoli.Cloud.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {

        private ModelFactory _modelFactory;
        private ApplicationUserManager _AppUserManager = null;
        private ApplicationRoleManager _AppRoleManager = null;

        protected ApplicationUserManager AppUserManager
        {
            get
            {
                return _AppUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        protected ApplicationRoleManager AppRoleManager
        {
            get
            {
                return _AppRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }

        public BaseApiController()
        {
        }

        protected ModelFactory TheModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(this.Request, this.AppUserManager);
                }
                return _modelFactory;
            }
        }

        protected IHttpActionResult GetErrorResult(ModelStateDictionary modelState)
        {
            
            return BadRequest(modelState);
        }
        protected IHttpActionResult GetErrorResult(string error)
        {
            if (error == null)
            {
                return InternalServerError();
            }
            ModelState.AddModelError("", error);
            return BadRequest(ModelState);

        }

        protected IHttpActionResult GetErrorResult(Exception ex)
        {
            if (ex == null)
            {
                return InternalServerError();
            }
            ModelState.AddModelError("", ex);
            return BadRequest(ModelState);

        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest(ModelState);
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}