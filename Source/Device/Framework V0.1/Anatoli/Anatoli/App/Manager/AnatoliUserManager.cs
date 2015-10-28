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
        public async Task<AnatoliUserModel> LoginAsync(string userName, string passWord)
        {
            var tk = await AnatoliClient.GetInstance().WebClient.RefreshTokenAsync(new TokenRefreshParameters(userName, passWord, "foo bar"));
            if (tk != null)
            {
                var userModel = await AnatoliClient.GetInstance().WebClient.SendGetRequestAsync<AnatoliUserModel>("/api/accounts/user/anatoli");
                return userModel;
            }
            return null;
        }
        public async Task<RegisterResult> RegisterAsync(string userName, string passWord, string confirmPassword, string firstName, string lastName, string tel, string email)
        {
            User user = new User();
            user.Email = "hooman.ahmadi1@gmail.com";
            user.FirstName = "hooman2";
            user.LastName = "ahmadi3";
            user.UserName = "hooman.ahmadi2";
            user.Password = "Hooman.ahmadi2";
            user.ConfirmPassword = "Hooman.ahmadi2";
            try
            {
                var result = await AnatoliClient.GetInstance().WebClient.SendPostRequestAsync<RegisterResult>(
                Configuration.WebService.Users.UserCreateUrl,
                user
                );
                ParseObject userParseObject = new ParseObject("AnatoliUser");
                userParseObject.Add("UserId", result.UserId);
                await userParseObject.SaveAsync();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        internal class User
        {
            public string UserName { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }

    }
}
