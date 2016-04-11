using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using Anatoli.ViewModels;
using System.Configuration;
using Anatoli.Business.Helpers;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.Business
{
    public abstract class BusinessDomain<TOut>
        where TOut : BaseViewModel, new ()
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public IPrincipalRepository PrincipalRepository
        {
            get;
            set;
        }

        public Guid ApplicationOwnerId
        {
            get;
            protected set;
        }

        public Guid DataOwnerId
        {
            get;
            protected set;
        }

        public Guid DataOwnerCenterId
        {
            get;
            protected set;
        }

        public bool GetRemovedData
        {
            get;
            protected set;
        }

        protected TOut PostOnlineData(string webApiURI, string data, bool needReturnData = false)
        {
            TOut returnData = new TOut();
            try
            {
                var client = new HttpClient();
                client.SetBearerToken(InterServerCommunication.Instance.GetInternalServerToken(ApplicationOwnerId.ToString()));
                HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var result = client.PostAsync(ConfigurationManager.AppSettings["InternalServer"] + webApiURI + "?ApplicationOwnerId=" + ApplicationOwnerId.ToString(), content).Result;
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    throw new Exception("Can not save order to server");
                }
                else if (!result.IsSuccessStatusCode)
                {
                    throw new Exception(result.Content.ReadAsStringAsync().Result);
                }

                if (needReturnData)
                {
                    var json = result.Content.ReadAsStringAsync().Result;
                    returnData = JsonConvert.DeserializeAnonymousType(json, new TOut());
                    return returnData;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                log.Error("Can not post to internal server", ex);
                throw ex;
            }
        }

        protected List<TOut> GetOnlineData(string webApiURI, string queryString)
        {
            try
            {
                var client = new HttpClient();
                client.SetBearerToken(InterServerCommunication.Instance.GetInternalServerToken(ApplicationOwnerId.ToString()));
                var result = client.GetAsync(ConfigurationManager.AppSettings["InternalServer"] + webApiURI + "?ApplicationOwnerId=" + ApplicationOwnerId.ToString() + "&" + queryString).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                var returnData = JsonConvert.DeserializeAnonymousType(json, new List<TOut>());
                return returnData;
            }
            catch (Exception ex)
            {
                log.Error("Can not read from internal server", ex);
                throw ex;
            }
        }
    }
}