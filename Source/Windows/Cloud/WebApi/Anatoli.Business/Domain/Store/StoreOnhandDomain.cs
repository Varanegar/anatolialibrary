using System;
using System.Linq;
using Anatoli.Business.Proxy;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess;
using Anatoli.ViewModels.StoreModels;
using EntityFramework.Extensions;
using Anatoli.Business.Helpers;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Anatoli.Business.Domain
{
    public class StoreActiveOnhandDomain : BusinessDomainV2<StoreActiveOnhand, StoreActiveOnhandViewModel, StoreActiveOnhandRepository, IStoreActiveOnhandRepository>, IBusinessDomainV2<StoreActiveOnhand, StoreActiveOnhandViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public StoreActiveOnhandDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public StoreActiveOnhandDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        public async Task<ICollection<StoreActiveOnhandViewModel>> GetAllByStoreId(Guid storeGuid)
        {
            return await GetAllAsync(p => p.StoreId == storeGuid);
        }
        public async Task<ICollection<StoreActiveOnhandViewModel>> GetAllByStoreIdChangedAfter(Guid storeGuid, DateTime selectedDate)
        {
            return await GetAllAsync(p => p.StoreId == storeGuid && p.LastUpdate >= selectedDate);
        }
        public List<StoreActiveOnhandViewModel> GetAllByStoreIdOnLine(Guid id)
        {
           return GetOnlineData(WebApiURIHelper.GetStoreOnHandLocalURI, "id=" + id);
        }
        public override async Task PublishAsync(List<StoreActiveOnhand> data)
        {
            try
            {
                MainRepository.DbContext.Configuration.AutoDetectChangesEnabled = false;
                var dataList = GetDataListToCheckForExistsData();

                foreach (var item in data)
                {
                    var model = dataList.Find(p => p.StoreId == item.StoreId && p.ProductId == item.ProductId);
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
        protected override void AddDataToRepository(StoreActiveOnhand currentStoreAciveOnHand, StoreActiveOnhand item)
        {
            if (currentStoreAciveOnHand != null)
            {
                if (currentStoreAciveOnHand.Qty != item.Qty)
                {
                    currentStoreAciveOnHand.Qty = item.Qty;
                    currentStoreAciveOnHand.LastUpdate = DateTime.Now;
                    MainRepository.Update(currentStoreAciveOnHand);
                }
            }
            else
            {
                item.Id = Guid.NewGuid();
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                MainRepository.Add(item);
            }
        }
        public override async Task CheckDeletedAsync(List<StoreActiveOnhandViewModel> dataViewModels)
        {
            try
            {
                MainRepository.DbContext.Configuration.AutoDetectChangesEnabled = false;
                var currentDataList = MainRepository.GetQuery()
                            .Where(p => p.ApplicationOwnerId == ApplicationOwnerKey && p.DataOwnerId == DataOwnerKey)
                            .Select(data => new StoreActiveOnhandViewModel
                            {
                                UniqueId = data.Id,
                                StoreGuid = data.StoreId,
                                ProductGuid = data.ProductId
                            })
                            .AsNoTracking()
                            .ToList();

                currentDataList.ForEach(item =>
                {
                    if (dataViewModels.Find(p => p.StoreGuid == item.StoreGuid && p.ProductGuid == item.ProductGuid) == null)
                        MainRepository.GetQuery().Where(p => p.StoreId == item.StoreGuid && p.ProductId == item.ProductGuid).Update(t => new StoreActiveOnhand { LastUpdate = DateTime.Now, IsRemoved = true });
                });

                await MainRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error("CheckForDeletedAsync", ex);
                throw ex;
            }
            finally
            {
                MainRepository.DbContext.Configuration.AutoDetectChangesEnabled = true;
                Logger.Info("PublishAsync Finish" + dataViewModels.Count);
            }
        }        
        #endregion

    }
}
