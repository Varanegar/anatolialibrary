using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.Order;
using Anatoli.ViewModels.StoreModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Thunderstruck;

namespace ClientApp
{
    public static class IncompleteManagement
    {
        public static List<IncompletePurchaseOrderViewModel> GetIncompleteFromServer(HttpClient client, string servserURI)
        {
            //F125EDC7-473D-4C59-B966-3EF9E6E6A7D9
            var result8 = client.GetAsync(servserURI + "/api/gateway/incompletepurchaseorder?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C&customerId=E42F8E97-BFD2-4B18-8C06-6F3BCFF9A42D").Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj = new List<IncompletePurchaseOrderViewModel>();
            var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            return x;
        }


        public static void UpdateIncompleteFromServer(HttpClient client, string servserURI)
        {
            var obj = GetIncompleteFromServer(client, servserURI);
            string data = new JavaScriptSerializer().Serialize(obj);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "/api/gateway/incompletepurchaseorder/save?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj2 = new { message = "", ModelState = new Dictionary<string, string[]>() };
            var x = JsonConvert.DeserializeAnonymousType(json8, obj2);
        }
    }
}
