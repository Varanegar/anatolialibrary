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
    public class StoreDomain : IBusinessDomain<Store, StoreViewModel>
        {

            #region Properties
            public IAnatoliProxy<Store, StoreViewModel> Proxy { get; set; }
            public IRepository<Store> Repository { get; set; }
            public IPrincipalRepository PrincipalRepository { get; set; }
            public Guid PrivateLabelOwnerId { get; private set; }

            #endregion

            #region Ctors
            StoreDomain() { }
            public StoreDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
            public StoreDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
                : this(new StoreRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<Store, StoreViewModel>.Create())
            {
                PrivateLabelOwnerId = privateLabelOwnerId;
            }
            public StoreDomain(IRepository<Store> repository, IPrincipalRepository principalRepository, IAnatoliProxy<Store, StoreViewModel> proxy)
            {
                Proxy = proxy;
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
                var stores = Proxy.ReverseConvert(StoreViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                stores.ForEach(item =>
                {
                    var currentStore = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();
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
                        currentStore = SetStoreCalendarData(currentStore, item.StoreCalendars.ToList(), Repository.DbContext);
                        currentStore = SetStoreRegionData(currentStore, item.StoreValidRegionInfoes.ToList(), Repository.DbContext);
                    }
                    else
                    {
                        item.Id = Guid.NewGuid();
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                    }
                    Repository.AddAsync(item);
                });
                await Repository.SaveChangesAsync();
            }

            public async Task Delete(List<StoreViewModel> StoreViewModels)
            {
                await Task.Factory.StartNew(() =>
                {
                    var stores = Proxy.ReverseConvert(StoreViewModels);

                    stores.ForEach(item =>
                    {
                        var product = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();
                       
                        Repository.DeleteAsync(product);
                    });

                    Repository.SaveChangesAsync();
                });
            }

            public Store SetStoreCalendarData(Store data, List<StoreCalendar> storeCalendars, AnatoliDbContext context)
            {
                data.StoreCalendars.Clear();
                storeCalendars.ForEach(item =>
                {
                    item.PrivateLabelOwner = data.PrivateLabelOwner;
                    item.CreatedDate = item.LastUpdate = data.CreatedDate;
                    data.StoreCalendars.Add(item);
                });
                return data;
            }

            public Store SetStoreRegionData(Store data, List<CityRegion> storeRegions, AnatoliDbContext context)
            {
                data.StoreValidRegionInfoes.Clear();
                storeRegions.ForEach(item =>
                {
                    item.PrivateLabelOwner = data.PrivateLabelOwner;
                    item.CreatedDate = item.LastUpdate = data.CreatedDate;
                    data.StoreValidRegionInfoes.Add(item);
                });
                return data;
            }

            #endregion
        }
}
