using Anatoli.Cloud.Gateway.Business.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        [Authorize(Roles = "Admin")]
        [Route("/chargroups")]
        public IHttpActionResult GetCharGroups()
        {
            return Ok(ProductCharGroupCloudHandler.GetInstance().GetSampleData());
        }

        [Authorize(Roles = "Admin")]
        [Route("/chartypes")]
        public IHttpActionResult GetCharTypes()
        {
            return Ok(ProductCharTypeCloudHandler.GetInstance().GetSampleData());
        }
    }
}
