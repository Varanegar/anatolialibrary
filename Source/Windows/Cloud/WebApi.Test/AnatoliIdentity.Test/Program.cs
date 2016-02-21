using log4net;
using log4net.Config;
using log4net.Repository;
using Newtonsoft.Json;
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
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {            
            try
            {

                //string servserURI = "http://46.209.104.2:7000/";
                //string servserURI = "http://192.168.201.71:8090/";
                string servserURI = "http://79.175.166.186/";
                //string servserURI = "http://localhost:59822/";
                //string servserURI = "http://localhost/";
                //string servserURI = "http://192.168.0.160:8081/";
                var oauthClient = new OAuth2Client(new Uri(servserURI + "/oauth/token"));
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromHours(1);
                //var storeData = StoreManagement.GetStoreInfo();
                //var storeOnHand = StoreManagement.GetStoreActiveOnhand();
                //var storePriceList = StoreManagement.GetStorePriceList();
                //ProductManagement.ProductGroupTest();

                log4net.Config.XmlConfigurator.Configure();

                var oauthresult = oauthClient.RequestResourceOwnerPasswordAsync("anatoli@varanegar.com", "anatoli@vn@87134", "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result; //, "foo bar"
                if (oauthresult.AccessToken != null)
                {
                    client.SetBearerToken(oauthresult.AccessToken);

                    var requestData = new RequestModel();
                    //requestData.installationId = Guid.Parse("b3cfc74e-2004-47f5-acd7-a9b6f8811076");
                    //string data = new JavaScriptSerializer().Serialize(requestData);
                    //HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    //var result8 = client.PostAsync(servserURI + "/api/testAuth/setparsinfo", content).Result;
                    //var json8 = result8.Content.ReadAsStringAsync().Result;
                    //var obj2 = new { message = "", ModelState = new Dictionary<string, string[]>() };
                    //var x = JsonConvert.DeserializeAnonymousType(json8, obj2);
                    HttpContent content = new StringContent("", Encoding.UTF8, "application/json");
                    var result8 = client.PostAsync(servserURI + "/api/accounts/changepassword/?username=0912073282&password=123456", content).Result;
                    var json8 = result8.Content.ReadAsStringAsync().Result;

                    //HttpContent content = new StringContent("", Encoding.UTF8, "application/json");
                    //var result8 = client.PostAsync(servserURI + "/api/accounts/ResetPassword/?username=0912073282&password=123456", content).Result;
                    //var json8 = result8.Content.ReadAsStringAsync().Result;

                    HttpContent content2 = new StringContent("", Encoding.UTF8, "application/json");
                    var result2 = client.PostAsync(servserURI + "/api/accounts/ConfirmMobile/?username=0912073282&code=752468", content2).Result;
                    var json2 = result2.Content.ReadAsStringAsync().Result;

                    var oauthresult2 = oauthClient.RequestResourceOwnerPasswordAsync("0912073282", "123456", "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result; //, "foo bar"

                    
                    //ProductManagement.DownloadProductRateFromServer(client, servserURI);

                    //ImageManagement.UploadCenterPicture(client, servserURI);
                    //ImageManagement.UploadProductGroupPicture(client, servserURI);
                    //ImageManagement.UploadProductPicture(client, servserURI);

                    //ProductManagement.UploadSupplierToServer(client, servserURI);
                    //ProductManagement.UploadManufactureToServer(client, servserURI);
                    //ProductManagement.UploadProductGroupToServer(client, servserURI);
                    //CharGroupManagement.SaveCharTypeInfoToServer(client, servserURI);
                    //CharGroupManagement.SaveCharGroupInfoToServer(client, servserURI);
                    //CityRegionManagement.UpdateCityRegionFromServer(client, servserURI);
                    StoreManagement.GetStoreFromServer(client, servserURI);
                    //StoreManagement.UploadStoreDataToServer(client, servserURI);
                    //ProductManagement.UploadProductToServer(client, servserURI);
                    
                    //StoreManagement.UploadStorePriceListDataToServer(client, servserURI);
                    //StoreManagement.UploadStoreOnHandDataToServer(client, servserURI);
                    //StoreManagement.DownloadOnhandOnlineFromServer(client, servserURI);
                    //BaseDataManagement.SaveBaseTypeInfoToServer(client, servserURI);

                    //UserManagement.TestUserInfo(client, servserURI);
                    //CustomerManagement.UpdateCustomerFromServer(client, servserURI);
                    //BasketManagement.UpdateCustomerBasketFromServer(client, servserURI);
                    //BasketManagement.DeleteCustomerBaskets(client, servserURI);
                    ProductManagement.DownloadProductGroupFromServer(client, servserURI);
                    //IncompleteManagement.GetIncompleteFromServer(client, servserURI);
                }
                
            }
            catch (Exception ex)
            {
                log.Error("error", ex);
                Console.WriteLine("Error, {0}", ex.Message);
            }
        }
    }
    public class data
    {
        public DateTime LastUpdateTime { get; set; }
    }
    #region User
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

    public class RequestModel
    {
        public string privateOwnerId { get; set; }
        public string stockId { get; set; }
        public string userId { get; set; }
        public string dateAfter { get; set; }
        public List<string> stockIds { get; set; }
        public Guid installationId { get; set; }
    }
    #endregion
}

