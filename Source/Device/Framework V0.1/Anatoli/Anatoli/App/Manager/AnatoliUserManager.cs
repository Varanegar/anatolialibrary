using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.Framework.Manager;
using Anatoli.App.Adapter;
using Anatoli.App.Model.AnatoliUser;
using Anatoli.Anatoliclient;
using Parse;
namespace Anatoli.App.Manager
{
    public class AnatoliUserManager : BaseManager<AnatoliUserAdapter, AnatoliUserListModel, AnatoliUserModel>
    {
        public async Task<LoginResult> LoginAsync(string userName, string passWord)
        {
            var result = await AnatoliClient.GetInstance().WebClient.SendPostRequestAsync<LoginResult>(
                Configuration.WebService.UserLoginUrl,
                new Tuple<string, string>("user_name", userName),
                new Tuple<string, string>("password", passWord)
                );
            return result;
        }
        public async Task<RegisterResult> RegisterAsync(string userName, string passWord, string firstName, string lastName, string tel)
        {
            var result = await AnatoliClient.GetInstance().WebClient.SendPostRequestAsync<RegisterResult>(
                Configuration.WebService.UserRegisterUrl,
                new Tuple<string, string>("user_name", userName),
                new Tuple<string, string>("password", passWord),
                new Tuple<string, string>("first_name", firstName),
                new Tuple<string, string>("last_name", lastName),
                new Tuple<string, string>("tel", tel)
                );
            ParseObject userParseObject = new ParseObject("AnatoliUser");
            await userParseObject.SaveAsync();
            return result;
        }
    }
}
