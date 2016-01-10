using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.StockModels;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;
using VNAppServer.Anatoli.Common;

namespace VNAppServer.Anatoli.SCM.Scheduler
{
    public class CalcSCMProcess
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public async Task CalcSCMAsync(IJobExecutionContext context, HttpClient client, TokenResponse oauthResult)
        {
            await Task.Factory.StartNew(() =>
                {
                    Parallel.Invoke(() =>
                        {

                        },
                        () =>
                        { }
                        );
                });
        }
        public List<StockProductRequestViewModel> CalcSCM(HttpClient client, TokenResponse oauthResult, Guid stockRequestTypeId, string serverUri, string queryString, string privateOwnerId, string stockId = "")
        {
            try
            {
                var requestData = new ConnectionHelperRequestModel();
                requestData.privateOwnerId = privateOwnerId;
                var result = new List<StockProductRequestViewModel>();

                var rules = ConnectionHelper.CallServerServicePost<List<StockProductRequestRuleViewModel>>(requestData, serverUri + UriInfo.GetProductRequestRulesURI, client);
                var products = ConnectionHelper.CallServerServiceGet<List<ProductViewModel>>(null, serverUri + UriInfo.GetProductsURI + queryString, client);
                var mainProductGroups = ConnectionHelper.CallServerServiceGet<List<MainProductGroupViewModel>>(null, serverUri + UriInfo.GetMainProductGroupsURI + queryString, client);

                List<StockViewModel> stockList = ConnectionHelper.CallServerServiceGet<List<StockViewModel>>(null, serverUri + UriInfo.GetStocksURI + queryString, client);
                foreach (StockViewModel item in stockList)
                {
                    var stockComplete = ConnectionHelper.CallServerServiceGet<List<StockViewModel>>(null, serverUri + UriInfo.GetStocksCompleteURI + queryString + "&stockId=" + item.UniqueId, client).FirstOrDefault();
                    result.AddRange(CalcStockProductRequest(stockComplete, rules, products, mainProductGroups, stockRequestTypeId).Result);
                }

                return result;
            }
            catch(Exception ex)
            {
                log.Error("Calc failed", ex);
                throw ex;

            }

        }

        private async Task<List<StockProductRequestViewModel>> CalcStockProductRequest(StockViewModel stockData, List<StockProductRequestRuleViewModel> rules,
                        List<ProductViewModel> products, List<MainProductGroupViewModel> productGroups, Guid stockRequestTypeId)
        {
            var result = new List<StockProductRequestViewModel>();
            try
            {
                var productRequestFlatList = new List<StockProductRequestProductViewModel>();
                var helper = new CalcSCMProductExtractHelper(stockData, rules, products, productGroups, stockRequestTypeId);
                await helper.ExtractProducts();
                await helper.GenerateStockRequests();
                result.AddRange(helper.StockRequests);

            }
            catch (Exception ex)
            {
                log.Error("Calc stock product request failed", ex);
                throw ex;
            }
            return result;

        }

        public void UploadDataToServer(HttpClient client, string serverURI, string privateOwnerQueryString, List<StockProductRequestViewModel> dbData)
        {
            try
            {
                log.Info("Start CallServerService URI ");
                var currentTime = DateTime.Now;
                string data = JsonConvert.SerializeObject(dbData);
                string URI = serverURI + UriInfo.SaveStockProductRequestURI + privateOwnerQueryString;
                var result = ConnectionHelper.CallServerServicePost(data, URI, client);

                log.Info("Completed CallServerService ");
            }
            catch (Exception ex)
            {
                log.Error("Failed CallServerService ", ex);
            }
        }
    }
}
