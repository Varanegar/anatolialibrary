using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/base/region")]
    public class RegionController : ApiController
    {
        [Authorize(Roles = "AuthorizedApp")]
        [Route("cityregion")]
        public IHttpActionResult Get()
        {
            return Ok();
        }
    }
}
