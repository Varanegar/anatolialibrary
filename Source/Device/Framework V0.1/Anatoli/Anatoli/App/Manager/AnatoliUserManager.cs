using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.Framework.Manager;
using Anatoli.App.Model.AnatoliUser;
using Anatoli.Framework.AnatoliBase;
using Parse;
using Anatoli.Framework.DataAdapter;
namespace Anatoli.App.Manager
{
    public class AnatoliUserManager : BaseManager<BaseDataAdapter<AnatoliUserModel>, AnatoliUserModel>
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
            userParseObject.Add("UserId", result.userModel.UserId);
            await userParseObject.SaveAsync();
            return result;
        }

        protected override string GetDataTable()
        {
            throw new NotImplementedException();
        }

        protected override string GetWebServiceUri()
        {
            throw new NotImplementedException();
        }
    }
}
