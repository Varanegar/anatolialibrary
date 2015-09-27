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
            string servserURI = "http://79.175.166.186/";
            var oauthClient = new OAuth2Client(new Uri(servserURI + "/oauth/token"));
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
                    content.Headers.ContentType = new MediaTypeHeaderValue("ap[ndplication/json");
                     * */
                    var client = new HttpClient();
                    client.SetBearerToken(oauthresult.AccessToken);

                    var result = client.GetAsync(servserURI + "/api/product/chargroups").Result; 
                    var json = result.Content.ReadAsStringAsync().Result;

                    var result2 = client.GetAsync(servserURI + "/api/product/chartypes").Result;
                    var json2 = result2.Content.ReadAsStringAsync().Result;
                    
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
