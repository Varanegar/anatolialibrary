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
using Anatoli.ViewModels;
using Anatoli.Business.Proxy.Concretes.StockProductRequestRuleConcretes;
using Anatoli.Business.Proxy.Concretes.StockProductRequestTypeConcretes;
using Anatoli.Business.Proxy.Concretes.StockProductRequestConcretes;
using Anatoli.Cloud.WebApi.Classes.Helpers;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/stockproductrequest")]
    public class StockProductRequestController : AnatoliApiController
    {
        #region Stock Product Request Rules List
        [Authorize(Roles = "AuthorizedApp,User")]
        [Route("rules")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStockProductRequestRules([FromBody] BaseRequestModel data)
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

        [Authorize(Roles = "AuthorizedApp,User")]
        [Route("rules/valid")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStockProductRequestValidRules([FromBody] StockProductRequestRequestModel data)
        {
            try
            {
                var stockDomain = new StockProductRequestRuleDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = DateTime.Now;
                if (data.ruleDate != "" && data.ruleDate != null)
                    validDate = GetDateFromString(data.ruleDate);

                var result = await stockDomain.GetAllValidAsync(validDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp,User")]
        [Route("rules/after")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStockProductRequestRulesAfter([FromBody] StockProductRequestRequestModel data)
        {
            try
            {
                var stockDomain = new StockProductRequestRuleDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = DateTime.Now;
                if (data.dateAfter != "" && data.dateAfter != null)
                    validDate = GetDateFromString(data.dateAfter);

                var result = await stockDomain.GetAllChangedAfterAsync(validDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp,User")]
        [Route("rules/save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveStockProductRequestRules(string ApplicationOwnerId, List<StockProductRequestRuleViewModel> data)
        {
            try
            {
                var stockDomain = new StockProductRequestRuleDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await stockDomain.PublishAsync(new StockProductRequestRuleProxy().ReverseConvert(data));
                return Ok(data);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [AnatoliAuthorize(Roles = "AuthorizedApp,User", Resource = "Stock", Action = "SaveStockProductRequestRule")]
        [Route("saveStockRequestProduct"), HttpPost]
        public async Task<IHttpActionResult> SaveStockProductRequestRule([FromBody] StockProductRequestRequestModel data)
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
                    ApplicationOwnerId = OwnerKey
                };

                if (tmp.ruleId != "")
                    model.UniqueId = tmp.ruleId;

                await new StockProductRequestRuleDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).PublishAsync(new StockProductRequestRuleProxy().ReverseConvert(new List<StockProductRequestRuleViewModel> { model }));

                return Ok();
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        [AnatoliAuthorize(Roles = "AuthorizedApp,User", Resource = "Stock", Action = "StockProductRequestRuleType")]
        [Route("stockProductRequestRuleTypes"), HttpPost]
        public async Task<IHttpActionResult> GetStockProductRequestRuleTypes([FromBody] BaseRequestModel data)
        {
            try
            {
                var result = await new StockProductRequestRuleDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetAllStockProductRequestRuleTypes();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        [AnatoliAuthorize(Roles = "AuthorizedApp,User", Resource = "Stock", Action = "StockProductRequestRuleCalcType")]
        [Route("stockProductRequestRuleCalcTypes"), HttpPost]
        public async Task<IHttpActionResult> GetStockProductRequestRuleCalcTypes([FromBody] BaseRequestModel data)
        {
            try
            {
                var result = await new StockProductRequestRuleDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetAllStockProductRequestRuleCalcTypes();

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
        [Authorize(Roles = "AuthorizedApp,User")]
        [Route("requesttypes")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStockProductRequestTypes([FromBody] BaseRequestModel data)
        {
            try
            {
                var stockDomain = new StockProductRequestTypeDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await stockDomain.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("requesttypes/save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveStockProductRequestTypes([FromBody] StockProductRequestRequestModel data)
        {
            try
            {
                var stockDomain = new StockProductRequestTypeDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await stockDomain.PublishAsync(new StockProductRequestTypeProxy().ReverseConvert(data.stockProductRequestTypeData));
                return Ok(data.stockProductRequestTypeData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        #endregion

        #region Stock Product Request
        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveStockProductRequests([FromBody] StockProductRequestRequestModel data)
        {
            try
            {
                var stockDomain = new StockProductRequestDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await stockDomain.PublishAsync(new StockProductRequestProxy().ReverseConvert(data.stockProductRequestData));
                return Ok(data.stockProductRequestData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        #endregion

        [AnatoliAuthorize(Roles = "AuthorizedApp,User", Resource = "Stock", Action = "StockProductRequestHistory")]
        [Route("history"), HttpPost]
        public async Task<IHttpActionResult> StockProductRequestHistory([FromBody] StockProductRequestRequestModel data)
        {
            try
            {
                Guid stockId;
                Guid.TryParse(data.stockId, out stockId);

                var model = await new StockProductRequestDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetHistory(stockId);

                return Ok(model);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);

                return GetErrorResult(ex);
            }
        }

        [AnatoliAuthorize(Roles = "AuthorizedApp,User", Resource = "Stock", Action = "StockProductRequestDetailsHistory")]
        [Route("detailshistory"), HttpPost]
        public async Task<IHttpActionResult> StockProductRequestDetailsHistory([FromBody] StockProductRequestRequestModel data)
        {
            try
            {
                var model = await new StockProductRequestProductDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetDetailsHistory(data.stockProductRequestId);

                return Ok(model);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);

                return GetErrorResult(ex);
            }
        }

        [AnatoliAuthorize(Roles = "AuthorizedApp,User", Resource = "Stock", Action = "StockProductRequests")]
        [Route("requests"), HttpPost]
        public async Task<IHttpActionResult> GetStockProductRequests([FromBody] BaseRequestModel data)
        {
            try
            {
                var term = data != null ? data.searchTerm : string.Empty;

                var currentUserId = User.GetAnatoliUserId();

                var model = await new StockProductRequestDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetStockProductRequests(term, Guid.Parse(currentUserId));

                return Ok(model);
            }
            catch (Exception ex)
            {
                log.Error(ex, "Web API Call Error");

                return GetErrorResult(ex);
            }
        }

        [AnatoliAuthorize(Roles = "AuthorizedApp,User", Resource = "Stock", Action = "requestProductDetails")]
        [Route("requestProductDetails"), HttpPost]
        public async Task<IHttpActionResult> GetStockProductRequestProductDetails([FromBody] StockProductRequestRequestModel data)
        {
            try
            {
                var model = await new StockProductRequestProductDetailDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetStockProductRequestProductDetails(data.stockProductRequestProductId);

                return Ok(model);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);

                return GetErrorResult(ex);
            }
        }

        [AnatoliAuthorize(Roles = "AuthorizedApp,User", Resource = "Stock", Action = "updateRequestProductDetails")]
        [Route("updateRequestProductDetails"), HttpPost]
        public async Task<IHttpActionResult> UpdateStockProductRequestProductDetails([FromBody] StockProductRequestRequestModel data)
        {
            try
            {
                var currentUserId = User.GetAnatoliUserId();

                await new StockProductRequestProductDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).UpdateStockProductRequestProductDetails(data.stockProductRequestProductData, Guid.Parse(data.stockId), Guid.Parse(currentUserId));

                return Ok(data.stockProductRequestProductData);
            }
            catch (Exception ex)
            {
                log.Error(ex, "Web API Call Error");

                return GetErrorResult(ex);
            }
        }
    }
}
