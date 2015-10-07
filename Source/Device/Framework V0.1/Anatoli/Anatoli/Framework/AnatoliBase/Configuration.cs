using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Anatoliclient
{
    public class Configuration
    {
        public static readonly string parseAppId = "ecg0bl83b3s1B57NtV65iGiy3IH38QfavsF1DHeX";
        public static readonly string parseDotNetKey = "SmgvPBYprBhYo1KTGPIhjoevR3YhXBccqFwqvfXL";
        public static readonly string PortalUri = "http://www.ayuz.ir/";
        public struct WebService
        {
            public static readonly string UserLoginUrl = "/Users/UserLogin";
            public static readonly string UserRegisterUrl = "test.php";
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
