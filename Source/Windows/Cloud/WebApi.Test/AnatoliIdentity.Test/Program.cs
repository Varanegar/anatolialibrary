using log4net;
using log4net.Config;
using log4net.Repository;
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

                //string servserURI = "http://79.175.166.186/";
                string servserURI = "http://localhost:59822/";
                //string servserURI = "http://localhost/";
                //string servserURI = "http://192.20.6.6/";
                var oauthClient = new OAuth2Client(new Uri(servserURI + "/oauth/token"));
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromHours(1);
                //var storeData = StoreManagement.GetStoreInfo();
                //var storeOnHand = StoreManagement.GetStoreActiveOnhand();
                //var storePriceList = StoreManagement.GetStorePriceList();
                //ProductManagement.ProductGroupTest();

                log4net.Config.XmlConfigurator.Configure();

                var oauthresult = oauthClient.RequestResourceOwnerPasswordAsync("AnatoliMobileApp", "Anatoli@App@Vn", "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result; //, "foo bar"
                if (oauthresult.AccessToken != null)
                {
                    client.SetBearerToken(oauthresult.AccessToken);
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
                    //StoreManagement.UploadStoreDataToServer(client, servserURI);
                    //ProductManagement.UploadProductToServer(client, servserURI);
                    //StoreManagement.UploadStorePriceListDataToServer(client, servserURI);
                    //StoreManagement.UploadStoreOnHandDataToServer(client, servserURI);
                    //StoreManagement.DownloadOnhandOnlineFromServer(client, servserURI);
                    BaseDataManagement.SaveBaseTypeInfoToServer(client, servserURI);

                    //UserManagement.TestUserInfo(client, servserURI);
                    //CustomerManagement.UpdateCustomerFromServer(client, servserURI);
                    //BasketManagement.UpdateCustomerBasketFromServer(client, servserURI);
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
    #endregion
}

//var result9 = client.GetAsync(servserURI + "/api/accounts/user/09122073285").Result;
//var json9 = result9.Content.ReadAsStringAsync().Result;

//CharGroup.GetCharGroupInfo(client);

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
