using Anatoli.Business.Domain;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.ViewModels.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/product")]
    public class ProductController : AnatoliApiController
    {
        #region Char Group
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("chargroups")]
        public async Task<IHttpActionResult> GetCharGroups(string privateOwnerId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var charGroupDomain = new CharGroupDomain(owner);
                var result = await charGroupDomain.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("chargroups/after")]
        public async Task<IHttpActionResult> GetCharGroups(string privateOwnerId, string dateAfter)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var charGroupDomain = new CharGroupDomain(owner);
                var validDate = DateTime.Parse(dateAfter);
                var result = await charGroupDomain.GetAllChangedAfter(validDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("chargroups/save")]
        public async Task<IHttpActionResult> SaveCharGroups(string privateOwnerId, List<CharGroupViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var charGroupDomain = new CharGroupDomain(owner);
                var result = await charGroupDomain.PublishAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        #endregion

        #region Char Type
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("chartypes")]
        public async Task<IHttpActionResult> GetCharTypes(string privateOwnerId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var charTypeDomain = new CharTypeDomain(owner);
                var result = await charTypeDomain.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("chartypes/after")]
        public async Task<IHttpActionResult> GetCharTypes(string privateOwnerId, string dateAfter)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var charTypeDomain = new CharTypeDomain(owner);
                var validDate = DateTime.Parse(dateAfter);
                var result = await charTypeDomain.GetAllChangedAfter(validDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("chartypes/save")]
        public async Task<IHttpActionResult> SaveCharTypes(string privateOwnerId, List<CharTypeViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var charTypeDomain = new CharTypeDomain(owner);
                var result = await charTypeDomain.PublishAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        #endregion

        #region Products
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("products")]
        public async Task<IHttpActionResult> GetProducts(string privateOwnerId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var productDomain = new ProductDomain(owner);
                var result = await productDomain.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        [AnatoliAuthorize(Roles = "AuthorizedApp, User", Resource = "Product", Action = "SearchProducts")]
        [Route("searchProducts"), HttpPost]
        public async Task<IHttpActionResult> SearchProductList([FromBody] RequestModel data)
        {
            try
            {
                var model = await new ProductDomain(OwnerKey).Search(data.searchTerm);

                return Ok(model.Select(s => new { id = s.UniqueId, name = s.ProductName }).ToList());
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("products/after")]
        public async Task<IHttpActionResult> GetProducts(string privateOwnerId, string dateAfter)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var productDomain = new ProductDomain(owner);
                var validDate = DateTime.Parse(dateAfter);
                var result = await productDomain.GetAllChangedAfter(validDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("save")]
        public async Task<IHttpActionResult> SaveProducts(string privateOwnerId, List<ProductViewModel> data)
        {
            try
            {
                if (data != null) log.Info("save product count : " + data.Count);
                var owner = Guid.Parse(privateOwnerId);
                var productDomain = new ProductDomain(owner);
                var result = await productDomain.PublishAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [AnatoliAuthorize(Roles = "AuthorizedApp", Resource = "Product", Action = "ProductTypes")]
        [Route("productTypes"), HttpPost]
        public async Task<IHttpActionResult> GetProductTypes()
        {
            try
            {
                var model = await new ProductTypeDomain(OwnerKey).GetAll();

                return Ok(model);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        #endregion

        #region Product Groups
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("productgroups")]
        public async Task<IHttpActionResult> GetProductGroups(string privateOwnerId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var productGroupDomain = new ProductGroupDomain(owner);
                var result = await productGroupDomain.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("productgroups/after")]
        public async Task<IHttpActionResult> GetProductGroups(string privateOwnerId, string dateAfter)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var productGroupDomain = new ProductGroupDomain(owner);
                var validDate = DateTime.Parse(dateAfter);
                var result = await productGroupDomain.GetAllChangedAfter(validDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("productgroups/save")]
        public async Task<IHttpActionResult> SaveProductGroups(string privateOwnerId, List<ProductGroupViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var productGroupDomain = new ProductGroupDomain(owner);
                var result = await productGroupDomain.PublishAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        #endregion

        #region Product Main Groups
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("mainproductgroups")]
        public async Task<IHttpActionResult> GetMainProductGroups(string privateOwnerId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var productGroupDomain = new MainProductGroupDomain(owner);
                var result = await productGroupDomain.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        [AnatoliAuthorize(Roles = "AuthorizedApp, User", Resource = "Product", Action = "MainProductGroupList")]
        [Route("mainProductGroupList"), HttpPost]
        public async Task<IHttpActionResult> GetMainProductGroupList()
        {
            try
            {
                var model = await new MainProductGroupDomain(OwnerKey).GetAll();

                return Ok(model.Select(s => new { id = s.UniqueId, groupName = s.GroupName, parent = s.ParentUniqueIdString }).ToList());
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("mainproductgroups/after")]
        public async Task<IHttpActionResult> GetMainProductGroups(string privateOwnerId, string dateAfter)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var productGroupDomain = new MainProductGroupDomain(owner);
                var validDate = DateTime.Parse(dateAfter);
                var result = await productGroupDomain.GetAllChangedAfter(validDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("mainproductgroups/save")]
        public async Task<IHttpActionResult> SaveMainProductGroups(string privateOwnerId, List<MainProductGroupViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var productGroupDomain = new MainProductGroupDomain(owner);
                var result = await productGroupDomain.PublishAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        #endregion
    }
}
