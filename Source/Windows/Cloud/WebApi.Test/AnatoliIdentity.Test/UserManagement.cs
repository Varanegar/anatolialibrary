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

            user.Email = "anatoli-mobile-app8@varanegar.com";
            user.FullName = "09125793221";
            user.Username = "09125793221";
            user.Password = "pass2113";
            user.ConfirmPassword = "pass2113";
            user.PrivateOwnerId = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C");
            user.Mobile = "091257932218";
            user.RoleName = "User";

            string data = new JavaScriptSerializer().Serialize(user);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            content.Headers.Add("OwnerKey", "79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240");
            content.Headers.Add("DataOwnerKey", "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C");
            content.Headers.Add("DataOwnerCenterKey", "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C");

            var result8 = client.PostAsync(servserURI + "/api/accounts/create", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj = new { message = "", ModelState = new Dictionary<string, string[]>() };
            var x = JsonConvert.DeserializeAnonymousType(json8, obj);
        }
    }
}
