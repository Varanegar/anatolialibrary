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
        public struct WebService
        {
            public static readonly string PortalAddress = "http://79.175.166.186/";
            public static readonly string OAuthTokenUrl = "/oauth/token";
            public static readonly string UserLoginUrl = "/Users/UserLogin";
            public static readonly string UserRegisterUrl = "api/accounts/create";
            public static readonly string RateProductUri = "/Products/Rate";
            public struct Products
            {
                public static readonly string ProductView = "product_view.php";
                public static readonly string ProductsView = "products_test.php";
                public static readonly string FOGList = "list.php";
            }
            public struct Stores
            {
                public static readonly string StoreView = "store_view";
                public static readonly string StoresView = "store_list";
            }
        }
    }
}
