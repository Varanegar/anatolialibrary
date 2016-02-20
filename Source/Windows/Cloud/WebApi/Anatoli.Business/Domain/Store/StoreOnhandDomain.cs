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

namespace Anatoli.Business.Domain
{
    public class StoreActiveOnhandDomain : BusinessDomain<StoreActiveOnhandViewModel>, IBusinessDomain<StoreActiveOnhand, StoreActiveOnhandViewModel>
    {
        #region Properties
        public IAnatoliProxy<StoreActiveOnhand, StoreActiveOnhandViewModel> Proxy { get; set; }
        public IRepository<StoreActiveOnhand> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        StoreActiveOnhandDomain() { }
        public StoreActiveOnhandDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public StoreActiveOnhandDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new StoreActiveOnhandRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<StoreActiveOnhand, StoreActiveOnhandViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public StoreActiveOnhandDomain(IRepository<StoreActiveOnhand> repository, IPrincipalRepository principalRepository, IAnatoliProxy<StoreActiveOnhand, StoreActiveOnhandViewModel> proxy)
        {
            Proxy = proxy;
            Repository = repository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<StoreActiveOnhandViewModel>> GetAll()
        {
            var storeActiveOnhands = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(storeActiveOnhands.ToList());
        }

        public async Task<List<StoreActiveOnhandViewModel>> GetAllByStoreId(string storeId)
        {
            Guid storeGuid = Guid.Parse(storeId);
            var storeActiveOnhands = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.StoreId == storeGuid);

            return Proxy.Convert(storeActiveOnhands.ToList());
        }
        public async Task<List<StoreActiveOnhandViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var storeActiveOnhands = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(storeActiveOnhands.ToList()); ;
        }
        public async Task<List<StoreActiveOnhandViewModel>> GetAllByStoreIdChangedAfter(string id, DateTime selectedDate)
        {
            Guid storeGuid = Guid.Parse(id);
            var storeActiveOnhands = await Repository.FindAllAsync(p => p.StoreId == storeGuid && p.LastUpdate >= selectedDate);

            return Proxy.Convert(storeActiveOnhands.ToList()); ;
        }
        public async Task<List<StoreActiveOnhandViewModel>> GetAllByStoreIdOnLine(string id)
        {
            var returnData = new List<StoreActiveOnhandViewModel>();
            
            await Task.Factory.StartNew(() =>
            {
                returnData = GetOnlineData(WebApiURIHelper.GetStoreOnHandLocalURI, "id=" + id);
            });
            
            return returnData;
        }

        public async Task<List<StoreActiveOnhandViewModel>> PublishAsync(List<StoreActiveOnhandViewModel> dataViewModels)
        {
            try
            {
                Repository.DbContext.Configuration.AutoDetectChangesEnabled = false;

                var storeActiveOnhands = Proxy.ReverseConvert(dataViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                var currentStoreAciveOnHands = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId).ToList();

                storeActiveOnhands.ForEach(item =>
                {
                    var currentStoreAciveOnHand = currentStoreAciveOnHands.Find(p => p.StoreId == item.StoreId && p.ProductId == item.ProductId);
                    if (currentStoreAciveOnHand != null)
                    {
                        if (currentStoreAciveOnHand.Qty != item.Qty)
                        {
                            currentStoreAciveOnHand.Qty = item.Qty;
                            currentStoreAciveOnHand.LastUpdate = DateTime.Now;
                            Repository.Update(currentStoreAciveOnHand);
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
        public async Task<List<StoreActiveOnhandViewModel>> CheckDeletedAsync(List<StoreActiveOnhandViewModel> dataViewModels)
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
        public async Task<List<StoreActiveOnhandViewModel>> Delete(List<StoreActiveOnhandViewModel> dataViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var storeActiveOnhands = Proxy.ReverseConvert(dataViewModels);

                storeActiveOnhands.ForEach(item =>
                {
                    var data = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();

                    Repository.DbContext.StoreActiveOnhands.Remove(data);
                });

                Repository.SaveChangesAsync();
            });

            return dataViewModels;
        }
        #endregion

    }
}
