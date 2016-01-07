using RestSharp.Portable;
using RestSharp.Portable.Authenticators;
using RestSharp.Portable.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;

namespace Anatoli.Framework.AnatoliBase
{
    public abstract class AnatoliWebClient
    {
        public abstract bool IsOnline();
        AnatoliTokenInfo _userTokenInfo;
        AnatoliTokenInfo _appTokenInfo;
        public async Task<bool> LoadTokenFileAsync()
        {
            try
            {
                byte[] cipherText = await Task.Run(() =>
                {
                    byte[] result = AnatoliClient.GetInstance().FileIO.ReadAllBytes(AnatoliClient.GetInstance().FileIO.GetDataLoction(), Configuration.tokenInfoFile);
                    return result;
                });
                byte[] plainText = Crypto.DecryptAES(cipherText);
                string tokenString = Encoding.Unicode.GetString(plainText, 0, plainText.Length);
                string[] tokenStringFields = tokenString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                _userTokenInfo.AccessToken = tokenStringFields[0];
                _userTokenInfo.ExpiresIn = long.Parse(tokenStringFields[1]);
                _appTokenInfo.AccessToken = tokenStringFields[2];
                _appTokenInfo.ExpiresIn = long.Parse(tokenStringFields[3]);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> SaveTokenFileAsync()
        {
            string content = "";
            if (_userTokenInfo != null)
            {
                if (_userTokenInfo.AccessToken != null)
                {
                    content += _userTokenInfo.AccessToken + Environment.NewLine + _userTokenInfo.ExpiresIn.ToString() + Environment.NewLine;
                }
            }
            if (_userTokenInfo != null)
            {
                if (_userTokenInfo.AccessToken != null)
                {
                    content += _appTokenInfo.AccessToken + Environment.NewLine + _appTokenInfo.ExpiresIn;
                }
            }

            try
            {
                bool wResult = await Task.Run(() =>
                {
                    var cipherText = Crypto.EncryptAES(content);
                    bool result = AnatoliClient.GetInstance().FileIO.WriteAllBytes(cipherText, AnatoliClient.GetInstance().FileIO.GetDataLoction(), Configuration.tokenInfoFile);
                    return true;
                });
                return wResult;
            }
            catch (Exception)
            {
                return false;
            }
        }
        async Task<AnatoliTokenInfo> GetTokenAsync(TokenType tokenType)
        {
            if (tokenType == TokenType.UserToken)
            {
                if (_userTokenInfo == null)
                {
                    await LoadTokenFileAsync();
                    if (_userTokenInfo == null)
                    {
                        await RefreshTokenAsync();
                        if (_userTokenInfo == null)
                        {
                            throw new ServerUnreachable();
                        }
                    }
                }
                return _userTokenInfo;
            }
            else
            {
                if (_appTokenInfo == null)
                {
                    await LoadTokenFileAsync();
                    if (_appTokenInfo == null)
                    {
                        await RefreshTokenAsync();
                        if (_appTokenInfo == null)
                        {
                            throw new ServerUnreachable();
                        }
                    }
                }
                return _appTokenInfo;
            }
        }
        RestRequest CreateRequest(AnatoliTokenInfo tokenInfo, string requestUrl, HttpMethod method, params Tuple<string, string>[] parameters)
        {
            var request = new RestRequest(requestUrl, method);
            request.AddParameter("Authorization", string.Format("Bearer {0}", tokenInfo.AccessToken), ParameterType.HttpHeader);
            request.AddHeader("Accept", "application/json");
            foreach (var item in parameters)
            {
                Parameter p = new Parameter();
                p.Name = item.Item1;
                p.Value = item.Item2;
                request.AddParameter(p);
            }
            return request;
        }
        RestRequest CreateRequest(AnatoliTokenInfo tokenInfo, string requestUrl, HttpMethod method, Object obj, params Tuple<string, string>[] parameters)
        {
            var request = new RestRequest(requestUrl, method);
            request.AddParameter("Authorization", string.Format("Bearer {0}", tokenInfo.AccessToken), ParameterType.HttpHeader);
            request.AddHeader("Accept", "application/json");
            foreach (var item in parameters)
            {
                Parameter p = new Parameter();
                p.Name = item.Item1;
                p.Value = item.Item2;
                request.AddParameter(p);
            }
            request.AddJsonBody(obj);
            return request;
        }

        RestRequest CreateRequest(AnatoliTokenInfo tokenInfo, string requestUrl, HttpMethod method, object obj)
        {

            var request = new RestRequest(requestUrl, method);
            request.AddParameter("Authorization", string.Format("Bearer {0}", tokenInfo.AccessToken), ParameterType.HttpHeader);
            //request.AddHeader("Accept", "application/json");
            request.AddJsonBody(obj);
            return request;
        }
        public async Task RefreshTokenAsync(TokenRefreshParameters parameters = null)
        {
            var tclient = new Thinktecture.IdentityModel.Client.OAuth2Client(new Uri(Configuration.WebService.PortalAddress + Configuration.WebService.OAuthTokenUrl));
            try
            {
                tclient.Timeout = new TimeSpan(0, 0, 30);
                var oauthresult = await tclient.RequestResourceOwnerPasswordAsync(Configuration.AppMobileAppInfo.UserName, Configuration.AppMobileAppInfo.Password, Configuration.AppMobileAppInfo.Scope);
                if (oauthresult.AccessToken == null)
                    throw new TokenException();
                _appTokenInfo = new AnatoliTokenInfo();
                _appTokenInfo.AccessToken = oauthresult.AccessToken;
                _appTokenInfo.ExpiresIn = oauthresult.ExpiresIn;

                if (parameters != null)
                {
                    oauthresult = await tclient.RequestResourceOwnerPasswordAsync(parameters.UserName, parameters.Password, parameters.Scope);
                    if (oauthresult.AccessToken == null)
                        throw new TokenException();
                    _userTokenInfo = new AnatoliTokenInfo();
                    _userTokenInfo.AccessToken = oauthresult.AccessToken;
                    _userTokenInfo.ExpiresIn = oauthresult.ExpiresIn;
                }

                await SaveTokenFileAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<Result> SendPostRequestAsync<Result>(TokenType tokenType, string requestUri, params Tuple<string, string>[] parameters)
        {
            var client = new RestClient(Configuration.WebService.PortalAddress);
            RestRequest request;
            var token = await GetTokenAsync(tokenType);
            request = CreateRequest(token, requestUri, HttpMethod.Post, parameters);
            return await ExecRequestAsync<Result>(client, request);
        }
        public async Task<Result> SendPostRequestAsync<Result>(TokenType tokenType, string requestUri, object obj)
        {
            var client = new RestClient(Configuration.WebService.PortalAddress);
            RestRequest request;
            var token = await GetTokenAsync(tokenType);
            request = CreateRequest(token, requestUri, HttpMethod.Post, obj);
            return await ExecRequestAsync<Result>(client, request);
        }
        public async Task<Result> SendPostRequestAsync<Result>(TokenType tokenType, string requestUri, object obj, params Tuple<string, string>[] parameters)
        {
            var client = new RestClient(Configuration.WebService.PortalAddress);
            RestRequest request;
            var token = await GetTokenAsync(tokenType);
            request = CreateRequest(token, requestUri, HttpMethod.Post, obj, parameters);
            return await ExecRequestAsync<Result>(client, request);
        }
        public async Task<Result> SendGetRequestAsync<Result>(TokenType tokenType, string requestUri, params Tuple<string, string>[] parameters)
        {
            var client = new RestClient(Configuration.WebService.PortalAddress);
            RestRequest request;
            var token = await GetTokenAsync(tokenType);
            request = CreateRequest(token, requestUri, HttpMethod.Get, parameters);
            return await ExecRequestAsync<Result>(client, request);
        }
        public Result SendGetRequest<Result>(TokenType tokenType, string requestUri, params Tuple<string, string>[] parameters)
        {
            var client = new RestClient(Configuration.WebService.PortalAddress);
            RestRequest request;
            var token = GetTokenAsync(tokenType).Result;
            request = CreateRequest(token, requestUri, HttpMethod.Get, parameters);
            var response = ExecRequest<Result>(client, request);
            return response;
        }
        async Task<Result> ExecRequestAsync<Result>(RestClient client, RestRequest request)
        {
            client.IgnoreResponseStatusCode = true;
            var respone = await client.Execute(request);
            JsonDeserializer deserializer = new JsonDeserializer();
            var result = deserializer.Deserialize<Result>(respone);
            return result;
        }
        Result ExecRequest<Result>(RestClient client, RestRequest request)
        {
            client.IgnoreResponseStatusCode = true;
            var respone = client.Execute(request);
            JsonDeserializer deserializer = new JsonDeserializer();
            var result = deserializer.Deserialize<Result>(respone.Result);
            return result;
        }

    }
    public class AnatoliMetaInfo
    {
        public string ErrorString { get; set; }
        public string ErrorCode { get; set; }
        public bool Result { get; set; }
    }
    public class AnatoliTokenInfo
    {
        public string AccessToken { get; set; }
        public long ExpiresIn { get; set; }
    }
    public enum TokenType
    {
        AppToken = 1,
        UserToken
    }
    public class TokenRefreshParameters
    {
        public TokenRefreshParameters(string userName, string passWord, string scope)
        {
            UserName = userName;
            Password = passWord;
            Scope = scope;
        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Scope { get; set; }
    }
    public class ServerUnreachable : Exception
    {

    }

    public class TokenException : Exception
    {

    }
}
