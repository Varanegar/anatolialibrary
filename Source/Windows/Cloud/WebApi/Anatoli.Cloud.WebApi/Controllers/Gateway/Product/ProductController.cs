using Anatoli.Business.Domain;
using Anatoli.Business.Proxy.ProductConcretes;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.ViewModels;
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
        [HttpPost]
        public async Task<IHttpActionResult> GetCharGroups()
        {
            try
            {
                var charGroupDomain = new CharGroupDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await charGroupDomain.GetAllAsync();

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
        [HttpPost]
        public async Task<IHttpActionResult> GetCharGroups([FromBody] BaseRequestModel data)
        {
            try
            {
                var charGroupDomain = new CharGroupDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = DateTime.Parse(data.dateAfter);
                var result = await charGroupDomain.GetAllChangedAfterAsync(validDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("chargroups/save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveCharGroups([FromBody] ProductRequestModel data)
        {
            try
            {
                var charGroupDomain = new CharGroupDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await charGroupDomain.PublishAsync(new CharGroupProxy().ReverseConvert(data.charGroupData));
                return Ok(data.charGroupData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("chargroups/checkdeleted")]
        [HttpPost]
        public async Task<IHttpActionResult> CheckDeletedCharGroups([FromBody] ProductRequestModel data)
        {
            try
            {
                var charGroupDomain = new CharGroupDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await charGroupDomain.CheckDeletedAsync(data.charGroupData);
                return Ok(data.charGroupData);
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
        [HttpPost]
        public async Task<IHttpActionResult> GetCharTypes()
        {
            try
            {
                var charTypeDomain = new CharTypeDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await charTypeDomain.GetAllAsync();

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
        [HttpPost]
        public async Task<IHttpActionResult> GetCharTypes([FromBody] BaseRequestModel model)
        {
            try
            {
                var charTypeDomain = new CharTypeDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = DateTime.Parse(model.dateAfter);
                var result = await charTypeDomain.GetAllChangedAfterAsync(validDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("chartypes/save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveCharTypes([FromBody] ProductRequestModel model)
        {
            try
            {
                var charTypeDomain = new CharTypeDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await charTypeDomain.PublishAsync(new CharTypeProxy().ReverseConvert(model.charTypeData));
                return Ok(model.charTypeData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("chartypes/checkdeleted")]
        [HttpPost]
        public async Task<IHttpActionResult> CheckDeletedCharTypes([FromBody] ProductRequestModel model)
        {
            try
            {
                var charTypeDomain = new CharTypeDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await charTypeDomain.CheckDeletedAsync(model.charTypeData);
                return Ok(model.charTypeData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        #endregion

        #region Product Tag
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("producttags")]
        [HttpPost]
        public async Task<IHttpActionResult> GetProductTags()
        {
            try
            {
                var domain = new ProductTagDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await domain.GetAllAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("producttags/after")]
        [HttpPost]
        public async Task<IHttpActionResult> GetProductTags([FromBody] BaseRequestModel model)
        {
            try
            {
                var domain = new ProductTagDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = DateTime.Parse(model.dateAfter);
                var result = await domain.GetAllChangedAfterAsync(validDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("producttags/save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveProductTags([FromBody] ProductRequestModel model)
        {
            try
            {
                var domain = new ProductTagDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await domain.PublishAsync(new ProductTagProxy().ReverseConvert(model.productTagData));
                return Ok(model.productTagData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("producttags/checkdeleted")]
        [HttpPost]
        public async Task<IHttpActionResult> CheckDeletedProductTags([FromBody] ProductRequestModel model)
        {
            try
            {
                var domain = new ProductTagDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await domain.CheckDeletedAsync(model.productTagData);
                return Ok(model.productTagData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        #endregion

        #region Product Tag Value
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("producttagvalues")]
        [HttpPost]
        public async Task<IHttpActionResult> GetProductTagValues()
        {
            try
            {
                var domain = new ProductTagValueDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await domain.GetAllAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("producttagvalues/after")]
        [HttpPost]
        public async Task<IHttpActionResult> GetProductTagValues([FromBody] BaseRequestModel model)
        {
            try
            {
                var domain = new ProductTagValueDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = DateTime.Parse(model.dateAfter);
                var result = await domain.GetAllChangedAfterAsync(validDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("producttagvalues/save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveProductTagValues([FromBody] ProductRequestModel model)
        {
            try
            {
                var domain = new ProductTagValueDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await domain.PublishAsync(new ProductTagValueProxy().ReverseConvert(model.productTagValueData));
                return Ok(model.productTagValueData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("producttagvalues/checkdeleted")]
        [HttpPost]
        public async Task<IHttpActionResult> CheckDeletedProductTagValues([FromBody] ProductRequestModel model)
        {
            try
            {
                var domain = new ProductTagValueDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await domain.CheckDeletedAsync(model.productTagValueData);
                return Ok(model.productTagValueData);
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
        [HttpPost]
        public async Task<IHttpActionResult> GetProducts()
        {
            try
            {
                var productDomain = new ProductDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await productDomain.GetAllAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [AnatoliAuthorize(Roles = "AuthorizedApp, User")] //, Resource = "Product", Action = "List"
        [Route("products/v2")]
        [HttpPost]
        [GzipCompression]
        public async Task<IHttpActionResult> GetProductsV2()
        {
            try
            {
                var result = await new ProductDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetAllAsync();

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
        public async Task<IHttpActionResult> SearchProductList([FromBody] BaseRequestModel data)
        {
            try
            {
                data.searchTerm = data.searchTerm.Replace("ی", "ي").Replace("ک", "ك");

                var model = await new ProductDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).Search(data.searchTerm);

                return Ok(model.Select(s => new { id = s.UniqueId, name = s.StoreProductName }).ToList());
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("products/after")]
        [HttpPost]
        public async Task<IHttpActionResult> GetProducts([FromBody] BaseRequestModel data)
        {
            try
            {
                var productDomain = new ProductDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = DateTime.Parse(data.dateAfter);
                var result = await productDomain.GetAllChangedAfterAsync(validDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin"), HttpPost]
        [Route("save")]
        public async Task<IHttpActionResult> SaveProducts([FromBody] ProductRequestModel data)
        {
            try
            {
                if (data != null) log.Info("save product count : " + data.productData.Count);
                var productDomain = new ProductDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await productDomain.PublishAsync(new ProductProxy().ReverseConvert(data.productData));
                return Ok(data.productData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin"), HttpPost]
        [Route("savesuppliers")]
        public async Task<IHttpActionResult> SaveProductSuppliers([FromBody] ProductRequestModel data)
        {
            try
            {
                var productDomain = new ProductDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await productDomain.SetProductSupplierData(data.productSupplierData);
                return Ok(data.productSupplierData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin"), HttpPost]
        [Route("savecharvalues")]
        public async Task<IHttpActionResult> SaveProductCharValues([FromBody] ProductRequestModel data)
        {
            try
            {
                var productDomain = new ProductDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await productDomain.SetProductCharValueData(data.productCharData);
                return Ok(data.productCharData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [AnatoliAuthorize(Roles = "DataSync, BaseDataAdmin", Resource = "Product", Action = "SaveProducts"), Route("saveProducts"), HttpPost]
        public async Task<IHttpActionResult> ChangeProductTypes([FromBody] ProductRequestModel data)
        {
            try
            {
                await new ProductDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).ChangeProductTypes(data.productData);

                return Ok(new { });
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }


        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("checkdeleted"), HttpPost]
        public async Task<IHttpActionResult> CheckeDeletedProducts([FromBody] ProductRequestModel data)
        {
            try
            {
                if (data != null) log.Info("save product count : " + data.productData.Count);
                var productDomain = new ProductDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey, true);
                await productDomain.CheckDeletedAsync(data.productData);
                return Ok(data.productData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [AnatoliAuthorize(Roles = "AuthorizedApp, User", Resource = "Product", Action = "ProductTypes")]
        [Route("productTypes"), HttpPost]
        public async Task<IHttpActionResult> GetProductTypes()
        {
            try
            {
                var model = await new ProductTypeDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetAllAsync();

                model.Add(new ViewModels.StockModels.ProductTypeViewModel { UniqueId = Guid.Empty, ProductTypeName = string.Empty });

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
        [HttpPost]
        public async Task<IHttpActionResult> GetProductGroups()
        {
            try
            {
                var productGroupDomain = new ProductGroupDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await productGroupDomain.GetAllAsync();
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
        [HttpPost]
        public async Task<IHttpActionResult> GetProductGroups([FromBody]BaseRequestModel model)
        {
            try
            {
                var productGroupDomain = new ProductGroupDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = DateTime.Parse(model.dateAfter);
                var result = await productGroupDomain.GetAllChangedAfterAsync(validDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("productgroups/save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveProductGroups([FromBody]ProductRequestModel data)
        {
            try
            {
                var productGroupDomain = new ProductGroupDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await productGroupDomain.PublishAsync(new ProductGroupProxy().ReverseConvert(data.productGroupData));
                return Ok(data.productGroupData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("productgroups/checkdeleted")]
        [HttpPost]
        public async Task<IHttpActionResult> CheckDeletedProductGroups([FromBody]ProductRequestModel data)
        {
            try
            {
                var productGroupDomain = new ProductGroupDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await productGroupDomain.CheckDeletedAsync(data.productGroupData);
                return Ok(data.productGroupData);
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
        [HttpPost]
        public async Task<IHttpActionResult> GetMainProductGroups()
        {
            try
            {
                var productGroupDomain = new MainProductGroupDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await productGroupDomain.GetAllAsync();
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
                var model = await new MainProductGroupDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetAllAsync();

                return Ok(model.Select(s => new { id = s.UniqueId, groupName = s.GroupName, parent = s.ParentUniqueIdString }).ToList());
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        [AnatoliAuthorize(Roles = "AuthorizedApp, User", Resource = "Product", Action = "MainProductGroupList")]
        [Route("filterMainProductGroupList"), HttpPost]
        public async Task<IHttpActionResult> FilterMainProductGroupList([FromBody] BaseRequestModel data)
        {
            try
            {
                data.searchTerm = data.searchTerm.Replace("ی", "ي").Replace("ک", "ك");

                var model = await new MainProductGroupDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).FilterMainProductGroupList(data.searchTerm);

                return Ok(model.ToList());
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("mainproductgroups/after")]
        [HttpPost]
        public async Task<IHttpActionResult> GetMainProductGroups([FromBody] BaseRequestModel data)
        {
            try
            {
                var productGroupDomain = new MainProductGroupDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = DateTime.Parse(data.dateAfter);
                var result = await productGroupDomain.GetAllChangedAfterAsync(validDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("mainproductgroups/save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveMainProductGroups([FromBody] ProductRequestModel data)
        {
            try
            {
                var productGroupDomain = new MainProductGroupDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await productGroupDomain.PublishAsync(new MainProductGroupProxy().ReverseConvert(data.mainProductGroupData));
                return Ok(data.mainProductGroupData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("mainproductgroups/checkdeleted")]
        [HttpPost]
        public async Task<IHttpActionResult> CheckDeletedMainProductGroups([FromBody] ProductRequestModel data)
        {
            try
            {
                var productGroupDomain = new MainProductGroupDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await productGroupDomain.CheckDeletedAsync(data.mainProductGroupData);
                return Ok(data.mainProductGroupData);
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
