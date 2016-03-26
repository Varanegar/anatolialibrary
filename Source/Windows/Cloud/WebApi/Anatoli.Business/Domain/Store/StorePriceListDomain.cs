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
using AutoMapper.QueryableExtensions;
using EntityFramework.Extensions;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Anatoli.Business.Domain
{
    public class StoreActivePriceListDomain : BusinessDomainV2<StoreActivePriceList, StoreActivePriceListViewModel, StoreActivePriceListRepository, IStoreActivePriceListRepository>, IBusinessDomainV2<StoreActivePriceList, StoreActivePriceListViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public StoreActivePriceListDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public StoreActivePriceListDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        public async Task<ICollection<StoreActivePriceListViewModel>> GetAllByStoreId(Guid storeGuid)
        {
            return await GetAllAsync(p => p.StoreId == storeGuid);
        }
        public async Task<ICollection<StoreActivePriceListViewModel>> GetAllByStoreIdChangedAfterAsync(Guid storeGuid, DateTime selectedDate)
        {
            return await GetAllAsync(p => p.StoreId == storeGuid && p.LastUpdate >= selectedDate);

        }
        public override async Task PublishAsync(List<StoreActivePriceList> data)
        {
            try
            {
                MainRepository.DbContext.Configuration.AutoDetectChangesEnabled = false;
                var dataList = GetDataListToCheckForExistsData();

                foreach (var item in data)
                {
                    var model = dataList.Find(p => p.StoreId == item.StoreId && p.ProductId == item.StoreId);
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

        public override async Task CheckDeletedAsync(List<StoreActivePriceListViewModel> dataViewModels)
        {
            try
            {
                var currentDataList = MainRepository.GetQuery()
                            .Where(p => p.ApplicationOwnerId == ApplicationOwnerKey && p.DataOwnerId == DataOwnerKey)
                            .Select(data => new StoreActivePriceListViewModel
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
                        MainRepository.GetQuery().Where(p => p.StoreId == item.StoreGuid && p.ProductId == item.ProductGuid).Update(t => new StoreActivePriceList { LastUpdate = DateTime.Now, IsRemoved = true });
                });

                await MainRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error("CheckForDeletedAsync", ex);
                throw ex;
            }
        }

        protected override void AddDataToRepository(StoreActivePriceList currentStoreActivePriceList, StoreActivePriceList item)
        {
            if (currentStoreActivePriceList != null)
            {
                if (currentStoreActivePriceList.Price != item.Price)
                {
                    currentStoreActivePriceList.Price = item.Price;
                    currentStoreActivePriceList.LastUpdate = DateTime.Now;
                    MainRepository.Update(currentStoreActivePriceList);
                }
            }
            else
            {
                item.Id = Guid.NewGuid();
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                MainRepository.Add(item);
            }
        }

        #endregion
    }
}
