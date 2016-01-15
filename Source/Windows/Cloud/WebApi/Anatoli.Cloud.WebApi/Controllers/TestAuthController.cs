using System.Web.Http;
using Anatoli.Cloud.WebApi.Classes;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [AnatoliAuthorizeAttribute(Resource = "TestAuth")]
    [RoutePrefix("api/TestAuth")]
    public class TestAuthController : AnatoliApiController
    {
        [AnatoliAuthorizeAttribute(Resource = "TestAuth", Action = "GetSample")]
        [Route("getsample"), HttpPost]
        public IHttpActionResult GetSample()
        {
            return Ok(OwnerKey);
        }

    }
}
