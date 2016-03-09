using Anatoli.Business.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Anatoli.ViewModels.StockModels;
using Anatoli.Cloud.WebApi.Classes;
using Newtonsoft.Json;
using PersianDate;
using System.Web;
using Microsoft.AspNet.Identity;
using Anatoli.ViewModels;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/stockproductrequest")]
    public class StockProductRequestController : AnatoliApiController
    {
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

        [AnatoliAuthorize(Roles = "AuthorizedApp", Resource = "Stock", Action = "SaveStockProductRequestRule")]
        [Route("saveStockRequestProduct"), HttpPost]
        public async Task<IHttpActionResult> SaveStockProductRequestRule([FromBody] RequestModel data)
        {
            try
            {
                dynamic tmp = JsonConvert.DeserializeObject(data.stockRequestProduct);

                var model = new StockProductRequestRuleViewModel
                {
                    UniqueId = Guid.NewGuid(),
                    StockProductRequestRuleName = tmp.ruleName,
                    FromPDate = tmp.fromDate,
                    ToPDate = tmp.toDate,
                    FromDate = ConvertDate.ToEn(tmp.fromDate.ToString()),
                    ToDate = ConvertDate.ToEn(tmp.toDate.ToString()),
                    Qty = tmp.quantity,
                    ProductTypeId = tmp.productType,
                    ReorderCalcTypeId = tmp.reOrderCalcType,
                    RuleTypeId = tmp.stockProductRequestRuleType,
                    RuleCalcTypeId = tmp.stockProductRequestRuleCalcType,
                    SupplierId = tmp.supplierId,
                    MainProductGroupId = tmp.mainProductGroupId,
                    ProductId = tmp.productId,
                    PrivateOwnerId = OwnerKey
                };

                if (tmp.ruleId != "")
                    model.UniqueId = tmp.ruleId;

                await new StockProductRequestRuleDomain(OwnerKey).PublishAsync(new List<StockProductRequestRuleViewModel> { model });

                return Ok();
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        [AnatoliAuthorize(Roles = "AuthorizedApp", Resource = "Stock", Action = "StockProductRequestRuleType")]
        [Route("stockProductRequestRuleTypes"), HttpPost]
        public async Task<IHttpActionResult> GetStockProductRequestRuleTypes([FromBody] RequestModel data)
        {
            try
            {
                var result = await new StockProductRequestRuleDomain(OwnerKey).GetAllStockProductRequestRuleTypes();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        [AnatoliAuthorize(Roles = "AuthorizedApp", Resource = "Stock", Action = "StockProductRequestRuleCalcType")]
        [Route("stockProductRequestRuleCalcTypes"), HttpPost]
        public async Task<IHttpActionResult> GetStockProductRequestRuleCalcTypes([FromBody] RequestModel data)
        {
            try
            {
                var result = await new StockProductRequestRuleDomain(OwnerKey).GetAllStockProductRequestRuleCalcTypes();

                return Ok(result);
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
        [Route("save")]
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

        [AnatoliAuthorize(Roles = "AuthorizedApp", Resource = "Stock", Action = "StockProductRequestHistory")]
        [Route("history"), HttpPost]
        public async Task<IHttpActionResult> StockProductRequestHistory([FromBody] RequestModel data)
        {
            try
            {
                Guid stockId;
                Guid.TryParse(data.stockId, out stockId);

                var model = await new StockProductRequestDomain(OwnerKey).GetHistory(stockId);

                return Ok(model);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);

                return GetErrorResult(ex);
            }
        }

        [AnatoliAuthorize(Roles = "AuthorizedApp", Resource = "Stock", Action = "StockProductRequestDetailsHistory")]
        [Route("detailshistory"), HttpPost]
        public async Task<IHttpActionResult> StockProductRequestDetailsHistory([FromBody] RequestModel data)
        {
            try
            {
                var model = await new StockProductRequestDomain(OwnerKey).GetDetailsHistory(data.stockProductRequestId);

                return Ok(model);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);

                return GetErrorResult(ex);
            }
        }

        [AnatoliAuthorize(Roles = "AuthorizedApp", Resource = "Stock", Action = "StockProductRequests")]
        [Route("requests"), HttpPost]
        public async Task<IHttpActionResult> GetStockProductRequests([FromBody] RequestModel data)
        {
            try
            {
                var term = data != null ? data.searchTerm : string.Empty;
                var currentUserId = Guid.Parse(HttpContext.Current.User.Identity.GetUserId());

                var model = await new StockProductRequestDomain(OwnerKey).GetStockProductRequests(term, currentUserId);

                return Ok(model);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);

                return GetErrorResult(ex);
            }
        }

        [AnatoliAuthorize(Roles = "AuthorizedApp", Resource = "Stock", Action = "requestProductDetails")]
        [Route("requestProductDetails"), HttpPost]
        public async Task<IHttpActionResult> GetStockProductRequestProductDetails([FromBody] RequestModel data)
        {
            try
            {
                var model = await new StockProductRequestDomain(OwnerKey).GetStockProductRequestProductDetails(data.stockProductRequestProductId);

                return Ok(model);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);

                return GetErrorResult(ex);
            }
        }

        [AnatoliAuthorize(Roles = "AuthorizedApp", Resource = "Stock", Action = "updateRequestProductDetails")]
        [Route("updateRequestProductDetails"), HttpPost]
        public async Task<IHttpActionResult> UpdateStockProductRequestProductDetails([FromBody] RequestModel data)
        {
            try
            {
                var currentUserId = Guid.Parse(HttpContext.Current.User.Identity.GetUserId());

                await new StockProductRequestDomain(OwnerKey).UpdateStockProductRequestProductDetails(data.StockProductRequestProductList, Guid.Parse(data.stockId), currentUserId);

                return Ok(data.StockProductRequestProductList);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);

                return GetErrorResult(ex);
            }
        }
    }
}
