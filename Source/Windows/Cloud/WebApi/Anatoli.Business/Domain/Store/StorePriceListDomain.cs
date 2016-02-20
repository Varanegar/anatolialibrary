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
    public class StoreActivePriceListDomain : BusinessDomain<StoreActivePriceListViewModel>, IBusinessDomain<StoreActivePriceList, StoreActivePriceListViewModel>
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
        public async Task<List<StoreActivePriceListViewModel>> GetAllByStoreId(string id)
        {
            Guid storeGuid = Guid.Parse(id);
            var storePriceLists = await Repository.FindAllAsync(p => p.StoreId == storeGuid);

            return Proxy.Convert(storePriceLists.ToList());
        }
        public async Task<List<StoreActivePriceListViewModel>> GetAllByStoreIdChangedAfter(string id, DateTime selectedDate)
        {
            Guid storeGuid = Guid.Parse(id);
            var storePriceLists = await Repository.FindAllAsync(p => p.StoreId == storeGuid && p.LastUpdate >= selectedDate);

            return Proxy.Convert(storePriceLists.ToList());
        }

        public async Task<List<StoreActivePriceListViewModel>> GetAll()
        {
            var storePriceLists = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(storePriceLists.ToList()); 
        }

        public async Task<List<StoreActivePriceListViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var storePriceLists = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(storePriceLists.ToList()); ;
        }

        public async Task<List<StoreActivePriceListViewModel>> PublishAsync(List<StoreActivePriceListViewModel> dataViewModels)
        {
            try
            {
                Repository.DbContext.Configuration.AutoDetectChangesEnabled = false;

                var storePriceLists = Proxy.ReverseConvert(dataViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                var currentStoreActivePriceLists = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId).ToList();

                storePriceLists.ForEach(item =>
                {
                    var currentStoreActivePriceList = currentStoreActivePriceLists.Find(p => p.StoreId == item.StoreId && p.ProductId == item.ProductId);
                    if (currentStoreActivePriceList != null)
                    {
                        if (currentStoreActivePriceList.Price != item.Price)
                        {
                            currentStoreActivePriceList.Price = item.Price;
                            currentStoreActivePriceList.LastUpdate = DateTime.Now;
                            Repository.Update(currentStoreActivePriceList);
                        }
                    }
                    else
                    {
                        item.Id = Guid.NewGuid();
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                        Repository.Add(item);
                    }
                });
                await Repository.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                log.Error("PublishAsync", ex);
                throw ex;
            }
            finally
            {
                Repository.DbContext.Configuration.AutoDetectChangesEnabled = true;
                log.Info("PublishAsync Finish" + dataViewModels.Count);
            }

            return dataViewModels;
        }
        public async Task<List<StoreActivePriceListViewModel>> CheckDeletedAsync(List<StoreActivePriceListViewModel> dataViewModels)
        {
            try
            {
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();
                var currentDataList = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId).ToList();

                currentDataList.ForEach(item =>
                {
                    if (dataViewModels.Find(p => p.UniqueId == item.Id) == null)
                    {
                        item.LastUpdate = DateTime.Now;
                        item.IsRemoved = true;
                        Repository.UpdateAsync(item);
                    }
                });

                await Repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.Error("CheckForDeletedAsync", ex);
                throw ex;
            }

            return dataViewModels;
        }

        public async Task<List<StoreActivePriceListViewModel>> Delete(List<StoreActivePriceListViewModel> dataViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var storePriceLists = Proxy.ReverseConvert(dataViewModels);

                storePriceLists.ForEach(item =>
                {
                    var data = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();

                    Repository.DbContext.StoreActivePriceLists.Remove(data);
                });

                Repository.SaveChangesAsync();
            });

            return dataViewModels;
        }
        #endregion
    }
}
