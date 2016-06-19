using System;
using System.Linq;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.DataAccess;
using Anatoli.ViewModels.BaseModels;
using Anatoli.Common.DataAccess.Interfaces;
using Anatoli.Common.Business;
using Anatoli.Common.Business.Interfaces;

namespace Anatoli.Business.Domain
{
    public class BasketDomain : BusinessDomainV2<Basket, BasketViewModel, BasketRepository, IBasketRepository>, IBusinessDomainV2<Basket, BasketViewModel>
    {
        #region Properties
        public IRepository<BasketItem> ItemRepository { get; set; }
        #endregion

        #region Ctors
        public BasketDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public BasketDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
            ItemRepository = new BasketItemRepository(dbc);
        }            
        #endregion

        #region Methods
        public async Task<ICollection<BasketViewModel>> GetBasketByCustomerId(Guid customerId)
        {
            return await GetAllAsync(p => p.CustomerId == customerId);
        }

        public override async Task PublishAsync(List<Basket> dataListInfo)
        {
            try
            {
                foreach(Basket item in dataListInfo)
                {
                    item.ApplicationOwnerId = ApplicationOwnerKey; item.DataOwnerId = DataOwnerKey; item.DataOwnerCenterId = DataOwnerCenterKey;
                    var currentBasket = MainRepository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                    if (currentBasket != null)
                    {
                        currentBasket.BasketName = item.BasketName;
                        currentBasket.LastUpdate = DateTime.Now;
                        currentBasket = await SetBasketItemData(currentBasket, item.BasketItems.ToList(), ((AnatoliDbContext)MainRepository.DbContext));
                        await MainRepository.UpdateAsync(currentBasket);
                    }
                    else
                    {
                        item.Id = item.Id == Guid.Empty ? Guid.NewGuid(): item.Id;
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        if (item.BasketItems != null)
                        {
                            item.BasketItems.ToList().ForEach(itemDetail =>
                                {
                                    itemDetail.ApplicationOwnerId = ApplicationOwnerKey; itemDetail.DataOwnerId = DataOwnerKey; itemDetail.DataOwnerCenterId = DataOwnerCenterKey;
                                    itemDetail.CreatedDate = itemDetail.LastUpdate = item.CreatedDate;
                                    itemDetail.Id = itemDetail.Id == Guid.Empty ? Guid.NewGuid() : itemDetail.Id;
                                });
                        }
                        await MainRepository.AddAsync(item);
                    }
                };
                await MainRepository.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Logger.Error("PublishAsync", ex);
                throw ex;
            }
        }

        public override async Task DeleteAsync(List<Basket> dataListInfo)
        {
                foreach( var item in dataListInfo)
                {
                    if (item.Id != BasketViewModel.CheckOutBasketTypeId &&
                        item.Id != BasketViewModel.FavoriteBasketTypeId &&
                        item.Id != BasketViewModel.IncompleteBasketTypeId)
                    {
                        await MainRepository.DeleteBatchAsync(p => p.Id == item.Id);
                    }
                };
        }

        public async Task<Basket> SetBasketItemData(Basket data, List<BasketItem> basketItems, AnatoliDbContext context)
        {
            await ItemRepository.DeleteBatchAsync(p => p.BasketId == data.Id);

            foreach (BasketItem item in basketItems)
            { 
                item.BasketId = data.Id;
                item.ApplicationOwnerId = data.ApplicationOwnerId;
                item.CreatedDate = item.LastUpdate = data.CreatedDate;
                item.Id = Guid.NewGuid();
                await ItemRepository.AddAsync(item);
            };

            await ItemRepository.SaveChangesAsync();
            return data;
        }

        #endregion
    }
}
