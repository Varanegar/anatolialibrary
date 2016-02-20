using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Anatoli.Cloud.WebApi.Classes
{
    public class SMSManager
    {
        public async Task SendSMS(string phoneNo, string messageBody)
        {
            using (var client = new HttpClient())
            {
                messageBody = messageBody.Replace(" ", "%20");
                string uriStr = "https://ws.adpdigital.com/url/send?unicode=1&username=varanegar&password=varanegar&dstaddress=" + phoneNo + "&body=" + messageBody + "&clientid=1";

                var uri = new Uri(uriStr);

                var response = await client.GetAsync(uri);

                string textResult = await response.Content.ReadAsStringAsync();
            }
        }
    }
}