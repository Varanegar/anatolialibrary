using Anatoli.Business.Domain;
using Anatoli.Business.Proxy.Concretes.ProductConcretes;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.ViewModels;
using Anatoli.ViewModels.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/base/brand")]
    public class BrandController : AnatoliApiController
    {
        #region Brand
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("brands")]
        [HttpPost]
        public async Task<IHttpActionResult> GetBrands()
        {
            try
            {
                var brandDomain = new BrandDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await brandDomain.GetAllAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }


        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("brands/after")]
        [HttpPost]
        public async Task<IHttpActionResult> GetBrands([FromBody]BaseRequestModel data)
        {
            try
            {
                var brandDomain = new BrandDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = DateTime.Parse(data.dateAfter);
                var result = await brandDomain.GetAllChangedAfterAsync(validDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveBrands([FromBody]GeneralRequestModel data)
        {
            var brandDomain = new BrandDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
            await brandDomain.PublishAsync(new BrandProxy().ReverseConvert(data.brandData));
            return Ok(data.brandData);
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("checkdeleted")]
        [HttpPost]
        public async Task<IHttpActionResult> CheckDeletedBrands([FromBody]GeneralRequestModel data)
        {
            var brandDomain = new BrandDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
            await brandDomain.CheckDeletedAsync(data.brandData);
            return Ok(data.brandData);
        }
        #endregion
    }
}
