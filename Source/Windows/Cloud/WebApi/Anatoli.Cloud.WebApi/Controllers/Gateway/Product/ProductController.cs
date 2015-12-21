using Anatoli.Business;
using Anatoli.Business.Domain;
using Anatoli.Cloud.Gateway.Business.Region;
using Anatoli.ViewModels.ProductModels;
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
        [Authorize(Roles = "User")]
        [Route("chargroups")]
        public async Task<IHttpActionResult> GetCharGroups()
        {
            var owner = Guid.Parse("CB11335F-6D14-49C9-9798-AD61D02EDBE1");
            var charGroupDomain = new CharGroupDomain(owner);
            var result = await charGroupDomain.GetAll();

            return Ok(result);
        }

        //[Authorize(Roles = "AuthorizedApp")]
        [Authorize(Roles = "User")]
        [Route("chargroups/save")]
        public async Task<IHttpActionResult> SaveCharGroups(List<CharGroupViewModel> data)
        {
            var owner = Guid.Parse("CB11335F-6D14-49C9-9798-AD61D02EDBE1");
            var charGroupDomain = new CharGroupDomain(owner);
            await charGroupDomain.PublishAsync(data);
            return Ok();
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
