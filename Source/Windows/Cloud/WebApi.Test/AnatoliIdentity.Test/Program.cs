using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Thinktecture.IdentityModel.Client;
using Thunderstruck;


namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {            
            try
            {
                //string servserURI = "http://79.175.166.186/";
                string servserURI = "http://localhost/";
                var oauthClient = new OAuth2Client(new Uri(servserURI + "/oauth/token"));
                var client = new HttpClient();

                var storeData = StoreManagement.GetStoreInfo();
                //ProductManagement.ProductGroupTest();

                var oauthresult = oauthClient.RequestResourceOwnerPasswordAsync("AnatoliMobileApp", "Anatoli@App@Vn", "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result; //, "foo bar"
                if (oauthresult.AccessToken != null)
                {
                    client.SetBearerToken(oauthresult.AccessToken);
                    UserManagement.TestUserInfo(client, servserURI);

                    //var result9 = client.GetAsync(servserURI + "/api/accounts/user/09122073285").Result;
                    //var json9 = result9.Content.ReadAsStringAsync().Result;

                    //CharGroup.GetCharGroupInfo(client);

                    Console.WriteLine(oauthresult.AccessToken);
                    Console.WriteLine();


                    
                    /*
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



                    var result10 = client.GetAsync(servserURI + "/api/gateway/store/GetStoreLists?appId="+ "CB11335F-6D14-49C9-9798-AD61D02EDBE1").Result;
                    var json10 = result9.Content.ReadAsStringAsync().Result;
                    */
                    //Console.WriteLine(json);
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

        //public string RoleName { get; set; }

        public Guid PrivateOwnerId { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
