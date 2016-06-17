using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.DataAccess;
using Anatoli.ViewModels.StoreModels;
using Anatoli.Common.DataAccess.Interfaces;
using Anatoli.Common.Business;
using Anatoli.Common.Business.Interfaces;

namespace Anatoli.Business.Domain
{
    public class StoreDomain : BusinessDomainV2<Store, StoreViewModel, StoreRepository, IStoreRepository>, IBusinessDomainV2<Store, StoreViewModel>
    {

        #region Properties
        public IRepository<CityRegion> CityRegionRepository { get; set; }
        public IRepository<StoreCalendar> StoreCalendarRepository { get; set; }
        #endregion

        #region Ctors
        public StoreDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public StoreDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
            CityRegionRepository = new CityRegionRepository(dbc);
            StoreCalendarRepository = new StoreCalendarRepository(dbc);
        }
        #endregion

        #region Methods
        protected override void AddDataToRepository(Store currentStore, Store item)
        {
            if (currentStore != null)
            {
                currentStore.StoreCode = item.StoreCode;
                currentStore.StoreName = item.StoreName;
                currentStore.Address = item.Address;
                currentStore.HasDelivery = item.HasDelivery;
                currentStore.HasCourier = item.HasCourier;
                currentStore.SupportAppOrder = item.SupportAppOrder;
                currentStore.SupportCallCenterOrder = item.SupportCallCenterOrder;
                currentStore.SupportWebOrder = item.SupportWebOrder;
                currentStore.Lat = item.Lat;
                currentStore.Lng = item.Lng;
                currentStore.LastUpdate = DateTime.Now;
                currentStore = SetStoreRegionData(currentStore, item.StoreValidRegionInfoes.ToList(), ((AnatoliDbContext)DBContext));
                if(item.StoreCalendars != null)
                    currentStore = SetStoreCalendarData(currentStore, item.StoreCalendars.ToList(), ((AnatoliDbContext)DBContext));
                MainRepository.Update(currentStore);
            }
            else
            {
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                item = SetStoreRegionData(item, item.StoreValidRegionInfoes.ToList(), ((AnatoliDbContext)DBContext));
                if(item.StoreCalendars != null)
                    item = SetStoreCalendarData(item, item.StoreCalendars.ToList(), ((AnatoliDbContext)DBContext));
                MainRepository.Add(item);
            }

        }

        public Store SetStoreRegionData(Store data, List<CityRegion> storeRegions, AnatoliDbContext context)
        {
            DBContext.Database.ExecuteSqlCommand("delete from StoreValidRegionInfoes where StoreId='" + data.Id + "'");
            data.StoreValidRegionInfoes.Clear();
            storeRegions.ForEach(item =>
            {
                var region = CityRegionRepository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                if (region != null)
                {
                    data.StoreValidRegionInfoes.Add(region);
                }
            });
            return data;
        }


        public Store SetStoreCalendarData(Store data, List<StoreCalendar> storeCalendars, AnatoliDbContext context)
        {
            DBContext.Database.ExecuteSqlCommand("delete from StoreCalendars where StoreId='" + data.Id + "'");
            data.StoreCalendars.Clear();
            storeCalendars.ForEach(item =>
            {
                item.ApplicationOwnerId = data.ApplicationOwnerId;
                item.DataOwnerCenterId = data.DataOwnerCenterId;
                item.DataOwnerId = data.DataOwnerId;
                item.StoreId = data.Id;
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                StoreCalendarRepository.Add(item);
            });
            return data;
        }
        #endregion
    }
}
