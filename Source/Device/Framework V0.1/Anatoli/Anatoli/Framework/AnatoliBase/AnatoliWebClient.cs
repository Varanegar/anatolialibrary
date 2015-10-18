using RestSharp.Portable;
using RestSharp.Portable.Authenticators;
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
        AnatoliTokenInfo _tokenInfo;
        public abstract AnatoliTokenInfo LoadTokenInfoFromFile();
        public abstract void SaveTokenInfoToFile(AnatoliTokenInfo tokenInfo);
        RestRequest CreateRequest(string requestUrl, HttpMethod method, params Tuple<string, string>[] parameters)
        {
            var request = new RestRequest(requestUrl, method);
            request.AddParameter("Authorization", string.Format("Bearer {0}", _tokenInfo.AccessToken), ParameterType.HttpHeader);
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
        public async Task<AnatoliTokenInfo> GetTokenAsync()
        {
            _tokenInfo = await Task.Run(() => LoadTokenInfoFromFile());
            return _tokenInfo;
        }
        public async Task<AnatoliTokenInfo> RefreshTokenAsync(TokenRefreshParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                var tclient = new Thinktecture.IdentityModel.Client.OAuth2Client(new Uri(Configuration.WebService.PortalAddress + Configuration.WebService.OAuthTokenUrl));
                try
                {
                    var oauthresult = await tclient.RequestResourceOwnerPasswordAsync(parameters.UserName, parameters.Password, parameters.Scope);
                    _tokenInfo = new AnatoliTokenInfo();
                    _tokenInfo.AccessToken = oauthresult.AccessToken;
                    _tokenInfo.ExpiresIn = oauthresult.ExpiresIn;
                    SaveTokenInfoToFile(_tokenInfo);
                    return _tokenInfo;
                }
                catch
                {
                    return null;
                }
            }
        }
        public async Task<Result> SendPostRequestAsync<Result>(string requestUri, params Tuple<string, string>[] parameters)
        {
            var client = new RestClient(Configuration.WebService.PortalAddress);
            var request = CreateRequest(requestUri, HttpMethod.Post, parameters);
            var asyncHandle = await client.Execute<Result>(request);
            return asyncHandle.Data;
        }
        public async Task<Result> SendGetRequestAsync<Result>(string requestUri, params Tuple<string, string>[] parameters)
        {
            var client = new RestClient(Configuration.WebService.PortalAddress);
            var request = CreateRequest(requestUri, HttpMethod.Get, parameters);
            var asyncHandle = await client.Execute<Result>(request);
            return asyncHandle.Data;
        }
        public Result SendGetRequest<Result>(string requestUri, params Tuple<string, string>[] parameters)
        {
            var client = new RestClient(Configuration.WebService.PortalAddress);
            var request = CreateRequest(requestUri, HttpMethod.Get, parameters);
            var response = client.Execute<Result>(request);
            return response.Result.Data;
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
}
