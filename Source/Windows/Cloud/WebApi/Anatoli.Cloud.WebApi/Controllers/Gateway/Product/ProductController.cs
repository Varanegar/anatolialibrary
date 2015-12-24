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
        #region Char Group
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("chargroups")]
        public async Task<IHttpActionResult> GetCharGroups(string privateOwnerId)
        {
            var owner = Guid.Parse(privateOwnerId);
            var charGroupDomain = new CharGroupDomain(owner);
            var result = await charGroupDomain.GetAll();

            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("chargroups/save")]
        public async Task<IHttpActionResult> SaveCharGroups(string privateOwnerId, List<CharGroupViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var charGroupDomain = new CharGroupDomain(owner);
            await charGroupDomain.PublishAsync(data);
            return Ok();
        }
        #endregion

        #region Char Type
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("chartypes")]
        public async Task<IHttpActionResult> GetCharTypes(string privateOwnerId)
        {
            var owner = Guid.Parse(privateOwnerId);
            var charTypeDomain = new CharTypeDomain(owner);
            var result = await charTypeDomain.GetAll();

            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("chartypes/save")]
        public async Task<IHttpActionResult> SaveCharTypes(string privateOwnerId, List<CharTypeViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var charTypeDomain = new CharTypeDomain(owner);
            await charTypeDomain.PublishAsync(data);
            return Ok();
        }
        #endregion

        #region Products
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("products")]
        public async Task<IHttpActionResult> GetProducts(string privateOwnerId)
        {
            var owner = Guid.Parse(privateOwnerId);
            var productDomain = new ProductDomain(owner);
            var result = await productDomain.GetAll();

            return  Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("products/save")]
        public async Task<IHttpActionResult> SaveProducts(string privateOwnerId, List<ProductViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var productDomain = new ProductDomain(owner);
            await productDomain.PublishAsync(data);
            return Ok();
        }        
        #endregion

        #region Product Groups
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("productgroups")]
        public async Task<IHttpActionResult> GetProductGroups(string privateOwnerId)
        {
            var owner = Guid.Parse(privateOwnerId);
            var productGroupDomain = new ProductGroupDomain(owner);
            var result = await productGroupDomain.GetAll();
            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("productgroups/save")]
        public async Task<IHttpActionResult> SaveProductGroups(string privateOwnerId, List<ProductGroupViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var productGroupDomain = new ProductGroupDomain(owner);
            await productGroupDomain.PublishAsync(data);
            return Ok();
        }        
        #endregion

    }
}
