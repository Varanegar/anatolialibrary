using System;
using System.Net;
using System.Web;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using Anatoli.Business.Domain.Authorization;

namespace Anatoli.Cloud.WebApi.Classes
{
    public class AnatoliAuthorizeAttribute : AuthorizeAttribute
    {
        #region Properties
        private string _responseReason = "";
        public bool ByPassAuthorization { get; set; }

        public object OwnerKey
        {
            get
            {
                return HttpContext.Current.Request.Headers["OwnerKey"];
            }
        }
        public bool HasOwnerKey
        {
            get
            {
                return OwnerKey != null ? true : false;
            }
        }
        public string Resource { get; set; }
        public string Action { get; set; }
        #endregion

        #region Methods
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            if (!string.IsNullOrEmpty(_responseReason))
                actionContext.Response.ReasonPhrase = _responseReason;
        }

        private IEnumerable<AnatoliAuthorizeAttribute> GetApiAuthorizeAttributes(HttpActionDescriptor descriptor)
        {
            return descriptor.GetCustomAttributes<AnatoliAuthorizeAttribute>(true)
                             .Concat(descriptor.ControllerDescriptor.GetCustomAttributes<AnatoliAuthorizeAttribute>(true));
        }

        private bool IsApiPageRequested(HttpActionContext actionContext)
        {
            var apiAttributes = GetApiAuthorizeAttributes(actionContext.ActionDescriptor);
            if (apiAttributes != null && apiAttributes.Any())
                return true;
            return false;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                if (IsApiPageRequested(actionContext))
                    if (!HasOwnerKey)
                    {
                        this.HandleUnauthorizedRequest(actionContext);

                        _responseReason = "Application key required.";
                    }
                    else
                        base.OnAuthorization(actionContext);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        private bool HasWebApiAccess()
        {
            if (string.IsNullOrEmpty(Resource) && string.IsNullOrEmpty(Action))
                return true;

            var user = HttpContext.Current.User;

            if (user == null || string.IsNullOrEmpty(user.Identity.GetUserId()))
                return false;

            var permissions = new AuthorizationDomain(Guid.Parse(OwnerKey.ToString())).GetPermissionsForPrincipal(user.Identity.GetUserId(), Resource, Action);

            if (permissions == null || permissions.Count == 0 || permissions.Any(a => a.Grant == false))
                return false;

            //check resource action from db for this Principal.
            return true;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            //Todo: check request ownerKey with claim ownerKey here

            //logic for check whether we have an attribute with ByPassAuthorization = true e.g [ByPassAuthorization(true)], if so then just return true 
            if (ByPassAuthorization || GetApiAuthorizeAttributes(actionContext.ActionDescriptor).Any(x => x.ByPassAuthorization))
                return true;

            //checking against our custom table goes here
            if (!this.HasWebApiAccess())
            {
                this.HandleUnauthorizedRequest(actionContext);

                _responseReason = "Access Denied";

                return false;
            }

            return base.IsAuthorized(actionContext);
        }
        #endregion
    }

    public class RequireHttpsAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    ReasonPhrase = "HTTPS Required"
                };
            }
            else
            {
                base.OnAuthorization(actionContext);
            }
        }
    }
}