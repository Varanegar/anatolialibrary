using Anatoli.Cloud.WebApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Anatoli.Cloud.WebApi.Models;
using System.Security.Claims;
using Aantoli.Common.Entity.Gateway.BaseValue;
using Anatoli.Cloud.Gateway.Business.BaseValue;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/basedata")]
    public class BaseTypeInfoController : BaseApiController
    {
        [Authorize(Roles = "AuthorizedApp")]
        [Route("basevalues")]
        public IHttpActionResult GetBaseValues()
        {
            return Ok(BaseValueTypeCloudHandler.GetInstance().GetSampleData());
        }
    }
}
