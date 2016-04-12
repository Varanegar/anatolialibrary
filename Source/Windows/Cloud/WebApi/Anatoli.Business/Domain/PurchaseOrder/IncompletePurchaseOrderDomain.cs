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
using Anatoli.Business.Helpers;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Anatoli.Business.Domain
{
    public class IncompletePurchaseOrderDomain : BusinessDomainV2<IncompletePurchaseOrder, IncompletePurchaseOrderViewModel, IncompletePurchaseOrderRepository, IIncompletePurchaseOrderRepository>, IBusinessDomainV2<IncompletePurchaseOrder, IncompletePurchaseOrderViewModel>
    {
        #region Properties
        public IRepository<IncompletePurchaseOrderLineItem> LineItemRepository { get; set; }

        #endregion

        #region Ctors
        public IncompletePurchaseOrderDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public IncompletePurchaseOrderDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
            LineItemRepository = new IncompletePurchaseOrderLineItemRepository(dbc);
        }
        #endregion

        #region Methods
        public async Task<List<IncompletePurchaseOrderViewModel>> GetAllByCustomerId(Guid customerId)
        {
            var data = (await GetAllAsync(p => p.CustomerId == customerId)).ToList();

            foreach(var item in data)
            {

                var lineItems = await new IncompletePurchaseOrderLineItemDomain(ApplicationOwnerKey, DataOwnerKey, DataOwnerCenterKey).GetAllAsync(p => p.IncompletePurchaseOrderId == item.UniqueId);
                item.LineItems.AddRange(lineItems.ToList());
            }
            return data;
        }

        public override async Task PublishAsync(List<IncompletePurchaseOrder> dataList)
        {
            try
            {
                foreach (IncompletePurchaseOrder item in dataList)
                {

                    item.ApplicationOwnerId = ApplicationOwnerKey; item.DataOwnerId = DataOwnerKey; item.DataOwnerCenterId = DataOwnerCenterKey;
                    var currentData = MainRepository.GetQuery().Where(p => p.CustomerId == item.CustomerId).FirstOrDefault();
                    if (currentData != null)
                    {
                        currentData.CityRegionId = item.CityRegionId;
                        currentData.DeliveryDate = item.DeliveryDate;
                        currentData.DeliveryFromTime = item.DeliveryFromTime;
                        currentData.DeliveryToTime = item.DeliveryToTime;
                        currentData.DeliveryTypeId = item.DeliveryTypeId;
                        currentData.CustomerShipAddressId = item.CustomerShipAddressId;
                        currentData.OrderShipAddress = item.OrderShipAddress;
                        currentData.Phone = item.Phone;
                        currentData.StoreId = item.StoreId;
                        currentData.Transferee = item.Transferee;
                        currentData.LastUpdate = DateTime.Now;
                        await MainRepository.UpdateAsync(currentData);
                    }
                    else
                    {
                        item.Id = (item.Id == Guid.Empty) ? Guid.NewGuid() : item.Id;
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        if (item.IncompletePurchaseOrderLineItems != null)
                        {
                            item.IncompletePurchaseOrderLineItems.ToList().ForEach(itemDetail =>
                            {
                                item.Id = Guid.NewGuid();
                                itemDetail.ApplicationOwnerId = item.ApplicationOwnerId; item.DataOwnerId = DataOwnerKey; item.DataOwnerCenterId = DataOwnerCenterKey;
                                itemDetail.CreatedDate = itemDetail.LastUpdate = item.CreatedDate;
                            });
                        }
                        await MainRepository.AddAsync(item);
                    }
                }
                await MainRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error("PublishAsync", ex);
                throw ex;
            }
        }

        public async Task<ICollection<IncompletePurchaseOrder>> Clear(List<IncompletePurchaseOrder> dataList)
        {
            await Task.Factory.StartNew(() =>
            {

                dataList.ForEach(item =>
                {
                    var itemDetailData = LineItemRepository.GetQuery().Where(p => p.IncompletePurchaseOrderId == item.Id);
                    itemDetailData.ToList().ForEach(itemDetail =>
                        {
                            LineItemRepository.DbContext.IncompletePurchaseOrderLineItems.Remove(itemDetail);
                        });
                });
            });
            await MainRepository.SaveChangesAsync();
            return dataList;
        }

        #endregion
    }
}
