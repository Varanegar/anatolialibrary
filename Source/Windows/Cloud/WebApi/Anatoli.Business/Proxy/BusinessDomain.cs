using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.ViewModels;
using System.Net.Http;
using Anatoli.Business.Helpers;
using System.Configuration;
using System.Text;
using Newtonsoft.Json;

namespace Anatoli.Business
{
    public abstract class BusinessDomain<TOut>
            where TOut : BaseViewModel, new()
    {
        
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; protected set; }

        protected void PostOnlineData(string webApiURI, string data)
        {
            var client = new HttpClient();
            client.SetBearerToken(InterServerCommunication.Instance.GetInternalServerToken(PrivateLabelOwnerId.ToString()));
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result = client.PostAsync(ConfigurationManager.AppSettings["ClientsFilePath"] + webApiURI + "?privateOwnerId="
                        + PrivateLabelOwnerId.ToString(), content).Result;
            return;
        }

        protected List<TOut> GetOnlineData(string webApiURI, string queryString)
        {
            var client = new HttpClient();
            client.SetBearerToken(InterServerCommunication.Instance.GetInternalServerToken(PrivateLabelOwnerId.ToString()));
            var result = client.GetAsync(ConfigurationManager.AppSettings["InternalServer"] + webApiURI + "?privateOwnerId="
                        + PrivateLabelOwnerId.ToString() + "&" + queryString).Result;
            var json = result.Content.ReadAsStringAsync().Result;

            var returnData = JsonConvert.DeserializeAnonymousType(json, new List<TOut>());

            return returnData;
        }
    }
}
