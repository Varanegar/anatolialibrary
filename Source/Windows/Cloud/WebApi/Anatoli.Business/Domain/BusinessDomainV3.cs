using NLog;
using System;
using AutoMapper;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection;
using Anatoli.DataAccess;
using Anatoli.ViewModels;
using System.Configuration;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Anatoli.Business.Helpers;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using EntityFramework.Extensions;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.Business.Domain
{
    public abstract class BusinessDomainV3<TSource> : IBusinessDomainV3<TSource> where TSource : BaseModel, new()
    {
        #region Properties
        protected static readonly Logger Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString());

        public OwnerInfo OwnerInfo { get; set; }

        public AnatoliDbContext DBContext { get; set; }

        public virtual IRepository<TSource> MainRepository { get; set; }

        public static TypeInfo TypeofMainRepository
        {
            get
            {
                var currentAssembly = typeof(IRepository<TSource>).GetTypeInfo().Assembly;

                return currentAssembly.DefinedTypes
                                      .Where(typ => typ.ImplementedInterfaces.Any(inter => inter == typeof(IRepository<TSource>)))
                                      .FirstOrDefault();
            }
        }
        #endregion

        #region Ctors
        BusinessDomainV3()
        {
        }

        public BusinessDomainV3(Guid applicationOwnerKey) : this(applicationOwnerKey, applicationOwnerKey, applicationOwnerKey)
        {
        }

        public BusinessDomainV3(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey) :
                                this(new OwnerInfo
                                {
                                    DataOwnerKey = dataOwnerKey,
                                    ApplicationOwnerKey = applicationOwnerKey,
                                    DataOwnerCenterKey = dataOwnerCenterKey,
                                    RemovedData = true
                                }, new AnatoliDbContext())
        {
        }

        public BusinessDomainV3(OwnerInfo ownerInfo, AnatoliDbContext dbc) :
                           this(ownerInfo, (IRepository<TSource>)Activator.CreateInstance(TypeofMainRepository, new object[] { dbc, ownerInfo }))
        {
            DBContext = dbc;
        }

        public BusinessDomainV3(OwnerInfo ownerInfo, IRepository<TSource> dataRepository)
        {
            MainRepository = dataRepository;
            OwnerInfo = ownerInfo;
            SetConditionForFetchingData();
        }
        #endregion

        #region Methods
        public virtual Expression<Func<TSource, TResult>> GetAllSelector<TResult>()
        {
            return null;
        }

        /// <summary>
        /// To set MainRepository.ExtraPredicate property
        /// </summary>
        /// <returns></returns>
        public virtual void SetConditionForFetchingData()
        {

        }

        protected TResult PostOnlineData<TResult>(string webApiURI, string data, bool needReturnData = false) where TResult : class, new()
        {
            var returnData = new TResult();

            try
            {
                var client = new HttpClient();

                client.SetBearerToken(InterServerCommunication.Instance.GetInternalServerToken(OwnerInfo.ApplicationOwnerKey.ToString(),
                                      OwnerInfo.DataOwnerKey.ToString()));

                var content = new StringContent(data, Encoding.UTF8, "application/json");

                content.Headers.Add("OwnerKey", OwnerInfo.ApplicationOwnerKey.ToString());
                content.Headers.Add("DataOwnerKey", OwnerInfo.DataOwnerKey.ToString());
                content.Headers.Add("DataOwnerCenterKey", OwnerInfo.DataOwnerKey.ToString());


                var result = client.PostAsync(ConfigurationManager.AppSettings["InternalServer"] + webApiURI, content).Result;

                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Can not save order to server");
                else if (!result.IsSuccessStatusCode)
                    throw new Exception(result.Content.ReadAsStringAsync().Result);

                if (needReturnData)
                {
                    var json = result.Content.ReadAsStringAsync().Result;

                    returnData = JsonConvert.DeserializeAnonymousType(json, new TResult());

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
        protected List<TResult> GetOnlineData<TResult>(string webApiURI, string data) where TResult : class, new()
        {
            try
            {
                var client = new HttpClient();

                client.SetBearerToken(InterServerCommunication.Instance.GetInternalServerToken(OwnerInfo.ApplicationOwnerKey.ToString(), OwnerInfo.DataOwnerKey.ToString()));

                var content = new StringContent(data, Encoding.UTF8, "application/json");

                content.Headers.Add("OwnerKey", OwnerInfo.ApplicationOwnerKey.ToString());
                content.Headers.Add("DataOwnerKey", OwnerInfo.DataOwnerKey.ToString());
                content.Headers.Add("DataOwnerCenterKey", OwnerInfo.DataOwnerKey.ToString());

                var result = client.PostAsync(ConfigurationManager.AppSettings["InternalServer"] + webApiURI
                           , content).Result;

                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Can not save order to server");
                else if (!result.IsSuccessStatusCode)
                    throw new Exception(result.Content.ReadAsStringAsync().Result);

                var json = result.Content.ReadAsStringAsync().Result;

                var returnData = JsonConvert.DeserializeAnonymousType(json, new List<TResult>());

                return returnData;
            }
            catch (Exception ex)
            {
                Logger.Error("Can not read from internal server", ex);
                throw ex;
            }
        }

        public async Task<TResult> GetByIdAsync<TResult>(Guid id)
        {
            if (GetAllSelector<TResult>() == null)
                return await MainRepository.GetByIdAsync(id, GetAllSelector<TResult>());

            return await MainRepository.GetByIdAsync<TResult>(id);
        }

        public async Task<List<TResult>> GetAllAsync<TResult>()
        {
            return await GetAllAsync<TResult>(null);
        }

        public async Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<TSource, bool>> predicate,
                                                              Expression<Func<TSource, TResult>> selector)
        {
            return await MainRepository.FindAllAsync(predicate, selector);
        }

        public async Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<TSource, bool>> predicate)
        {
            return await GetAllAsync(predicate, GetAllSelector<TResult>());
        }

        public async Task<List<TResult>> GetAllChangedAfterAsync<TResult>(DateTime selectedDate)
        {
            return await GetAllAsync<TResult>(p => p.LastUpdate >= selectedDate);
        }

        public virtual async Task PublishAsync(List<TSource> data)
        {
            try
            {
                MainRepository.DbContext.Configuration.AutoDetectChangesEnabled = false;

                var dataList = GetDataListToCheckForExistsData();

                foreach (var item in data)
                {
                    var model = dataList.Find(p => p.Id == item.Id);

                    item.ApplicationOwnerId = OwnerInfo.ApplicationOwnerKey;
                    item.DataOwnerId = OwnerInfo.DataOwnerKey;
                    item.DataOwnerCenterId = OwnerInfo.DataOwnerCenterKey;

                    AddDataToRepository(model, item);
                }

                await MainRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error("PublishAsync", ex);
                throw ex;
            }
            finally
            {
                MainRepository.DbContext.Configuration.AutoDetectChangesEnabled = true;
                Logger.Info("PublishAsync Finish" + data.Count);
            }
        }

        public virtual async Task PublishAsync<TResult>(List<TResult> data) where TResult : BaseViewModel
        {
            var dest = Mapper.Map<List<TResult>, List<TSource>>(data);

            await PublishAsync(dest);
        }

        public virtual async Task PublishAsync(TSource data)
        {
            var model = await MainRepository.GetByIdAsync(data.Id);

            data.ApplicationOwnerId = OwnerInfo.ApplicationOwnerKey;
            data.DataOwnerId = OwnerInfo.DataOwnerKey;
            data.DataOwnerCenterId = OwnerInfo.DataOwnerCenterKey;

            AddDataToRepository(model, data);

            await MainRepository.SaveChangesAsync();
        }

        public virtual async Task PublishAsync<TResult>(TResult data) where TResult : BaseViewModel
        {
            var dest = Mapper.Map<TResult, TSource>(data);

            await PublishAsync(dest);
        }

        public virtual async Task DeleteAsync(List<TSource> data)
        {
            foreach (var item in data)
                await MainRepository.DeleteBatchAsync(p => p.Id == item.Id);
        }

        public virtual async Task DeleteAsync<TResult>(List<TResult> data) where TResult : BaseViewModel
        {
            foreach (var item in data)
                await MainRepository.DeleteBatchAsync(p => p.Id == item.UniqueId);
        }

        public virtual async Task CheckDeletedAsync<TResult>(List<TResult> dataViewModels) where TResult : BaseViewModel, new()
        {
            try
            {
                var currentDataList = await MainRepository.GetAllAsync(data => new TResult { UniqueId = data.Id });

                currentDataList.ForEach(item =>
                {
                    if (dataViewModels.Find(p => p.UniqueId == item.UniqueId) == null)
                        MainRepository.GetQuery()
                                      .Where(p => p.Id == item.UniqueId)
                                      .UpdateAsync(t => new TSource { LastUpdate = DateTime.Now, IsRemoved = true });
                });

                await MainRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error("CheckForDeletedAsync", ex);
                throw ex;
            }
        }

        public virtual void AddDataToRepository(TSource currentData, TSource newItem)
        {
            return;
        }

        public virtual List<TSource> GetDataListToCheckForExistsData()
        {
            return MainRepository.GetAll();
        }
        #endregion
    }
}
