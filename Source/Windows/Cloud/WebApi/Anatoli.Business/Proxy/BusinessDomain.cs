using System;
using System.Text;
using System.Net.Http;
using System.Data.Entity;
using System.Linq;
using Newtonsoft.Json;
using Anatoli.ViewModels;
using System.Configuration;
using System.Threading.Tasks;
using Anatoli.Business.Helpers;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using System.Linq.Expressions;
using Anatoli.DataAccess;
using Anatoli.DataAccess.Repositories;
using AutoMapper.QueryableExtensions;
using EntityFramework.Extensions;

namespace Anatoli.Business
{
    public abstract class BusinessDomain<TOut>
            where TOut : BaseViewModel, new()
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid ApplicationOwnerId { get; protected set; }
        public Guid DataOwnerId { get; protected set; }
        public Guid DataOwnerCenterId { get; protected set; }
        public bool GetRemovedData { get; protected set; }

        protected TOut PostOnlineData(string webApiURI, string data, bool needReturnData = false)
        {
            TOut returnData = new TOut();

            try
            {
                var client = new HttpClient();
                client.SetBearerToken(InterServerCommunication.Instance.GetInternalServerToken(ApplicationOwnerId.ToString()));
                HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var result = client.PostAsync(ConfigurationManager.AppSettings["InternalServer"] + webApiURI + "?ApplicationOwnerId="
                            + ApplicationOwnerId.ToString(), content).Result;
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
                var result = client.GetAsync(ConfigurationManager.AppSettings["InternalServer"] + webApiURI + "?ApplicationOwnerId="
                            + ApplicationOwnerId.ToString() + "&" + queryString).Result;
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


    public abstract class BusinessDomainV2<TMainSource, TMainSourceView, TMainSourceRepository, TIMainSourceRepository> : IBusinessDomainV2<TMainSource, TMainSourceView>
            where TMainSource : BaseModel, new()
            where TMainSourceView : BaseViewModel, new()
            where TMainSourceRepository : AnatoliRepository<TMainSource>, TIMainSourceRepository, new()
            where TIMainSourceRepository : IRepository<TMainSource>
    {
        #region Properties
        protected static log4net.ILog Logger { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid ApplicationOwnerKey { get; protected set; }
        public Guid DataOwnerKey { get; protected set; }
        public Guid DataOwnerCenterKey { get; protected set; }
        public bool GetRemovedData { get; protected set; }
        public virtual TMainSourceRepository MainRepository { get; set; }
        public AnatoliDbContext DBContext { get; set; }
        #endregion

        #region Ctors
        BusinessDomainV2()  { }
        public BusinessDomainV2(Guid applicationOwnerKey)
            : this(applicationOwnerKey, applicationOwnerKey, applicationOwnerKey, new AnatoliDbContext())
        {
        }
        public BusinessDomainV2(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey) 
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {
        }
        public BusinessDomainV2(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, (TMainSourceRepository)Activator.CreateInstance(typeof(TMainSourceRepository), dbc), new PrincipalRepository(dbc))
        {
            DBContext = dbc;

        }
        public BusinessDomainV2(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, 
                    TMainSourceRepository dataRepository, IPrincipalRepository principalRepository)
        {
            MainRepository = dataRepository;
            PrincipalRepository = principalRepository;
            ApplicationOwnerKey = applicationOwnerKey; DataOwnerKey = dataOwnerKey; DataOwnerCenterKey = dataOwnerCenterKey;
            Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        #endregion

        #region Methods
        protected TMainSourceView PostOnlineData(string webApiURI, string data, bool needReturnData = false)
        {
            var returnData = new TMainSourceView();

            try
            {
                var client = new HttpClient();

                client.SetBearerToken(InterServerCommunication.Instance.GetInternalServerToken(ApplicationOwnerKey.ToString()));

                var content = new StringContent(data, Encoding.UTF8, "application/json");

                var result = client.PostAsync(ConfigurationManager.AppSettings["InternalServer"] + webApiURI + "?ApplicationOwnerId="
                           + ApplicationOwnerKey.ToString(), content).Result;

                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Can not save order to server");
                else if (!result.IsSuccessStatusCode)
                    throw new Exception(result.Content.ReadAsStringAsync().Result);

                if (needReturnData)
                {
                    var json = result.Content.ReadAsStringAsync().Result;

                    returnData = JsonConvert.DeserializeAnonymousType(json, new TMainSourceView());
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

        protected List<TMainSourceView> GetOnlineData(string webApiURI, string queryString)
        {
            try
            {
                var client = new HttpClient();

                client.SetBearerToken(InterServerCommunication.Instance.GetInternalServerToken(ApplicationOwnerKey.ToString()));

                var result = client.GetAsync(ConfigurationManager.AppSettings["InternalServer"] + webApiURI + "?ApplicationOwnerId="
                           + ApplicationOwnerKey.ToString() + "&" + queryString).Result;

                var json = result.Content.ReadAsStringAsync().Result;

                var returnData = JsonConvert.DeserializeAnonymousType(json, new List<TMainSourceView>());

                return returnData;
            }
            catch (Exception ex)
            {
                Logger.Error("Can not read from internal server", ex);

                throw ex;
            }
        }

        public async Task<TMainSourceView> GetByIdAsync(Guid id)
        {
            if (GetRemovedData)
                return await MainRepository.GetQuery()
                    .Where(p => p.ApplicationOwnerId == ApplicationOwnerKey && p.DataOwnerId == DataOwnerKey && p.Id == id)
                    .AsNoTracking()
                    .ProjectTo<TMainSourceView>().FirstAsync();
            else
                return await MainRepository.GetQuery()
                    .Where(p => p.ApplicationOwnerId == ApplicationOwnerKey && p.DataOwnerId == DataOwnerKey && !p.IsRemoved && p.Id == id)
                    .AsNoTracking()
                    .ProjectTo<TMainSourceView>().FirstAsync(); 
        }
        public async Task<List<TMainSourceView>> GetAllAsync()
        {
            if (GetRemovedData)
                return await MainRepository.GetQuery()
                    .Where(p => p.ApplicationOwnerId == ApplicationOwnerKey && p.DataOwnerId == DataOwnerKey)
                    .AsNoTracking()
                    .ProjectTo<TMainSourceView>().ToListAsync();
            else
                return await MainRepository.GetQuery()
                    .Where(p => p.ApplicationOwnerId == ApplicationOwnerKey && p.DataOwnerId == DataOwnerKey && !p.IsRemoved)
                    .AsNoTracking()
                    .ProjectTo<TMainSourceView>().ToListAsync();
        }
        public async Task<List<TMainSourceView>> GetAllAsync(Func<TMainSource,bool> predicate)
        {
            Func<TMainSource,bool> predicate2 = null;
            if (GetRemovedData)
                predicate2 = p => p.ApplicationOwnerId == ApplicationOwnerKey && p.DataOwnerId == DataOwnerKey;
            else
                predicate2 = p => p.ApplicationOwnerId == ApplicationOwnerKey && p.DataOwnerId == DataOwnerKey && !p.IsRemoved;

            Expression<Func<TMainSource, bool>> combinedAnd = s => (predicate(s) && predicate2(s));

            return await MainRepository.GetQuery()
                .Where(combinedAnd)
                .AsNoTracking()
                .ProjectTo<TMainSourceView>().ToListAsync();
        }

        public async Task<List<TMainSourceView>> GetAllChangedAfterAsync(DateTime selectedDate)
        {
            Expression<Func<TMainSource, bool>> predicate = null;

            if (GetRemovedData)
                predicate = p => p.ApplicationOwnerId == ApplicationOwnerKey && p.DataOwnerId == DataOwnerKey && p.LastUpdate >= selectedDate;
            else
                predicate = p => p.ApplicationOwnerId == ApplicationOwnerKey && p.DataOwnerId == DataOwnerKey && !p.IsRemoved && p.LastUpdate >= selectedDate;

            return await MainRepository.GetQuery()
                .Where(predicate)
                .AsNoTracking()
                .ProjectTo<TMainSourceView>().ToListAsync();
        }

        public virtual async Task PublishAsync(List<TMainSource> data)
        {
            try
            {
                MainRepository.DbContext.Configuration.AutoDetectChangesEnabled = false;
                var dataList = GetDataListToCheckForExistsData();

                foreach (var item in data)
                {
                    var model = dataList.Find(p => p.Id == item.Id);
                    item.ApplicationOwnerId = ApplicationOwnerKey; item.DataOwnerId = DataOwnerKey; item.DataOwnerCenterId = DataOwnerCenterKey;
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
        public virtual async Task PublishAsync(TMainSource data)
        {
            var model = await MainRepository.GetByIdAsync(data.Id);
            data.ApplicationOwnerId = ApplicationOwnerKey; data.DataOwnerId = DataOwnerKey; data.DataOwnerCenterId = DataOwnerCenterKey;
            AddDataToRepository(model, data);
            await MainRepository.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(List<TMainSource> data)
        {
            foreach (var item in data)
                await MainRepository.DeleteBatchAsync(p => p.Id == item.Id);
        }

        public virtual async Task DeleteAsync(List<TMainSourceView> data)
        {
            foreach (var item in data)
                await MainRepository.DeleteBatchAsync(p => p.Id == item.UniqueId);
        }

        public Expression<Func<TMainSource, bool>> GetCondition()
        {
            return null;
        }

        public virtual async Task CheckDeletedAsync(List<TMainSourceView> dataViewModels)
        {
            try
            {
                var currentDataList = MainRepository.GetQuery()
                            .Where(p => p.ApplicationOwnerId == ApplicationOwnerKey && p.DataOwnerId == DataOwnerKey)
                            .Select(data => new TMainSourceView
                            {
                                UniqueId = data.Id
                            })
                            .AsNoTracking()
                            .ToList();

                currentDataList.ForEach(item =>
                {
                    if (dataViewModels.Find(p => p.UniqueId == item.UniqueId) == null)
                        MainRepository.GetQuery().Where(p => p.Id == item.UniqueId).Update(t => new TMainSource { LastUpdate= DateTime.Now, IsRemoved = true});
                });

                await MainRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error("CheckForDeletedAsync", ex);
                throw ex;
            }
        }

        protected virtual void AddDataToRepository(TMainSource currentData, TMainSource newItem)
        {
            throw new NotImplementedException();
        }
        public virtual List<TMainSource> GetDataListToCheckForExistsData()
        {
            return MainRepository.GetQuery()
                            .Where(p => p.DataOwnerId == DataOwnerKey && p.ApplicationOwnerId == ApplicationOwnerKey)
                            .AsNoTracking()
                            .ToList();
        }
        #endregion
    }
}
