using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.Framework.Manager;
using Anatoli.App.Model.AnatoliUser;
using Anatoli.Framework.AnatoliBase;
using Anatoli.Framework.DataAdapter;
using PCLCrypto;
using Anatoli.App.Model;
namespace Anatoli.App.Manager
{
    public class AnatoliUserManager : BaseManager<BaseDataAdapter<AnatoliUserModel>, AnatoliUserModel>
    {
        public static async Task<AnatoliUserModel> LoginAsync(string userName, string passWord)
        {
            await AnatoliClient.GetInstance().WebClient.RefreshTokenAsync(new TokenRefreshParameters(userName, passWord, "foo bar"));
            var userModel = await AnatoliClient.GetInstance().WebClient.SendGetRequestAsync<AnatoliUserModel>(TokenType.UserToken, "/api/accounts/user/" + userName);
            if (userModel.IsValid)
            {
#pragma warning disable
                BasketManager.SyncDataBase();
#pragma warning restore
            }
            return userModel;
        }
        public async Task<RegisterResult> RegisterAsync(string passWord, string confirmPassword, string tel, string email)
        {
            AnatoliUserModel user = new AnatoliUserModel();
            if (!String.IsNullOrEmpty(email))
            {
                user.Email = email.Trim();
            }
            user.Username = tel;
            user.Password = passWord;
            user.ConfirmPassword = passWord;
            user.Mobile = tel;
            try
            {
                var result = await AnatoliClient.GetInstance().WebClient.SendPostRequestAsync<RegisterResult>(
                    TokenType.AppToken,
                Configuration.WebService.Users.UserCreateUrl,
                user
                );
                //ParseObject userParseObject = new ParseObject("AnatoliUser");
                //userParseObject.Add("UserId", result.UserId);
                //await userParseObject.SaveAsync();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void CancelRegisterTask()
        {
            throw new NotImplementedException();
        }
        public static async Task SaveUserInfoAsync(AnatoliUserModel user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("Could not save null user!");
            }
            string content = user.Email +
                Environment.NewLine + user.Username +
                Environment.NewLine + user.UniqueId +
                Environment.NewLine + user.Id;
            bool wResult = await Task.Run(() =>
                {
                    var cipherText = Crypto.EncryptAES(content);
                    bool result = AnatoliClient.GetInstance().FileIO.WriteAllBytes(cipherText, AnatoliClient.GetInstance().FileIO.GetDataLoction(), Configuration.userInfoFile);
                    return result;
                });
        }

        public static async Task<AnatoliUserModel> ReadUserInfoAsync()
        {
            try
            {
                byte[] cipherText = await Task.Run(() =>
                {
                    byte[] result = AnatoliClient.GetInstance().FileIO.ReadAllBytes(AnatoliClient.GetInstance().FileIO.GetDataLoction(), Configuration.userInfoFile);
                    return result;
                });
                byte[] plainText = Crypto.DecryptAES(cipherText);
                string userInfo = Encoding.Unicode.GetString(plainText, 0, plainText.Length);
                string[] userInfoFields = userInfo.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                AnatoliUserModel user = new AnatoliUserModel();
                user.Email = userInfoFields[0];
                user.Username = userInfoFields[1];
                user.UniqueId = userInfoFields[2];
                user.Id = userInfoFields[3];
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<bool> LogoutAsync()
        {
            var fileIO = AnatoliClient.GetInstance().FileIO;
            try
            {
                await Task.Run(() =>
                    {
                        fileIO.DeleteFile(fileIO.GetDataLoction(), Configuration.userInfoFile);
                        fileIO.DeleteFile(fileIO.GetDataLoction(), Configuration.tokenInfoFile);
                        fileIO.DeleteFile(fileIO.GetDataLoction(), Configuration.customerInfoFile);
                    }
                    );
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static async Task<ChangePasswordBindingModel> ChangePassword(string p1, string p2)
        {
            var obj = new ChangePasswordBindingModel();
            obj.ConfirmPassword = p2;
            obj.NewPassword = p2;
            obj.OldPassword = p1;
            var result = await AnatoliClient.GetInstance().WebClient.SendPostRequestAsync<ChangePasswordBindingModel>(TokenType.UserToken, Configuration.WebService.Users.ChangePasswordUri, obj);
            return result;
        }
    }
}
