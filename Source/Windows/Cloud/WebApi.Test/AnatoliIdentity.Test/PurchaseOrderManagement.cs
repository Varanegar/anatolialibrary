
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.Order;
using Anatoli.ViewModels.ProductModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Thunderstruck;

namespace ClientApp
{
    public class PurchaseOrderManagement
    {
        public static List<PurchaseOrderViewModel> GetCustomerSellInfoFromServer(HttpClient client, string servserURI)
        {
            //F125EDC7-473D-4C59-B966-3EF9E6E6A7D9
            //var result8 = client.GetAsync(servserURI + "/api/gateway/purchaseorder/bycustomerid/local?customerId=2BFFBDF6-9362-4CB6-807B-37D2DFBEBB96&privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result;
            var result8 = client.GetAsync(servserURI + "/api/gateway/purchaseorder/bycustomerid/local?customerId=2BFFBDF6-9362-4CB6-807B-37D2DFBEBB96&centerid=A7B07040-707A-4D51-9703-BB6710EBADE7").Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj = new List<PurchaseOrderViewModel>();
            var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            return x;
        }

        public static List<PurchaseOrderLineItemViewModel> GetCustomerSellDetailInfoFromServer(HttpClient client, string servserURI)
        {
            //F125EDC7-473D-4C59-B966-3EF9E6E6A7D9
            //var result8 = client.GetAsync(servserURI + "/api/gateway/purchaseorder/bycustomerid/local?customerId=2BFFBDF6-9362-4CB6-807B-37D2DFBEBB96&privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result;
            var result8 = client.GetAsync(servserURI + "/api/gateway/purchaseorder/lineitem/local?poId=ECE4C694-B392-4ED2-A022-33018A502CDA&centerid=A7B07040-707A-4D51-9703-BB6710EBADE7").Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj = new List<PurchaseOrderLineItemViewModel>();
            var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            return x;
        }

        public static List<PurchaseOrderStatusHistoryViewModel> GetCustomerSellHistoryInfoFromServer(HttpClient client, string servserURI)
        {
            //F125EDC7-473D-4C59-B966-3EF9E6E6A7D9
            //var result8 = client.GetAsync(servserURI + "/api/gateway/purchaseorder/bycustomerid/local?customerId=2BFFBDF6-9362-4CB6-807B-37D2DFBEBB96&privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result;
            var result8 = client.GetAsync(servserURI + "/api/gateway/purchaseorder/statushistory/local?poId=2F02876E-DC77-410E-9B77-34F1DC071F80&centerid=A7B07040-707A-4D51-9703-BB6710EBADE7").Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj = new List<PurchaseOrderStatusHistoryViewModel>();
            var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            return x;
        }
    }
}
