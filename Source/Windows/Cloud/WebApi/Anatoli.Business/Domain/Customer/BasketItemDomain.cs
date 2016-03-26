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
    public class BasketItemDomain : BusinessDomainV2<BasketItem, BasketItemViewModel, BasketItemRepository, IBasketItemRepository>, IBusinessDomainV2<BasketItem, BasketItemViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public BasketItemDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public BasketItemDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        public async Task<List<BasketItemViewModel>> GetByIds(List<BasketItemViewModel> data)
        {
            foreach(var item in data)
            {
                var value = await MainRepository.FindAsync(p => p.BasketId == item.BasketId && p.ProductId == item.ProductId);
                if(value !=  null) item.Qty = value.Qty;
            };
            return data;
        }
        protected override void AddDataToRepository(BasketItem currentBasket, BasketItem item)
        {
            if (currentBasket != null)
            {
                currentBasket.ProductId = item.ProductId;
                currentBasket.Qty = item.Qty;
                currentBasket.LastUpdate = DateTime.Now;
                MainRepository.Update(currentBasket);
            }
            else
            {
                item.Id = item.Id == Guid.Empty ? Guid.NewGuid() : item.Id;
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                MainRepository.Add(item);
            }
        }
        public async Task<ICollection<BasketItem>> ChangeAsync(List<BasketItem> dataListInfo)
        {
            try
            {
                foreach (BasketItem item in dataListInfo)
                {
                    item.ApplicationOwnerId = ApplicationOwnerKey; item.DataOwnerId = DataOwnerKey; item.DataOwnerCenterId = DataOwnerCenterKey;
                    var currentBasket = MainRepository.GetQuery().Where(p => p.ProductId == item.ProductId && p.BasketId == item.BasketId).FirstOrDefault();
                    if (currentBasket != null)
                    {
                        currentBasket.ProductId = item.ProductId;
                        currentBasket.Qty += item.Qty;
                        currentBasket.LastUpdate = DateTime.Now;
                        await MainRepository.UpdateAsync(currentBasket);
                    }
                    else
                    {
                        item.Id = item.Id == Guid.Empty ? Guid.NewGuid() : item.Id;
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        await MainRepository.AddAsync(item);
                    }
                };
                await MainRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error("ChangeAsync", ex);
                throw ex;
            }
            return dataListInfo;

        }

        #endregion
    }
}
