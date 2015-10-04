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
        public static class WebService
        {
            public static readonly string UserLoginUrl = "/Users/UserLogin";
            public static readonly string UserRegisterUrl = "test.php";
            public static readonly string RateProductUri = "/Products/Rate";
            public static class Products
            {
                public static readonly string ProductView = "product_view.php";
                public static readonly string ProductsView = "products_test.php";
                public static readonly string FOGList = "list.php";
            }
        }
    }
}
