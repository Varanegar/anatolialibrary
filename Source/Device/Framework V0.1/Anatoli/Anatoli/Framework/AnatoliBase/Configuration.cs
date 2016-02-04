using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.AnatoliBase
{
    public class Configuration
    {
        public static readonly string parseAppId = "wUAgTsRuLdin0EvsBhPniG40O24i2nEGVFl8R5OI";
        public static readonly string parseDotNetKey = "G7guVuyx35bb4fBOwo7BVhlG2L2E2qKLQI0sLAe0";
        public static readonly string userInfoFile = "userInfo";
        public static readonly string customerInfoFile = "customerInfo";
        public static readonly string tokenInfoFile = "tk.info";
        public struct AppMobileAppInfo
        {
            public static readonly string UserName = "AnatoliMobileApp";
            public static readonly string Password = "Anatoli@App@Vn";
            //public static readonly string UserName = "petropay";
            //public static readonly string Password = "petropay";
            public static readonly string Scope = "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
        }
        public struct WebService
        {
            //public static readonly string PortalAddress = "http://79.175.166.186/";
            //public static readonly string PortalAddress = "http://192.168.201.46/";
            public static string PortalAddress = "http://192.168.0.160:8081/";
            public static readonly string OAuthTokenUrl = "/oauth/token";
            public static readonly string ParseInfo = "api/TestAuth/setParsInfo";
            public static readonly string BaseDatas = "api/gateway/basedata/basedatas/?PrivateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
            public struct ImageManager
            {
                public static readonly string Images = "/api/imageManager/images/after/?PrivateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
                public static readonly string ImageSave = "/api/imageManager/save/?PrivateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
            }
            public struct Products
            {
                public static readonly string ProductsView = "/api/gateway/product/products/after/?PrivateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
                public static readonly string FOGList = "list.php";
                public static readonly string ProductGroups = "/api/gateway/product/productgroups/after/?PrivateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
            }
            public struct Stores
            {
                public static readonly string StoresView = "/api/gateway/store/stores/after/?PrivateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
                public static readonly string PricesView = "/api/gateway/store/storepricelist/after/?PrivateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
                public static readonly string CityRegion = "/api/gateway/base/region/cityregions/after/?PrivateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
                public static readonly string DeliveryTime = "/api/gateway/store/storecalendar/after/?Id=23fc03ff-53f0-432c-b6f4-e560b9088d54&PrivateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
                public static readonly string CalcPromo = "/api/gateway/purchaseorder/calcpromo/?Id=23fc03ff-53f0-432c-b6f4-e560b9088d54&PrivateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
                public static readonly string Create = "/api/gateway/purchaseorder/create/?Id=23fc03ff-53f0-432c-b6f4-e560b9088d54&PrivateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
            }
            public struct Users
            {
                public static readonly string UserCreateUrl = "/api/accounts/create";
                public static readonly string UserAuthUrl = "/api/accounts/create";
                public static readonly string ViewProfileUrl = "/api/gateway/customer/customers";
                public static readonly string SaveProfileUrl = "/api/gateway/customer/save";
                public static readonly string OrdersHistory = "api/gateway/purchaseorder/history";
                public static readonly string ShoppingCardSave = "/api/gateway/incompletepurchaseorder/save?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
                public static readonly string ShoppingCardView = "/api/gateway/incompletepurchaseorder?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C&customerId=E42F8E97-BFD2-4B18-8C06-6F3BCFF9A42D";
                public static readonly string FavoritSaveItem = "/api/gateway/basket/basketitem/save?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
                public static readonly string FavoritDeleteItem = "/api/gateway/basket/basketitem/delete?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
                public static readonly string FavoritView = "/api/gateway/basket/customerbaskets/bybasket?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
                public static readonly string BasketView = "/api/gateway/basket/customerbaskets/bycustomer?customerId=4ED993BB-B746-4AC7-B455-EBE91834364F&privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
                public static readonly string ChangePasswordUri = "/api/accounts/changepassword/?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
            }
            public static readonly string CityRegion = "/api/gateway/base/region/cityregions/after/?PrivateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
        }

        public static async Task ReadConfigFromFile()
        {
            try
            {
                var content = await Task.Run(() =>
                {
                    return AnatoliClient.GetInstance().FileIO.ReadAllText(AnatoliClient.GetInstance().FileIO.GetDataLoction(), "config");
                });
                if (!String.IsNullOrEmpty(content))
                {
                    WebService.PortalAddress = content;
                }
            }
            catch (Exception)
            {

            }

        }

        public static async Task SaveConfigToFile()
        {
            try
            {
                await Task.Run(() =>
                {
                    AnatoliClient.GetInstance().FileIO.WriteAllText(WebService.PortalAddress, AnatoliClient.GetInstance().FileIO.GetDataLoction(), "config");
                });
            }
            catch (Exception)
            {

            }

        }
    }
}
