using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Thinktecture.IdentityModel.Client;


namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {            
            try
            {
                //string servserURI = "http://79.175.166.186/";
                string servserURI = "http://localhost:59822/";
                var oauthClient = new OAuth2Client(new Uri(servserURI + "/oauth/token"));

                var client = new HttpClient();
                //User user = new User();
                //user.Email = "hooman.ahmadi1@gmail.com";
                //user.FullName = "hooman2";
                //user.Username = "hooman.ahmadi2";
                //user.Password = "Hooman.ahmadi2";
                //user.ConfirmPassword = "Hooman.ahmadi2";
                //user.PrivateOwnerId = Guid.Parse("CB11335F-6D14-49C9-9798-AD61D02EDBE1");
                //user.Mobile = "09128501330";
                //user.RoleName = "AuthorizedApp";

                //string data = new JavaScriptSerializer().Serialize(user);
                //HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
                //var result8 = client.PostAsync(servserURI + "/api/accounts/create", content).Result;
                //var json8 = result8.Content.ReadAsStringAsync().Result;

                //Console.WriteLine(json8);
                //return;

                var oauthresult = oauthClient.RequestResourceOwnerPasswordAsync("hooman.ahmadi2", "Hooman.ahmadi2").Result; //, "foo bar"
                if (oauthresult.AccessToken != null)
                {
                    Console.WriteLine(oauthresult.AccessToken);
                    Console.WriteLine();
                    /*
                    HttpRequestMessage request = new HttpRequestMessage();
                    HttpContent content = new StringContent(@"{ ""username"": ""anatoli""}");
                    content.Headers.ContentType = new MediaTypeHeaderValue("ap[ndplication/json");
                     * */
                    client.SetBearerToken(oauthresult.AccessToken);

                    var result = client.GetAsync(servserURI + "/api/gateway/product/chargroups").Result;
                    var json = result.Content.ReadAsStringAsync().Result;

                    var result3 = client.GetAsync(servserURI + "/api/gateway/product/chartypes").Result;
                    var json3 = result3.Content.ReadAsStringAsync().Result;

                    var result4 = client.GetAsync(servserURI + "/api/gateway/product/productlist").Result;
                    var json4 = result4.Content.ReadAsStringAsync().Result;

                    var result5 = client.GetAsync(servserURI + "/api/gateway/product/productgroups").Result;
                    var json5 = result5.Content.ReadAsStringAsync().Result;

                    var result6 = client.GetAsync(servserURI + "/api/gateway/base/region/cityregion").Result;
                    var json6 = result6.Content.ReadAsStringAsync().Result;

                    var result7 = client.GetAsync(servserURI + "/api/gateway/basedata/basevalues").Result;
                    var json7 = result7.Content.ReadAsStringAsync().Result;

                    var result2 = client.GetAsync(servserURI + "/api/gateway/base/manufacture/manufactures").Result;
                    var json2 = result2.Content.ReadAsStringAsync().Result;


                    var result9 = client.GetAsync(servserURI + "/api/accounts/user/anatoli").Result;
                    var json9 = result9.Content.ReadAsStringAsync().Result;

                    var result10 = client.GetAsync(servserURI + "/api/gateway/store/GetStoreLists?appId="+ "CB11335F-6D14-49C9-9798-AD61D02EDBE1").Result;
                    var json10 = result9.Content.ReadAsStringAsync().Result;

                    Console.WriteLine(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error, {0}", ex.Message);
            }
        }
    }

    public class User
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string Mobile { get; set; }

        public string RoleName { get; set; }

        public Guid PrivateOwnerId { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
