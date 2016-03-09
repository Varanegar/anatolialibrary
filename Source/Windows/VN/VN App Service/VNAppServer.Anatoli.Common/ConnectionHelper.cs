using Anatoli.ViewModels.BaseModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VNAppServer.Anatoli.Common
{
    public class ConnectionHelper
    {


        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string CallServerServicePost(string data, string URI, HttpClient client, string OwnerKey)
        {
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            content.Headers.Add("OwnerKey", OwnerKey);
            var result = client.PostAsync(URI, content).Result;
            var str = result.Content.ReadAsStringAsync().Result;
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return str;
            }
            else
            {
                log.Error("Fail CallServerService URI :" + URI + "\n" + str);
                throw new Exception("Fail CallServerService URI :" + URI + "\n" + str);
            }
        }

        public static string CallServerServicePost(string data, string URI, HttpClient client)
        {
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result = client.PostAsync(URI, content).Result;
            var str = result.Content.ReadAsStringAsync().Result;
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return str;
            }
            else
            {
                log.Error("Fail CallServerService URI :" + URI + "\n" + str);
                throw new Exception("Fail CallServerService URI :" + URI + "\n" + str);
            }
        }
        public static T CallServerServicePost<T>(string data, string URI, HttpClient client)
        {
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result = client.PostAsync(URI, content).Result;
            var str = result.Content.ReadAsStringAsync().Result;
            if (result.StatusCode == System.Net.HttpStatusCode.OK || result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return JsonConvert.DeserializeObject<T>(str);
            }
            else 
            {
                log.Error("Fail CallServerService URI :" + URI + "\n" + str);
                throw new Exception("Fail CallServerService URI :" + URI + "\n" + str);
            }
        }

        public static T CallServerServicePost<T>(ConnectionHelperRequestModel data, string URI, HttpClient client, string OwnerKey)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            content.Headers.Add("OwnerKey", OwnerKey);
            var result = client.PostAsync(URI, content).Result;
            var str = result.Content.ReadAsStringAsync().Result;
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(str);
            }
            else
            {
                log.Error("Fail CallServerService URI :" + URI + "\n" + str);
                throw new Exception("Fail CallServerService URI :" + URI + "\n" + str);
            }
        }

        public static T CallServerServicePost<T>(ConnectionHelperRequestModel data, string URI, HttpClient client)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var result = client.PostAsync(URI, content).Result;
            var str = result.Content.ReadAsStringAsync().Result;
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(str);
            }
            else
            {
                log.Error("Fail CallServerService URI :" + URI + "\n" + str);
                throw new Exception("Fail CallServerService URI :" + URI + "\n" + str);
            }
        }
        public static T CallServerServiceGet<T>(string URI, HttpClient client)
            where T : class, new()
        {
            var result = client.GetAsync(URI).Result;
            var str = result.Content.ReadAsStringAsync().Result;
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(str);
            }
            else
            {
                log.Error("Fail CallServerService URI :" + URI + "\n" + str);
                throw new Exception("Fail CallServerService URI :" + URI + "\n" + str);
            }
        }
        public static void CallServerService(List<ItemImageViewModel> dataList, HttpClient client, string URI)
        {
            dataList.ForEach(item =>
            {
                var requestContent = new MultipartFormDataContent();
                var imageContent = new ByteArrayContent(item.image);
                imageContent.Headers.ContentType =
                    MediaTypeHeaderValue.Parse("image/jpeg");

                requestContent.Add(imageContent, item.BaseDataId + "-" + item.ID, item.BaseDataId + "-" + item.ID + ".png");
                var response = client.PostAsync(URI + "&isDefault=" + item.IsDefault + "&imageId=" + item.UniqueId + "&imagetype=" + item.ImageType + "&token=" + item.BaseDataId, requestContent).Result;
                if(response.IsSuccessStatusCode)
                {
                    return;
                }
                else
                {
                    log.Error("Fail CallServerService URI :" + URI);
                    throw new Exception("Fail CallServerService URI :" + URI);
                }
            }
            );
        }
    }
}
