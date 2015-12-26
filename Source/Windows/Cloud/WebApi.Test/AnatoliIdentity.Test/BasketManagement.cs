using Anatoli.ViewModels.BaseModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ClientApp
{
    public static class BasketManagement
    {
        public static List<BasketViewModel> GetCustomerBaskets(HttpClient client, string servserURI)
        {
            List<BasketViewModel> basketList = new List<BasketViewModel>();

            var result8 = client.GetAsync(servserURI + "/api/gateway/basket/customerbaskets?id=0737BD2F-EF7B-4E18-8445-FBE3CD296BEC&privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj = new List<BasketViewModel>();
            var x = JsonConvert.DeserializeAnonymousType(json8, basketList);
            return x;
            
        }

        public static void UpdateCustomerBasketFromServer(HttpClient client, string servserURI)
        {
            var obj = GetCustomerBaskets(client, servserURI);
            BasketItemViewModel bb = new BasketItemViewModel();
            bb.Comment = "asdsad";
            bb.Qty = 10;
            bb.ProductId = Guid.Parse("941c74b3-0ea4-4a65-9666-1f6bd0b9d630");
            obj[0].BasketItems.Add(bb);
            //obj.Baskets.RemoveAt(1);
            string data = new JavaScriptSerializer().Serialize(obj);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "/api/gateway/basket/save?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj2 = new { message = "", ModelState = new Dictionary<string, string[]>() };
            var x = JsonConvert.DeserializeAnonymousType(json8, obj2);
        }

    }
}
