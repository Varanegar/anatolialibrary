using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;

namespace ClientApp
{
    public class BaseTestManagment
    {

        protected static string OwnerKey = "79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240";
        protected static string DataOwnerKey = "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
        protected static string DataOwnerCenterKey = "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
      

        protected static void WriteToConsole(string str)
        {
            Console.WriteLine("<------------------------------------------------------------------------------>");
            Console.WriteLine("<" + DateTime.Now.ToString("F") + ">");
            Console.WriteLine(str);
            Console.WriteLine("<------------------------------------------------------------------------------>");
            Console.ReadLine();
        }

        protected static void Call(HttpClient client, string path, object req)
        {
            string data = new JavaScriptSerializer().Serialize(req);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            content.Headers.Add("OwnerKey", OwnerKey);
            content.Headers.Add("DataOwnerKey", DataOwnerKey);
            content.Headers.Add("DataOwnerCenterKey", DataOwnerCenterKey);

            var result8 = client.PostAsync(path, content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            // var obj = new { message = "", ModelState = new Dictionary<string, string[]>() };
            //var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            WriteToConsole(json8);

        }
    }
}
