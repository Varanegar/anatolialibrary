using Anatoli.Business;
using Anatoli.Business.Domain;
using Anatoli.Cloud.Gateway.Business.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/product")]
    public class ProductController : ApiController
    {
        [Authorize(Roles = "AuthorizedApp")]
        [Route("chargroups")]
        public IHttpActionResult GetCharGroups()
        {
            return Ok(ProductCharGroupCloudHandler.GetInstance().GetSampleData());
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("chartypes")]
        public IHttpActionResult GetCharTypes()
        {
            return Ok(ProductCharTypeCloudHandler.GetInstance().GetSampleData());
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("productlist")]
        public async Task<IHttpActionResult> GetProducts()
        {
            var owner = Guid.Parse("CB11335F-6D14-49C9-9798-AD61D02EDBE1");
            var productDomain = new ProductDomain(owner);
            var result = await productDomain.GetAll();

            return  Ok(result);
           
            // return Ok(ProductCloudHandler.GetInstance().GetSampleData());
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("productgroups")]
        public IHttpActionResult GetProductGroups()
        {
            return Ok(ProductGroupCloudHandler.GetInstance().GetSampleData());
        }

    }
}
