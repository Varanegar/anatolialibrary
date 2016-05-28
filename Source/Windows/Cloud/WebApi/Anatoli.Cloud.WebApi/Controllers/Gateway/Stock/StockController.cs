using System;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using Anatoli.Business.Domain;
using System.Collections.Generic;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.ViewModels.StoreModels;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels;
using Anatoli.Business.Proxy.Concretes.StockActiveOnHandConcretes;
using Anatoli.Business.Proxy.Concretes.StockConcretes;
using Anatoli.Business.Proxy.Concretes.StockProductConcretes;
using Anatoli.Business.Proxy.Concretes.StockProductRequestRuleConcretes;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/stock")]
    public class StockController : AnatoliApiController
    {
        #region stock On Hand List
        [Authorize(Roles = "AuthorizedApp,User")]
        [Route("stockOnhand")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStockOnhands()
        {
            try
            {
                var stockDomain = new StockActiveOnHandDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await stockDomain.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp,User")]
        [Route("stockOnhand/after")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStockOnhands([FromBody] StockRequestModel data)
        {
            try
            {
                var stockDomain = new StockActiveOnHandDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = GetDateFromString(data.dateAfter);
                var result = await stockDomain.GetAllChangedAfterAsync(validDate);
                return Ok(validDate);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp,User")]
        [Route("stockOnhandbyid/")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStockOnhandsByStockId([FromBody] StockRequestModel data)
        {
            try
            {
                var stockDomain = new StockActiveOnHandDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await stockDomain.GetAllByStockId(data.stockId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp,User"), HttpPost]
        [Route("stockOnhand/save")]
        public async Task<IHttpActionResult> SaveStockOnhands([FromBody] StockRequestModel data)
        {
            try
            {
                var stockDomain = new StockActiveOnHandDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await stockDomain.PublishAsync(new StockActiveOnHandProxy().ReverseConvert(data.stockActiveOnHandData));
                return Ok(data.stockActiveOnHandData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error info", ex);
                return GetErrorResult(ex);
            }
        }
        #endregion

        #region Stock List
        [AnatoliAuthorize(Roles = "AuthorizedApp,User")] //, Resource = "Stock", Action = "List"
        [Route("stocks"), HttpPost]
        public async Task<IHttpActionResult> GetStocks()
        {
            try
            {
                var stockDomain = new StockDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);

                var result = await stockDomain.GetAllAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [AnatoliAuthorize(Roles = "AuthorizedApp, User, DataSync, BaseDataAdmin"),
         Route("save"), HttpPost] //, Resource = "Stock", Action = "SaveStocks"
        public async Task<IHttpActionResult> SaveStocks([FromBody] StockRequestModel data)
        {
            try
            {
                await new StockDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).PublishAsync(new StockProxy().ReverseConvert(data.stockData));

                return Ok(data.stockData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        
        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("checkdeleted")]
        [HttpPost]
        public async Task<IHttpActionResult> CheckDeletedStocks([FromBody]StockRequestModel data)
        {
            var businessDomain = new StockDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
            await businessDomain.CheckDeletedAsync(data.stockData);
            return Ok(data.stockData);
        }


        [AnatoliAuthorize(Roles = "AuthorizedApp,User", Resource = "Stock", Action = "UserStocks"),
         Route("userStocks"), HttpPost]
        public async Task<IHttpActionResult> GetUserStocks([FromBody] BaseRequestModel data)
        {
            try
            {
                var result = await new StockDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetAllByUserId(Guid.Parse(data.userId));

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [AnatoliAuthorize(Roles = "AuthorizedApp,User", Resource = "Stock", Action = "SaveUserStocks"),
         Route("saveUserStocks"), HttpPost]
        public async Task<IHttpActionResult> SaveUserStocks([FromBody] StockRequestModel data)
        {
            try
            {
                //await new StockDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).SaveStocksUser(Guid.Parse(data.userId), data.stockIds == null ? null : data.stockIds.Select(s => Guid.Parse(s)).ToList());

                //return Ok(new { });
                return BadRequest();
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [AnatoliAuthorize(Roles = "AuthorizedApp")]
        [Route("stocks/complete")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStockCompletes([FromBody] StockRequestModel data)
        {
            try
            {
                var stockDomain = new StockDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await stockDomain.GetByIdAsync(data.stockId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp,User")]
        [Route("stocks/after")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStocks([FromBody] StockRequestModel data)
        {
            try
            {
                var stockDomain = new StockDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = GetDateFromString(data.dateAfter);
                var result = await stockDomain.GetAllChangedAfterAsync(validDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        #endregion

        #region Stock Product List
        [Authorize(Roles = "AuthorizedApp,User")]
        [Route("stockproduct")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStockProducts()
        {
            try
            {
                var stockDomain = new StockProductDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await stockDomain.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp,User")]
        [Route("stockproduct/stockid")]
        [GzipCompression]
        [HttpPost]
        public async Task<IHttpActionResult> GetStockProductsByStockId([FromBody] StockRequestModel data)
        {
            try
            {
                var result = await new StockProductDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetAllByStockId(data.stockId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }


        [Authorize(Roles = "AuthorizedApp,User")]
        [Route("stockproduct/after")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStockProducts([FromBody] BaseRequestModel data)
        {
            try
            {
                var stockDomain = new StockProductDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = GetDateFromString(data.dateAfter);
                var result = await stockDomain.GetAllChangedAfterAsync(validDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("stockproduct/save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveStockProducts([FromBody] StockRequestModel data)
        {
            try
            {
                var stockDomain = new StockProductDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);

                await stockDomain.PublishAsync(new StockProductProxy().ReverseConvert(data.stockProductData));

                return Ok(new { });
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        #endregion

        #region Product Rules
        [AnatoliAuthorize(Roles = "AuthorizedApp,User", Resource = "Stock", Action = "ProductRequestRules")]
        [Route("productRequestRules"), HttpPost]
        public async Task<IHttpActionResult> GetProductRequestRules()
        {
            try
            {
                var stockDomain = new StockProductRequestRuleDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);

                var result = await stockDomain.GetAllAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        [AnatoliAuthorize(Roles = "AuthorizedApp,User", Resource = "Stock", Action = "ProductRequestRules")]
        [Route("productRequestRule"), HttpPost]
        public async Task<IHttpActionResult> GetProductRequestRule([FromBody] StockProductRequestRequestModel data)
        {
            try
            {
                var result = await new StockProductRequestRuleDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetByIdAsync(data.ruleId);

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
