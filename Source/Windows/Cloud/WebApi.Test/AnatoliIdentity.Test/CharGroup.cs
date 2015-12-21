using Anatoli.ViewModels.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ClientApp
{
    public class CharGroup
    {
        public static string servserURI = "http://localhost:59822/";
        public static void GetCharGroupInfo(HttpClient client)
        {

            var result = client.GetAsync(servserURI + "/api/gateway/product/chargroups").Result;
            var json = result.Content.ReadAsStringAsync().Result;

            
            List<CharGroupViewModel> dataList = new List<CharGroupViewModel>();
            CharGroupViewModel data = new CharGroupViewModel();
            data.CharGroupCode = 1;
            data.CharGroupName = "mohammad";
            data.ID = 6;
            CharTypeViewModel charData = new CharTypeViewModel();
            charData.ID = 1;
            data.CharTypes = new List<CharTypeViewModel>();
            data.CharTypes.Add(charData);
            charData = new CharTypeViewModel();
            charData.ID = 4;
            charData.CharTypeDesc = "mohammad-1";
            data.CharTypes.Add(charData);
            dataList.Add(data);

            string jsonData = new JavaScriptSerializer().Serialize(dataList);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "/api/gateway/product/chargroups/save", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
        }
    }
}
