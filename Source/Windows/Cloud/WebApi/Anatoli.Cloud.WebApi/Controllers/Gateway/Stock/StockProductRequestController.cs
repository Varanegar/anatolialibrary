using Anatoli.Business.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Anatoli.ViewModels.StockModels;
using Anatoli.Cloud.WebApi.Classes;
using Newtonsoft.Json;
using System.Globalization;
using PersianDate;

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
