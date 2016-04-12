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
using Anatoli.ViewModels.Order;

namespace Anatoli.Business.Domain
{
    public class IncompletePurchaseOrderLineItemDomain : BusinessDomainV2<IncompletePurchaseOrderLineItem, IncompletePurchaseOrderLineItemViewModel, IncompletePurchaseOrderLineItemRepository, IIncompletePurchaseOrderLineItemRepository>, IBusinessDomainV2<IncompletePurchaseOrderLineItem, IncompletePurchaseOrderLineItemViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public IncompletePurchaseOrderLineItemDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public IncompletePurchaseOrderLineItemDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        public async Task<ICollection<IncompletePurchaseOrderLineItem>> PublishAsync(List<IncompletePurchaseOrderLineItem> lineItems)
        {
            try
            {
                foreach (IncompletePurchaseOrderLineItem item in lineItems)
                {
                    item.ApplicationOwnerId = ApplicationOwnerKey; item.DataOwnerId = DataOwnerKey; item.DataOwnerCenterId = DataOwnerCenterKey;
                    var currentBasket = MainRepository.GetQuery().Where(p => p.ProductId == item.ProductId && p.IncompletePurchaseOrderId == item.IncompletePurchaseOrderId).FirstOrDefault();
                    if (currentBasket != null)
                    {
                        currentBasket.ProductId = item.ProductId;
                        currentBasket.Qty = item.Qty;
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
            catch(Exception ex)
            {
                Logger.Error("PublishAsync", ex);
                throw ex;
            }
            return lineItems;

        }

        public async Task<ICollection<IncompletePurchaseOrderLineItem>> ChangeAsync(List<IncompletePurchaseOrderLineItem> lineItems)
        {
            try
            {
                foreach (IncompletePurchaseOrderLineItem item in lineItems)
                {
                    item.ApplicationOwnerId = ApplicationOwnerKey; item.DataOwnerId = DataOwnerKey; item.DataOwnerCenterId = DataOwnerCenterKey;
                    var currentBasket = MainRepository.GetQuery().Where(p => p.ProductId == item.ProductId && p.IncompletePurchaseOrderId == item.IncompletePurchaseOrderId).FirstOrDefault();
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
            return lineItems;

        }
        public async Task DeleteByProductIdAsync(List<IncompletePurchaseOrderLineItemViewModel> data)
        {
            foreach (var item in data)
                await MainRepository.DeleteBatchAsync(p => p.IncompletePurchaseOrderId == item.IncompletePurchaseOrderId && p.ProductId == item.ProductId);
        }

        #endregion
    }
}
