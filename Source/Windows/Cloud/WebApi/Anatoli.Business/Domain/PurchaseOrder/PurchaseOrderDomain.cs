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
using Anatoli.ViewModels.CustomerModels;
using Anatoli.ViewModels;
using Anatoli.Business.Proxy.Concretes;

namespace Anatoli.Business.Domain
{
    public class PurchaseOrderDomain : BusinessDomainV2<PurchaseOrder, PurchaseOrderViewModel, PurchaseOrderRepository, IPurchaseOrderRepository>, IBusinessDomainV2<PurchaseOrder, PurchaseOrderViewModel>
    {
        #region Properties
        public IRepository<Customer> CustomerRepository { get; set; }

        #endregion

        #region Ctors
        public PurchaseOrderDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public PurchaseOrderDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
            CustomerRepository = new CustomerRepository(dbc);
        }
        #endregion

        #region Methods
        public async Task<List<PurchaseOrderViewModel>> GetAllByCustomerIdOnLine(PurchaseOrderRequestModel dataRequest)
        {
            var returnData = new List<PurchaseOrderViewModel>();

            await Task.Factory.StartNew(() =>
            {
                dataRequest.centerId = "all";  
                string data = JsonConvert.SerializeObject(dataRequest);

                 returnData.AddRange(GetOnlineData(WebApiURIHelper.GetPoByCustomerIdLocalURI, data ));
            });
            return returnData;
        }

        public override async Task PublishAsync(List<PurchaseOrder> dataList)
        {
            try
            {
                foreach (PurchaseOrder item in dataList)
                {

                    item.ApplicationOwnerId = ApplicationOwnerKey; item.DataOwnerId = DataOwnerKey; item.DataOwnerCenterId = DataOwnerCenterKey;
                    var currentData = MainRepository.GetQuery().Where(p => p.Id == item.Id && p.DataOwnerId == DataOwnerKey).FirstOrDefault();
                    if (currentData != null)
                    {
                        currentData.ActionSourceId = item.ActionSourceId;
                        currentData.AppOrderNo = item.AppOrderNo;
                        currentData.BackOfficeId = item.BackOfficeId;
                        currentData.CancelDesc = item.CancelDesc;
                        currentData.CancelReasonId = item.CancelReasonId;
                        currentData.ChargeAmount = item.ChargeAmount;
                        currentData.ChargeFinalAmount = item.ChargeFinalAmount;
                        currentData.Comment = item.Comment;
                        currentData.CustomerId = item.CustomerId;
                        currentData.CustomerShipAddressId = item.CustomerShipAddressId;
                        currentData.DeliveryDate = item.DeliveryDate;
                        currentData.DeliveryFromTime = item.DeliveryFromTime;
                        currentData.DeliveryPDate = item.DeliveryPDate;
                        currentData.DeliveryToTime = item.DeliveryToTime;
                        currentData.DeliveryTypeId = item.DeliveryTypeId;
                        currentData.DeviceIMEI = item.DeviceIMEI;
                        currentData.Discount2FinalAmount = item.Discount2FinalAmount;
                        currentData.DiscountAmount = item.DiscountAmount;
                        currentData.DiscountAmount2 = item.DiscountAmount2;
                        currentData.DiscountCodeId = item.DiscountCodeId;
                        currentData.DiscountFinalAmount = item.DiscountFinalAmount;
                        currentData.FinalNetAmount = item.FinalNetAmount;
                        currentData.IsCancelled = item.IsCancelled;
                        currentData.IsRemoved = item.IsRemoved;
                        currentData.NetAmount = item.NetAmount;
                        currentData.Number_ID = item.Number_ID;
                        currentData.OrderDate = item.OrderDate;
                        currentData.OrderPDate = item.OrderPDate;
                        currentData.OrderTime = item.OrderTime;
                        currentData.OtherAdd = item.OtherAdd;
                        currentData.OtherFinalAdd = item.OtherFinalAdd;
                        currentData.OtherFinalSub = item.OtherFinalSub;
                        currentData.OtherSub = item.OtherSub;
                        currentData.PaymentTypeId = item.PaymentTypeId;
                        currentData.PurchaseOrderStatusId = item.PurchaseOrderStatusId;
                        currentData.ShippingCost = item.ShippingCost;
                        currentData.ShippingFinalCost = item.ShippingFinalCost;
                        currentData.StoreId = item.StoreId;
                        currentData.TaxAmount = item.TaxAmount;
                        currentData.TaxFinalAmount = item.TaxFinalAmount;
                        currentData.TotalAmount = item.TotalAmount;
                        currentData.TotalFinalAmount = item.TotalFinalAmount;
                        currentData.LastUpdate = DateTime.Now;
                        currentData = await SetLineItemData(currentData, item.PurchaseOrderLineItems.ToList(), DBContext);
                        await MainRepository.UpdateAsync(currentData);
                    }
                    else
                    {
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        if (item.PurchaseOrderLineItems != null)
                        {
                            item.PurchaseOrderLineItems.ToList().ForEach(itemDetail =>
                            {
                                itemDetail.Id = Guid.NewGuid();
                                itemDetail.ApplicationOwnerId = item.ApplicationOwnerId;
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

        public async Task<PurchaseOrder> SetLineItemData(PurchaseOrder data, List<PurchaseOrderLineItem> dataList, AnatoliDbContext context)
        {
            await Task.Factory.StartNew(() =>
            {
                data.PurchaseOrderLineItems.Clear();
                foreach (PurchaseOrderLineItem item in dataList)
                {
                    item.ApplicationOwnerId = ApplicationOwnerKey; item.DataOwnerId = DataOwnerKey; item.DataOwnerCenterId = DataOwnerCenterKey;
                    item.CreatedDate = item.LastUpdate = data.CreatedDate;
                    item.Id = Guid.NewGuid();
                    data.PurchaseOrderLineItems.Add(item);
                }
            });
            return data;
        }

        public async Task<PurchaseOrderViewModel> PublishOrderOnline(PurchaseOrderRequestModel data)
        {
            if (data.orderEntity.UniqueId == Guid.Empty)
                data.orderEntity.UniqueId = Guid.NewGuid();

            var returnData = new PurchaseOrderViewModel();
            var customerDomain = new CustomerDomain(ApplicationOwnerKey, DataOwnerKey, DataOwnerCenterKey);
            data.orderEntity.Customer = await customerDomain.GetByIdAsync(data.orderEntity.UserId);
            string dataStr = JsonConvert.SerializeObject(data);
            await Task.Factory.StartNew(() =>
            {
                returnData = PostOnlineData(WebApiURIHelper.SaveOrderLocalURI, dataStr, true);
            });

            var order = new PurchaseOrderProxy().ReverseConvert(returnData);
            await PublishAsync(order);

            return returnData;
        }

        public async Task<PurchaseOrderViewModel> CalcPromoOnline(PurchaseOrderRequestModel dataRequest)
        {
            var returnData = new PurchaseOrderViewModel();
            string data = JsonConvert.SerializeObject(dataRequest);

            await Task.Factory.StartNew(() =>
            {
                returnData = PostOnlineData(WebApiURIHelper.CalcPromoLocalURI, data, true);
            });
            return returnData;
        }

        #endregion
    }
}
