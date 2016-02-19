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
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.Business.Domain
{
    public class BasketItemDomain : BusinessDomain<BasketItemViewModel>, IBusinessDomain<BasketItem, BasketItemViewModel>
    {
        #region Properties
        public IAnatoliProxy<BasketItem, BasketItemViewModel> Proxy { get; set; }
        public IRepository<BasketItem> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        BasketItemDomain() { }
        public BasketItemDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public BasketItemDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new BasketItemRepository(dbc),new PrincipalRepository(dbc), AnatoliProxy<BasketItem, BasketItemViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public BasketItemDomain(IBasketItemRepository basketItemRepository, IPrincipalRepository principalRepository, IAnatoliProxy<BasketItem, BasketItemViewModel> proxy)
        {
            Proxy = proxy;
            Repository = basketItemRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<BasketItemViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }
        public async Task<List<BasketItemViewModel>> GetByIds(List<BasketItemViewModel> data)
        {
            foreach(var item in data)
            {
                var value = await Repository.FindAsync(p => p.BasketId == item.BasketId && p.ProductId == item.ProductId);
                if(value !=  null) item.Qty = value.Qty;
            };
            return data;
        }
        public async Task<List<BasketItemViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BasketItemViewModel>> PublishAsync(List<BasketItemViewModel> dataViewModels)
        {
            try
            {
                var dataListInfo = Proxy.ReverseConvert(dataViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                foreach (BasketItem item in dataListInfo)
                {
                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    var currentBasket = Repository.GetQuery().Where(p => p.ProductId == item.ProductId && p.BasketId == item.BasketId).FirstOrDefault();
                    if (currentBasket != null)
                    {
                        currentBasket.ProductId = item.ProductId;
                        currentBasket.Qty = item.Qty;
                        currentBasket.LastUpdate = DateTime.Now;
                        await Repository.UpdateAsync(currentBasket);
                    }
                    else
                    {
                        item.Id = item.Id == Guid.Empty ? Guid.NewGuid() : item.Id;
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        await Repository.AddAsync(item);
                    }
                };
                await Repository.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                log.Error("PublishAsync", ex);
                throw ex;
            }
            return dataViewModels;

        }

        public async Task<List<BasketItemViewModel>> ChangeAsync(List<BasketItemViewModel> dataViewModels)
        {
            try
            {
                var dataListInfo = Proxy.ReverseConvert(dataViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                foreach (BasketItem item in dataListInfo)
                {
                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    var currentBasket = Repository.GetQuery().Where(p => p.ProductId == item.ProductId && p.BasketId == item.BasketId).FirstOrDefault();
                    if (currentBasket != null)
                    {
                        currentBasket.ProductId = item.ProductId;
                        currentBasket.Qty += item.Qty;
                        currentBasket.LastUpdate = DateTime.Now;
                        await Repository.UpdateAsync(currentBasket);
                    }
                    else
                    {
                        item.Id = item.Id == Guid.Empty ? Guid.NewGuid() : item.Id;
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        await Repository.AddAsync(item);
                    }
                };
                await Repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.Error("ChangeAsync", ex);
                throw ex;
            }
            return dataViewModels;

        }

        public async Task<List<BasketItemViewModel>> Delete(List<BasketItemViewModel> dataViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var dataListInfo = Proxy.ReverseConvert(dataViewModels);

                dataListInfo.ForEach(item =>
                {
                    var basketItem = Repository.GetQuery().Where(p => p.ProductId == item.ProductId && p.BasketId == item.BasketId).FirstOrDefault();

                    Repository.DeleteAsync(basketItem);
                });

                Repository.SaveChangesAsync();
            });
            return dataViewModels;
        }
        #endregion
    }
}
