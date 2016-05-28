using Anatoli.Business;
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
    [RoutePrefix("api/gateway/productrate")]
    public class ProductRateController : AnatoliApiController
    {
        #region ProductRates
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("productrates")]
        [HttpPost]
        public async Task<IHttpActionResult> GetProductRates()
        {
            try
            {
                var productRateDomain = new ProductRateDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await productRateDomain.GetAllAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("productrateavgs")]
        [HttpPost]
        public async Task<IHttpActionResult> GetProductRateAvgs()
        {
            try
            {
                var productRateDomain = new ProductRateDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await productRateDomain.GetAllAvg();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("productrateavgs/after")]
        [HttpPost]
        public async Task<IHttpActionResult> GetProductRates([FromBody] BaseRequestModel data)
        {
            try
            {
                var productRateDomain = new ProductRateDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = GetDateFromString(data.dateAfter);
                var result = await productRateDomain.GetAllAvgChangeAfter(validDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveProductRates([FromBody] ProductRequestModel data)
        {
            try
            {
                if (data != null) log.Info("save product count : " + data.productRateData.Count);
                var productRateDomain = new ProductRateDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await productRateDomain.PublishAsyncWithResult(new ProductRateProxy().ReverseConvert(data.productRateData));
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
