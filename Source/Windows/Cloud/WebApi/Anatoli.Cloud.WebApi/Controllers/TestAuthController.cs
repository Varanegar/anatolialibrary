using System.Web.Http;
using Anatoli.Cloud.WebApi.Classes;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [AnatoliAuthorize(Resource = "TestAuth")]
    [RoutePrefix("api/TestAuth")]
    public class TestAuthController : AnatoliApiController
    {
        [AnatoliAuthorize(Resource = "TestAuth", Action = "GetSample")]
        [Route("getsample"), HttpPost]
        public IHttpActionResult GetSample()
        {
            return Ok(OwnerKey);
        }
    }
}
