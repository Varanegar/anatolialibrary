using Anatoli.ViewModels;
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
    public static class PermissionManagement
    {
        public static void GetPermissions(HttpClient client, string servserURI)
        {

            string data = "";

            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            content.Headers.Add("OwnerKey", "79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240");
            content.Headers.Add("DataOwnerKey", "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C");
            content.Headers.Add("DataOwnerCenterKey", "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C");
            var result8 = client.PostAsync(servserURI + "api/accounts/myWebpages", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;

            //return x;
            
        }

    }
}
