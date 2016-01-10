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
    public class IncompletePurchaseOrderLineItemDomain : BusinessDomain<IncompletePurchaseOrderLineItemViewModel>, IBusinessDomain<IncompletePurchaseOrderLineItem, IncompletePurchaseOrderLineItemViewModel>
    {
        #region Properties
        public IAnatoliProxy<IncompletePurchaseOrderLineItem, IncompletePurchaseOrderLineItemViewModel> Proxy { get; set; }
        public IRepository<IncompletePurchaseOrderLineItem> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        IncompletePurchaseOrderLineItemDomain() { }
        public IncompletePurchaseOrderLineItemDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public IncompletePurchaseOrderLineItemDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new IncompletePurchaseOrderLineItemRepository(dbc),new PrincipalRepository(dbc), AnatoliProxy<IncompletePurchaseOrderLineItem, IncompletePurchaseOrderLineItemViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public IncompletePurchaseOrderLineItemDomain(IIncompletePurchaseOrderLineItemRepository basketItemRepository, IPrincipalRepository principalRepository, IAnatoliProxy<IncompletePurchaseOrderLineItem, IncompletePurchaseOrderLineItemViewModel> proxy)
        {
            Proxy = proxy;
            Repository = basketItemRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<IncompletePurchaseOrderLineItemViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }
        public async Task<List<IncompletePurchaseOrderLineItemViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            throw new NotImplementedException();
        }

        public async Task<List<IncompletePurchaseOrderLineItemViewModel>> PublishAsync(List<IncompletePurchaseOrderLineItemViewModel> dataViewModels)
        {
            try
            {
                var lineItems = Proxy.ReverseConvert(dataViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                foreach (IncompletePurchaseOrderLineItem item in lineItems)
                {
                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    var currentBasket = Repository.GetQuery().Where(p => p.ProductId == item.ProductId && p.IncompletePurchaseOrderId == item.IncompletePurchaseOrderId).FirstOrDefault();
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

        public async Task<List<IncompletePurchaseOrderLineItemViewModel>> ChangeAsync(List<IncompletePurchaseOrderLineItemViewModel> dataViewModels)
        {
            try
            {
                var lineItems = Proxy.ReverseConvert(dataViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                foreach (IncompletePurchaseOrderLineItem item in lineItems)
                {
                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    var currentBasket = Repository.GetQuery().Where(p => p.ProductId == item.ProductId && p.IncompletePurchaseOrderId == item.IncompletePurchaseOrderId).FirstOrDefault();
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

        public async Task<List<IncompletePurchaseOrderLineItemViewModel>> Delete(List<IncompletePurchaseOrderLineItemViewModel> dataViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var basketItems = Proxy.ReverseConvert(dataViewModels);

                basketItems.ForEach(item =>
                {
                    var basketItem = Repository.GetQuery().Where(p => p.ProductId == item.ProductId && p.IncompletePurchaseOrderId == item.IncompletePurchaseOrderId).FirstOrDefault();

                    Repository.DbContext.IncompletePurchaseOrderLineItems.Remove(basketItem);
                });

                Repository.SaveChangesAsync();
            });
            return dataViewModels;
        }
        #endregion
    }
}
