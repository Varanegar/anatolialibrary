﻿
using Anatoli.ViewModels;
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
            var dataInfo = new PurchaseOrderRequestModel();
            dataInfo.customerId = Guid.Parse("05496ec3-1d64-4ae3-b6d2-e78cbd89c843");
            dataInfo.centerId = "";
            string data = JsonConvert.SerializeObject(dataInfo);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            content.Headers.Add("OwnerKey", "79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240");
            content.Headers.Add("DataOwnerKey", "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C");
            content.Headers.Add("DataOwnerCenterKey", "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C");

            var result8 = client.GetAsync(servserURI + "/api/gateway/purchaseorder/bycustomerid/local/?customerId=05496ec3-1d64-4ae3-b6d2-e78cbd89c843&privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result;
            //var result8 = client.GetAsync(servserURI + "/api/gateway/purchaseorder/bycustomerid/local?customerId=7A86ABD1-A660-4905-957F-447546112981&centerid=A7B07040-707A-4D51-9703-BB6710EBADE7").Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj = new List<PurchaseOrderViewModel>();
            var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            return x;
        }

        public static List<PurchaseOrderLineItemViewModel> GetCustomerSellDetailInfoFromServer(HttpClient client, string servserURI)
        {
            var dataInfo = new PurchaseOrderRequestModel();
            dataInfo.customerId = Guid.Parse("4bd55fe2-1879-4e33-a931-0398b831108c");
            dataInfo.centerId = "";
            string data = JsonConvert.SerializeObject(dataInfo);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            content.Headers.Add("OwnerKey", "79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240");
            content.Headers.Add("DataOwnerKey", "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C");
            content.Headers.Add("DataOwnerCenterKey", "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C");


            //F125EDC7-473D-4C59-B966-3EF9E6E6A7D9
            var result8 = client.PostAsync(servserURI + "/api/gateway/purchaseorder/bycustomerid/", content).Result;
            //var result8 = client.GetAsync(servserURI + "/api/gateway/purchaseorder/lineitem/?poId=406de307-39aa-40e9-9ecc-4779b181a11e&privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj = new List<PurchaseOrderLineItemViewModel>();
            var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            return x;
        }

        public static List<PurchaseOrderStatusHistoryViewModel> GetCustomerSellHistoryInfoFromServer(HttpClient client, string servserURI)
        {
            //F125EDC7-473D-4C59-B966-3EF9E6E6A7D9
            //var result8 = client.GetAsync(servserURI + "/api/gateway/purchaseorder/bycustomerid/local?customerId=2BFFBDF6-9362-4CB6-807B-37D2DFBEBB96&privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result;
            var result8 = client.GetAsync(servserURI + "/api/gateway/purchaseorder/statushistory/?poId=406de307-39aa-40e9-9ecc-4779b181a11e&privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj = new List<PurchaseOrderStatusHistoryViewModel>();
            var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            return x;
        }
    }
}
