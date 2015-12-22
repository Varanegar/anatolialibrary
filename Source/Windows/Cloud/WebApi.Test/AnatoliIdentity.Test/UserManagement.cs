using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ClientApp
{
    public class UserManagement
    {
        public static void TestUserInfo(HttpClient client, string servserURI)
        {
            User user = new User();
            user.Email = "hooman.ahmadi4@gmail.com";
            user.FullName = "hooman4";
            user.Username = "hooman.ahmadi4";
            user.Password = "Hooman.ahmadi4";
            user.ConfirmPassword = "Hooman.ahmadi4";
            user.PrivateOwnerId = Guid.Parse("CB11335F-6D14-49C9-9798-AD61D02EDBE1");
            user.Mobile = "09128501331";
            user.RoleName = "AuthorizedApp";

            string data = new JavaScriptSerializer().Serialize(user);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "/api/accounts/create", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
        }
    }
}
