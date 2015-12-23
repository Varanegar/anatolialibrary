using Newtonsoft.Json;
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

            user.Email = "anatoli-mobile-app2@varanegar.com";
            user.FullName = "anatoli-mobile-app2";
            user.Username = "091257932212";
            user.Password = "pass211";
            user.ConfirmPassword = "pass211";
            user.PrivateOwnerId = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C");
            user.Mobile = "091257932212";
            //user.RoleName = "User";
            
            string data = new JavaScriptSerializer().Serialize(user);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "/api/accounts/create", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj = new { message = "", ModelState = new Dictionary<string, string[]>() };
            var x = JsonConvert.DeserializeAnonymousType(json8, obj);
        }
    }
}
