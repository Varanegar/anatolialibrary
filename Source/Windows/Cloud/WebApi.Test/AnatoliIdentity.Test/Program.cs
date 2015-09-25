using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;


namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var oauthClient = new OAuth2Client(new Uri("http://localhost:59823/oauth/token"));
            try
            {
                var oauthresult = oauthClient.RequestResourceOwnerPasswordAsync("anatoli", "anatoli", "foo bar").Result;
                if (oauthresult.AccessToken != null)
                {
                    Console.WriteLine(oauthresult.AccessToken);
                    Console.WriteLine();
                    /*
                    HttpRequestMessage request = new HttpRequestMessage();
                    HttpContent content = new StringContent(@"{ ""username"": ""anatoli""}");
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                     * */
                    var client = new HttpClient();
                    client.SetBearerToken(oauthresult.AccessToken);

                    var result = client.GetAsync("http://localhost:59823/api/gateway/basedata/basevalues").Result; 
                    var json = result.Content.ReadAsStringAsync().Result;

                    var result2 = client.GetAsync("http://localhost:59823/api/app/basket/baskets").Result;
                    var json2 = result.Content.ReadAsStringAsync().Result;
                    
                    Console.WriteLine(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error, {0}", ex.Message);
            }
        }
    }
}
