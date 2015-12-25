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

namespace Anatoli.Business.Domain
{
    public class StoreActivePriceListDomain : IBusinessDomain<StoreActivePriceList, StoreActivePriceListViewModel>
    {

        #region Properties
        public IAnatoliProxy<StoreActivePriceList, StoreActivePriceListViewModel> Proxy { get; set; }
        public IRepository<StoreActivePriceList> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        StoreActivePriceListDomain() { }
        public StoreActivePriceListDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public StoreActivePriceListDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new StoreActivePriceListRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<StoreActivePriceList, StoreActivePriceListViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public StoreActivePriceListDomain(IRepository<StoreActivePriceList> repository, IPrincipalRepository principalRepository, IAnatoliProxy<StoreActivePriceList, StoreActivePriceListViewModel> proxy)
        {
            Proxy = proxy;
            Repository = repository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<StoreActivePriceListViewModel>> GetAll()
        {
            var storeCalendars = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(storeCalendars.ToList()); 
        }

        public async Task<List<StoreActivePriceListViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var storeCalendars = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(storeCalendars.ToList()); ;
        }

        public async Task PublishAsync(List<StoreActivePriceListViewModel> StoreActivePriceListViewModels)
        {
            var storeCalendars = Proxy.ReverseConvert(StoreActivePriceListViewModels);
            var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

            storeCalendars.ForEach(item =>
            {
                var currentStoreActivePriceLists = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();
                if (currentStoreActivePriceLists != null)
                {
                    currentStoreActivePriceLists.StoreId = item.StoreId;
                    currentStoreActivePriceLists.ProductId = item.ProductId;
                    currentStoreActivePriceLists.Price = item.Price;
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

        public async Task Delete(List<StoreActivePriceListViewModel> storeCalendarViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var storeCalendars = Proxy.ReverseConvert(storeCalendarViewModels);

                storeCalendars.ForEach(item =>
                {
                    var product = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();

                    Repository.DeleteAsync(product);
                });

                Repository.SaveChangesAsync();
            });
        }
        #endregion
    }
}
