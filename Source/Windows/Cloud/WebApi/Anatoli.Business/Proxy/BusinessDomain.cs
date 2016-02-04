using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using Anatoli.ViewModels;
using System.Configuration;
using System.Threading.Tasks;
using Anatoli.Business.Helpers;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.Business
{
    public abstract class BusinessDomain<TOut>
            where TOut : BaseViewModel, new()
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; protected set; }

        protected TOut PostOnlineData(string webApiURI, string data, bool needReturnData = false)
        {
            TOut returnData = new TOut();

            try
            {
                var client = new HttpClient();
                client.SetBearerToken(InterServerCommunication.Instance.GetInternalServerToken(PrivateLabelOwnerId.ToString()));
                HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var result = client.PostAsync(ConfigurationManager.AppSettings["InternalServer"] + webApiURI + "?privateOwnerId="
                            + PrivateLabelOwnerId.ToString(), content).Result;
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
                client.SetBearerToken(InterServerCommunication.Instance.GetInternalServerToken(PrivateLabelOwnerId.ToString()));
                var result = client.GetAsync(ConfigurationManager.AppSettings["InternalServer"] + webApiURI + "?privateOwnerId="
                            + PrivateLabelOwnerId.ToString() + "&" + queryString).Result;
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


    public abstract class BusinessDomain1<TMainSource> : IBusinessDomain1<TMainSource>
            where TMainSource : BaseModel, new()
    {
        #region Properties
        protected static log4net.ILog Logger { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid ApplicationKey { get; protected set; }
        public virtual IRepository<TMainSource> MainRepository { get; set; }
        #endregion

        #region Ctors
        public BusinessDomain1() : this(log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)) { }
        public BusinessDomain1(log4net.ILog logger)
        {
            Logger = logger;
        }
        #endregion

        #region Methods
        protected TMainSource PostOnlineData(string webApiURI, string data, bool needReturnData = false)
        {
            var returnData = new TMainSource();

            try
            {
                var client = new HttpClient();

                client.SetBearerToken(InterServerCommunication.Instance.GetInternalServerToken(ApplicationKey.ToString()));

                var content = new StringContent(data, Encoding.UTF8, "application/json");

                var result = client.PostAsync(ConfigurationManager.AppSettings["InternalServer"] + webApiURI + "?privateOwnerId="
                           + ApplicationKey.ToString(), content).Result;

                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Can not save order to server");
                else if (!result.IsSuccessStatusCode)
                    throw new Exception(result.Content.ReadAsStringAsync().Result);

                if (needReturnData)
                {
                    var json = result.Content.ReadAsStringAsync().Result;

                    returnData = JsonConvert.DeserializeAnonymousType(json, new TMainSource());

                    return returnData;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                Logger.Error("Can not post to internal server", ex);

                throw ex;
            }
        }

        protected List<TMainSource> GetOnlineData(string webApiURI, string queryString)
        {
            try
            {
                var client = new HttpClient();

                client.SetBearerToken(InterServerCommunication.Instance.GetInternalServerToken(ApplicationKey.ToString()));

                var result = client.GetAsync(ConfigurationManager.AppSettings["InternalServer"] + webApiURI + "?privateOwnerId="
                           + ApplicationKey.ToString() + "&" + queryString).Result;

                var json = result.Content.ReadAsStringAsync().Result;

                var returnData = JsonConvert.DeserializeAnonymousType(json, new List<TMainSource>());

                return returnData;
            }
            catch (Exception ex)
            {
                Logger.Error("Can not read from internal server", ex);

                throw ex;
            }
        }

        public async Task<ICollection<TMainSource>> GetAllAsync()
        {
            return await MainRepository.GetAllAsync();
        }

        public async Task<ICollection<TMainSource>> GetAllChangedAfterAsync(DateTime selectedDate)
        {
            return await MainRepository.FindAllAsync(p => p.LastUpdate >= selectedDate && p.PrivateLabelOwner.Id == ApplicationKey);
        }

        public async Task<ICollection<TMainSource>> PublishAsync(List<TMainSource> data)
        {
            foreach (var item in data)
            {
                var model = MainRepository.GetByIdAsync(item.Id);
                if (model == null)
                    await MainRepository.AddAsync(item);
                else
                    await MainRepository.UpdateBatchAsync(p => p.Id == item.Id, item);
            }

            return data;
        }

        public async Task DeleteAsync(List<TMainSource> data)
        {
            foreach (var item in data)
                await MainRepository.DeleteBatchAsync(p => p.Id == item.Id);
        }
        #endregion
    }
}
