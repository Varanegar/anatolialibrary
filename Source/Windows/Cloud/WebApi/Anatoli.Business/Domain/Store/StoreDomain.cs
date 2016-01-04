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
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.Business.Domain
{
    public class StoreDomain : BusinessDomain<StoreViewModel>, IBusinessDomain<Store, StoreViewModel>
    {

        #region Properties
            public IAnatoliProxy<Store, StoreViewModel> Proxy { get; set; }
            public IRepository<Store> Repository { get; set; }
            public IRepository<CityRegion> CityRegionRepository { get; set; }
            public IRepository<StoreCalendar> StoreCalendarRepository { get; set; }
            public IPrincipalRepository PrincipalRepository { get; set; }
            public Guid PrivateLabelOwnerId { get; private set; }

            #endregion

        #region Ctors
        StoreDomain() { }
        public StoreDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public StoreDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new StoreRepository(dbc), new CityRegionRepository(dbc), new StoreCalendarRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<Store, StoreViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public StoreDomain(IRepository<Store> repository, IRepository<CityRegion> cityRegionRepository, IRepository<StoreCalendar> storeCalendarRepository, IPrincipalRepository principalRepository, IAnatoliProxy<Store, StoreViewModel> proxy)
        {
            Proxy = proxy;
            Repository = repository;
            CityRegionRepository = cityRegionRepository;
            StoreCalendarRepository = storeCalendarRepository;
            Repository = repository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<StoreViewModel>> GetAll()
        {
            var stores = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(stores.ToList()); ;
        }

        public async Task<List<StoreViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var stores = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(stores.ToList()); ;
        }

        public async Task PublishAsync(List<StoreViewModel> StoreViewModels)
        {
            try
            {
                var stores = Proxy.ReverseConvert(StoreViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                foreach (Store item in stores)
                {
                    var currentStore = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
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
                        currentStore = await SetStoreCalendarData(currentStore, item.StoreCalendars.ToList(), Repository.DbContext);
                        currentStore = await SetStoreRegionData(currentStore, item.StoreValidRegionInfoes.ToList(), Repository.DbContext);
                        await Repository.UpdateAsync(currentStore);
                    }
                    else
                    {
                        item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        var currentNewStore = await SetStoreCalendarData(item, item.StoreCalendars.ToList(), Repository.DbContext);
                        currentNewStore = await SetStoreRegionData(currentNewStore, item.StoreValidRegionInfoes.ToList(), Repository.DbContext);
                        await Repository.AddAsync(currentNewStore);
                    }
                }
                await Repository.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
                log.Error("PublishAsync", ex);
            }
        }

        public async Task Delete(List<StoreViewModel> StoreViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var stores = Proxy.ReverseConvert(StoreViewModels);

                stores.ForEach(item =>
                {
                    var product = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                       
                    Repository.DeleteAsync(product);
                });

                Repository.SaveChangesAsync();
            });
        }

        public async Task<Store> SetStoreCalendarData(Store data, List<StoreCalendar> storeCalendars, AnatoliDbContext context)
        {
            Repository.DbContext.Database.ExecuteSqlCommand("delete from StoreCalendars where StoreId='" + data.Id + "'");

            foreach (StoreCalendar item in storeCalendars)
            {
                item.StoreId = data.Id;
                item.PrivateLabelOwner = data.PrivateLabelOwner;
                item.CreatedDate = item.LastUpdate = data.CreatedDate;
                //item.Id = Guid.NewGuid();
                await StoreCalendarRepository.AddAsync(item);
            };
            return data;
        }

        public async Task<Store> SetStoreRegionData(Store data, List<CityRegion> storeRegions, AnatoliDbContext context)
        {
            await Task.Factory.StartNew(() =>
            {

                Repository.DbContext.Database.ExecuteSqlCommand("delete from StoreValidRegionInfoes where StoreId='" + data.Id + "'");
                data.StoreValidRegionInfoes.Clear();
                storeRegions.ForEach(item =>
                {
                    var region = CityRegionRepository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                    if (region != null)
                    {
                        data.StoreValidRegionInfoes.Add(region);
                    }
                });
            });
            return data;
        }

        #endregion
    }
}
