using System;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using Anatoli.Business.Domain;
using System.Collections.Generic;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.ViewModels.StoreModels;
using Anatoli.ViewModels.StockModels;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/stock")]
    public class StockController : AnatoliApiController
    {
        //public class RequestModel
        //{
        //    public string privateOwnerId { get; set; }
        //    public string stockId { get; set; }
        //    public string userId { get; set; }
        //    public string dateAfter { get; set; }
        //}

        #region stock On Hand List
        [Authorize(Roles = "AuthorizedApp")]
        [Route("stockOnhand")]
        public async Task<IHttpActionResult> GetStockOnhands(string privateOwnerId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var stockDomain = new StockActiveOnHandDomain(owner);
                var result = await stockDomain.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("stockOnhand/after")]
        public async Task<IHttpActionResult> GetStockOnhands(string privateOwnerId, string dateAfter)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var stockDomain = new StockActiveOnHandDomain(owner);
                var validDate = DateTime.Parse(dateAfter);
                var result = await stockDomain.GetAllChangedAfter(validDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("stockOnhandbyid/")]
        public async Task<IHttpActionResult> GetStockOnhandsByStockId(string privateOwnerId, string id)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var stockDomain = new StockActiveOnHandDomain(owner);
                var result = await stockDomain.GetAllByStockId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("stockOnhand/save")]
        public async Task<IHttpActionResult> SaveStockOnhands(string privateOwnerId, List<StoreActiveOnhandViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var stockDomain = new StoreActiveOnhandDomain(owner);
                var result = await stockDomain.PublishAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        #endregion

        #region Stock List
        [Authorize(Roles = "AuthorizedApp")]
        [Route("stocks")]
        public async Task<IHttpActionResult> GetStocks(string privateOwnerId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var stockDomain = new StockDomain(owner);
                var result = await stockDomain.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [AnatoliAuthorize(Roles = "AuthorizedApp", Resource = "Stock", Action = "UserStocks"),
         Route("userStocks"), HttpPost]
        public async Task<IHttpActionResult> GetUserStocks([FromBody] RequestModel data)
        {
            try
            {
                var result = await new StockDomain(OwnerKey).GetAllByUserId(data.userId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        [AnatoliAuthorize(Roles = "AuthorizedApp", Resource = "Stock", Action = "SaveUserStocks"),
         Route("saveUserStocks"), HttpPost]
        public async Task<IHttpActionResult> SaveUserStocks([FromBody] RequestModel data)
        {
            try
            {
                await new StockDomain(OwnerKey).SaveStocksUser(data.userId, data.stockIds == null ? null : data.stockIds.Select(s => Guid.Parse(s)).ToList());

                return Ok(new { });
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Route("stocks/complete")]
        public async Task<IHttpActionResult> GetStockCompletes(string privateOwnerId, string stockId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var stockDomain = new StockDomain(owner);
                var result = await stockDomain.GetStockCompleteInfo(stockId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("stocks/after")]
        public async Task<IHttpActionResult> GetStocks(string privateOwnerId, string dateAfter)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var stockDomain = new StockDomain(owner);
                var validDate = DateTime.Parse(dateAfter);
                var result = await stockDomain.GetAllChangedAfter(validDate);
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
        public async Task<IHttpActionResult> SaveStocks(string privateOwnerId, List<StockViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var stockDomain = new StockDomain(owner);
                var result = await stockDomain.PublishAsync(data);
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
        [Authorize(Roles = "AuthorizedApp")]
        [Route("stockproduct")]
        public async Task<IHttpActionResult> GetStockProducts(string privateOwnerId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var stockDomain = new StockProductDomain(owner);
                var result = await stockDomain.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("stockproduct/stockid")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStockProductsByStockId([FromBody] RequestModel data)
        {
            try
            {
                var owner = Guid.Parse(data.privateOwnerId);
                var stockDomain = new StockProductDomain(owner);
                var result = await stockDomain.GetAllByStockId(data.stockId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }


        [Authorize(Roles = "AuthorizedApp")]
        [Route("stockproduct/after")]
        public async Task<IHttpActionResult> GetStockProducts([FromBody] RequestModel data)
        {
            try
            {
                var owner = Guid.Parse(data.privateOwnerId);
                var stockDomain = new StockProductDomain(owner);
                var validDate = DateTime.Parse(data.dateAfter);
                var result = await stockDomain.GetAllChangedAfter(validDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("stockproduct/save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveStockProducts(string privateOwnerId, List<StockProductViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var stockDomain = new StockProductDomain(owner);
                await stockDomain.PublishAsync(data);
                return Ok(data);
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
