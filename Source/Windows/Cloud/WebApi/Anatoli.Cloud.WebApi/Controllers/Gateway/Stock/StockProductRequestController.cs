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
    [RoutePrefix("api/gateway/stockproductrequest")]
    public class StockProductRequestController : BaseApiController
    {
        public class RequestModel
        {
            public string privateOwnerId { get; set; }
            public string stockId { get; set; }
            public string dateAfter { get; set; }
            public string ruleDate { get; set; }
        }

        #region Stock Product Request Rules List
        [Authorize(Roles = "AuthorizedApp")]
        [Route("rules")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStockProductRequestRules([FromBody] RequestModel data)
        {
            try
            {
                var owner = Guid.Parse(data.privateOwnerId);
                var stockDomain = new StockProductRequestRuleDomain(owner);
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
        [Route("rules/valid")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStockProductRequestValidRules([FromBody] RequestModel data)
        {
            try
            {
                var owner = Guid.Parse(data.privateOwnerId);
                var stockDomain = new StockProductRequestRuleDomain(owner);
                var validDate = DateTime.Now;
                if (data.ruleDate != "" && data.ruleDate != null)
                    validDate = DateTime.Parse(data.ruleDate);

                var result = await stockDomain.GetAll(validDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("rules/after")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStockProductRequestRulesAfter([FromBody] RequestModel data)
        {
            try
            {
                var owner = Guid.Parse(data.privateOwnerId);
                var stockDomain = new StockProductRequestRuleDomain(owner);
                var validDate = DateTime.Now;
                if (data.dateAfter != "" && data.dateAfter != null)
                    validDate = DateTime.Parse(data.dateAfter);

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
        [Route("rules/save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveStockProductRequestRules(string privateOwnerId, List<StockProductRequestRuleViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var stockDomain = new StockProductRequestRuleDomain(owner);
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

        #region Stock Product Request Rules List
        [Authorize(Roles = "AuthorizedApp")]
        [Route("requesttypes")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStockProductRequestTypes([FromBody] RequestModel data)
        {
            try
            {
                var owner = Guid.Parse(data.privateOwnerId);
                var stockDomain = new StockProductRequestTypeDomain(owner);
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
        [Route("requesttypes/save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveStockProductRequestTypes(string privateOwnerId, List<StockProductRequestTypeViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var stockDomain = new StockProductRequestTypeDomain(owner);
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

        #region Stock Product Request
        [Authorize(Roles = "AuthorizedApp")]
        [Route("stockproductrequest/save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveStockProductRequests(string privateOwnerId, List<StockProductRequestViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var stockDomain = new StockProductRequestDomain(owner);
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
    }
}
