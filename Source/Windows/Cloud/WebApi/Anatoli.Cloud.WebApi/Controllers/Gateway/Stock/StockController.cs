using Anatoli.Business;
using Anatoli.Business.Domain;
using Anatoli.ViewModels.StoreModels;
using Anatoli.PMC.Business.Domain.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Anatoli.ViewModels.StockModels;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/stock")]
    public class StockController : ApiController
    {
        public class RequestModel
        {
            public string privateOwnerId { get; set; }
            public string stockId { get; set; }
        }

        #region stock On Hand List
        [Authorize(Roles = "AuthorizedApp")]
        [Route("stockOnhand")]
        public async Task<IHttpActionResult> GetStockOnhands(string privateOwnerId)
        {
            var owner = Guid.Parse(privateOwnerId);
            var stockDomain = new StockActiveOnHandDomain(owner);
            var result = await stockDomain.GetAll();
            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("stockOnhand/after")]
        public async Task<IHttpActionResult> GetStockOnhands(string privateOwnerId, string dateAfter)
        {
            var owner = Guid.Parse(privateOwnerId);
            var stockDomain = new StockActiveOnHandDomain(owner);
            var validDate = DateTime.Parse(dateAfter);
            var result = await stockDomain.GetAllChangedAfter(validDate);
            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("stockOnhandbyid/")]
        public async Task<IHttpActionResult> GetStockOnhandsByStockId(string privateOwnerId, string id)
        {
            var owner = Guid.Parse(privateOwnerId);
            var stockDomain = new StockActiveOnHandDomain(owner);
            var result = await stockDomain.GetAllByStockId(id);
            return Ok(result);
        }

        //[Authorize(Roles = "AuthorizedApp")]
        //[Route("stockOnhandbyid/online")]
        //public async Task<IHttpActionResult> GetStockOnlineOnhandsByStockId(string privateOwnerId, string id)
        //{
        //    var owner = Guid.Parse(privateOwnerId);
        //    var stockDomain = new StoreActiveOnhandDomain(owner);
        //    var result = await stockDomain.GetAllByStockIdOnLine(id);
        //    result.ForEach(item =>
        //        {
        //            item.PrivateOwnerId = owner;
        //        });
        //    return Ok(result);
        //}

        //[Authorize(Roles = "AuthorizedApp")]
        //[Route("stockOnhandbyid/local")]
        //public async Task<IHttpActionResult> GetStockLocalOnhandsByStockId(string id)
        //{
        //    var result = new List<StoreActiveOnhandViewModel>();
        //    await Task.Factory.StartNew(() =>
        //    {
        //        var stockDomain = new PMCStoreOnHandDomain();
        //        result = stockDomain.GetAllByStockId(id);
        //    });
        //    return Ok(result);
        //}

        //[Authorize(Roles = "AuthorizedApp")]
        //[Route("stockOnhandbyid/after/")]
        //public async Task<IHttpActionResult> GetStockOnhandsAfter(string privateOwnerId, string id, string dateAfter)
        //{
        //    var owner = Guid.Parse(privateOwnerId);
        //    var stockDomain = new StoreActiveOnhandDomain(owner);
        //    var validDate = DateTime.Parse(dateAfter);
        //    var result = await stockDomain.GetAllByStockIdChangedAfter(id, validDate);
        //    return Ok(result);
        //}

        [Authorize(Roles = "AuthorizedApp")]
        [Route("stockOnhand/save")]
        public async Task<IHttpActionResult> SaveStockOnhands(string privateOwnerId, List<StoreActiveOnhandViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var stockDomain = new StoreActiveOnhandDomain(owner);
            await stockDomain.PublishAsync(data);
            return Ok();
        }
        #endregion

        #region Store List
        [Authorize(Roles = "AuthorizedApp")]
        [Route("stocks")]
        public async Task<IHttpActionResult> GetStocks(string privateOwnerId)
        {
            var owner = Guid.Parse(privateOwnerId);
            var stockDomain = new StockDomain(owner);
            var result = await stockDomain.GetAll();
            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("stocks/after")]
        public async Task<IHttpActionResult> GetStocks(string privateOwnerId, string dateAfter)
        {
            var owner = Guid.Parse(privateOwnerId);
            var stockDomain = new StockDomain(owner);
            var validDate = DateTime.Parse(dateAfter);
            var result = await stockDomain.GetAllChangedAfter(validDate);
            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("save")]
        public async Task<IHttpActionResult> SaveStocks(string privateOwnerId, List<StockViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var stockDomain = new StockDomain(owner);
            await stockDomain.PublishAsync(data);
            return Ok();
        }
        #endregion

        #region Stock Product List
        [Authorize(Roles = "AuthorizedApp")]
        [Route("stockproduct")]
        public async Task<IHttpActionResult> GetStockProducts(string privateOwnerId)
        {
            var owner = Guid.Parse(privateOwnerId);
            var stockDomain = new StockProductDomain(owner);
            var result = await stockDomain.GetAll();
            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("stockproduct/stockid")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStockProductsByStockId([FromBody] RequestModel data)
        {
            var owner = Guid.Parse(data.privateOwnerId);
            var stockDomain = new StockProductDomain(owner);
            var result = await stockDomain.GetAllByStockId(data.stockId);
            return Ok(result);
        }


        [Authorize(Roles = "AuthorizedApp")]
        [Route("stockproduct/after")]
        public async Task<IHttpActionResult> GetStockProducts(string privateOwnerId, string dateAfter)
        {
            var owner = Guid.Parse(privateOwnerId);
            var stockDomain = new StockProductDomain(owner);
            var validDate = DateTime.Parse(dateAfter);
            var result = await stockDomain.GetAllChangedAfter(validDate);
            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("stockproduct/save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveStockProducts(string privateOwnerId, List<StockProductViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var stockDomain = new StockProductDomain(owner);
            await stockDomain.PublishAsync(data);
            return Ok(data);
        }
        #endregion
    }
}
