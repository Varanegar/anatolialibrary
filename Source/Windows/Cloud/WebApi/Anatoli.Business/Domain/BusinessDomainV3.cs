using System;
using LinqKit;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using Anatoli.DataAccess;
using Anatoli.ViewModels;
using System.Data.Entity;
using System.Configuration;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Anatoli.Business.Helpers;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using EntityFramework.Extensions;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;

namespace Anatoli.Business.Domain
{
    public abstract class BusinessDomainV3<TSource> : IBusinessDomainV3<TSource> where TSource : BaseModel, new()
    {
        #region Properties
        protected static log4net.ILog Logger { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid ApplicationOwnerKey { get; protected set; }
        public Guid DataOwnerKey { get; protected set; }
        public Guid DataOwnerCenterKey { get; protected set; }
        public bool GetRemovedData { get; protected set; }
        public AnatoliDbContext DBContext { get; set; }
        public virtual IRepository<TSource> MainRepository { get; set; }
        #endregion

        #region Ctors
        BusinessDomainV3()
        {
        }

        public BusinessDomainV3(Guid applicationOwnerKey) : this(applicationOwnerKey, applicationOwnerKey, applicationOwnerKey, new AnatoliDbContext())
        {
        }

        public BusinessDomainV3(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey) :
                                this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {
        }

        public BusinessDomainV3(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc) :
                                this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey,
                                    (IRepository<TSource>)Activator.CreateInstance(typeof(IRepository<TSource>), dbc),
                                    new PrincipalRepository(dbc))
        {
            DBContext = dbc;
        }

        public BusinessDomainV3(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, IRepository<TSource> dataRepository,
                                IPrincipalRepository principalRepository)
        {
            MainRepository = dataRepository;
            PrincipalRepository = principalRepository;
            ApplicationOwnerKey = applicationOwnerKey;
            DataOwnerKey = dataOwnerKey;
            DataOwnerCenterKey = dataOwnerCenterKey;

            Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }
        #endregion

        #region Methods
        protected abstract Expression<Func<TSource, TResult>> GetAllSelector<TResult>();
        /// <summary>
        /// To set MainRepository ExtraPredicate property
        /// </summary>
        /// <returns></returns>
        protected abstract Expression<Func<TSource, bool>> SetConditionForFetchingData();

        protected TResult PostOnlineData<TResult>(string webApiURI, string data, bool needReturnData = false) where TResult : class, new()
        {
            var returnData = new TResult();

            try
            {
                var client = new HttpClient();

                client.SetBearerToken(InterServerCommunication.Instance.GetInternalServerToken(ApplicationOwnerKey.ToString(), DataOwnerKey.ToString()));

                var content = new StringContent(data, Encoding.UTF8, "application/json");

                content.Headers.Add("OwnerKey", ApplicationOwnerKey.ToString());
                content.Headers.Add("DataOwnerKey", DataOwnerKey.ToString());
                content.Headers.Add("DataOwnerCenterKey", DataOwnerKey.ToString());


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

                client.SetBearerToken(InterServerCommunication.Instance.GetInternalServerToken(ApplicationOwnerKey.ToString(), DataOwnerKey.ToString()));

                var content = new StringContent(data, Encoding.UTF8, "application/json");

                content.Headers.Add("OwnerKey", ApplicationOwnerKey.ToString());
                content.Headers.Add("DataOwnerKey", DataOwnerKey.ToString());
                content.Headers.Add("DataOwnerCenterKey", DataOwnerKey.ToString());

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
            return await MainRepository.GetQuery().Where(p => p.Id == id).Select(GetAllSelector<TResult>()).FirstOrDefaultAsync();
        }

        public async Task<List<TResult>> GetAllAsync<TResult>()
        {
            return await GetAllAsync<TResult>(null);
        }

        public async Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<TSource, bool>> predicate,
                                                             Expression<Func<TSource, TResult>> selector)
        {
            Expression<Func<TSource, bool>> criteria2 = p=> p.ApplicationOwnerId == ApplicationOwnerKey && 
                                                            p.DataOwnerId == DataOwnerKey &&
                                                            p.IsRemoved == (GetRemovedData ? p.IsRemoved : false);
            if (predicate != null)
                criteria2 = PredicateBuilder.And(predicate, criteria2);                                 
           
            return await MainRepository.GetQuery().Where(criteria2).AsNoTracking().Select(selector).ToListAsync();
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

                    item.ApplicationOwnerId = ApplicationOwnerKey;
                    item.DataOwnerId = DataOwnerKey;
                    item.DataOwnerCenterId = DataOwnerCenterKey;

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

        public virtual async Task PublishAsync(TSource data)
        {
            var model = await MainRepository.GetByIdAsync(data.Id);

            data.ApplicationOwnerId = ApplicationOwnerKey;
            data.DataOwnerId = DataOwnerKey;
            data.DataOwnerCenterId = DataOwnerCenterKey;

            AddDataToRepository(model, data);

            await MainRepository.SaveChangesAsync();
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
                var currentDataList = MainRepository.GetQuery()
                                                    .Where(p => p.ApplicationOwnerId == ApplicationOwnerKey && p.DataOwnerId == DataOwnerKey)
                                                    .Select(data => new TResult { UniqueId = data.Id })
                                                    .AsNoTracking()
                                                    .ToList();

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

        protected virtual void AddDataToRepository(TSource currentData, TSource newItem)
        {
            return;
        }

        public virtual List<TSource> GetDataListToCheckForExistsData()
        {
            return MainRepository.GetQuery()
                                 .Where(p => p.DataOwnerId == DataOwnerKey && p.ApplicationOwnerId == ApplicationOwnerKey)
                                 .AsNoTracking()
                                 .ToList();
        }
        #endregion
    }
}
