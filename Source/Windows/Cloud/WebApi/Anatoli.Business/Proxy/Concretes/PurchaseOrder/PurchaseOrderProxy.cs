using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.ViewModels.StoreModels;
using Anatoli.ViewModels.Order;

namespace Anatoli.Business.Proxy.Concretes
{
    public class PurchaseOrderProxy : AnatoliProxy<PurchaseOrder, PurchaseOrderViewModel>, IAnatoliProxy<PurchaseOrder, PurchaseOrderViewModel>
    {
        public IAnatoliProxy<PurchaseOrderLineItem, PurchaseOrderLineItemViewModel> PurchaseOrderLineItemProxy { get; set; }
        #region Ctors
        public PurchaseOrderProxy() :
            this(AnatoliProxy<PurchaseOrderLineItem, PurchaseOrderLineItemViewModel>.Create()
            )
        { }

        public PurchaseOrderProxy(IAnatoliProxy<PurchaseOrderLineItem, PurchaseOrderLineItemViewModel> purchaseOrderLineItemProxy
            )
        {
            PurchaseOrderLineItemProxy = purchaseOrderLineItemProxy;
        }
        #endregion


        public override PurchaseOrderViewModel Convert(PurchaseOrder data)
        {
            return new PurchaseOrderViewModel()
                {
                    ID = data.Number_ID,
                    UniqueId = data.Id,
                    PrivateOwnerId = data.PrivateLabelOwner_Id,

                    ActionSourceValueId = data.ActionSourceId,
                    Amount = data.TotalAmount,
                    AppOrderNo = data.AppOrderNo,
                    BackOfficeId = data.BackOfficeId,
                    CancelReason = data.CancelDesc,
                    ChargeAmount = data.ChargeAmount,
                    Comment = data.Comment,
                    UserId  = data.CustomerId,
                    DeliveryDate = data.DeliveryDate,
                    DeliveryFromTime = data.DeliveryFromTime,
                    DeliveryPDate = data.DeliveryPDate,
                    DeliveryToTime = data.DeliveryToTime,
                    DeliveryTypeId = data.DeliveryTypeId,
                    DeviceIMEI = data.DeviceIMEI,
                    DiscountAmount = data.DiscountAmount,
                    DiscountAmount2 = data.DiscountAmount2,
                    FinalAmount = data.TotalFinalAmount,
                    FinalChargeAmount = data.ChargeFinalAmount,
                    FinalDiscountAmount = data.DiscountFinalAmount,
                    FinalDiscountAmount2 = data.Discount2FinalAmount,
                    FinalFreightAmount= data.ShippingFinalCost,
                    FinalNetAmount = data.FinalNetAmount,
                    FinalPayableAmount = data.FinalNetAmount,
                    FinalTaxAmount = data.TaxFinalAmount,
                    FreightAmount = data.ShippingCost,
                    IsCancelled = data.IsCancelled,
                    NetAmount = data.NetAmount,
                    OrderDate = data.OrderDate,
                    OrderTime = data.OrderTime,
                    PayableAmount = data.NetAmount,
                    PaymentTypeValueId = data.PaymentTypeId,
                    PurchaseOrderStatusValueId = data.PurchaseOrderStatusId,
                    StoreGuid = data.StoreId,
                    TaxAmount = data.TaxAmount,

                    LineItems = (data.PurchaseOrderLineItems == null)?null:PurchaseOrderLineItemProxy.Convert(data.PurchaseOrderLineItems.ToList()),
                };
        }

        public override PurchaseOrder ReverseConvert(PurchaseOrderViewModel data)
        {
            return new PurchaseOrder()
                {
                    Number_ID = data.ID,
                    Id = data.UniqueId,
                    PrivateLabelOwner_Id = data.PrivateOwnerId,

                    ActionSourceId = data.ActionSourceValueId,
                    TotalAmount = data.Amount,
                    AppOrderNo = data.AppOrderNo,
                    BackOfficeId = data.BackOfficeId,
                    CancelDesc = data.CancelReason,
                    ChargeAmount = data.ChargeAmount,
                    Comment = data.Comment,
                    CustomerId = data.UserId,
                    DeliveryDate = data.DeliveryDate,
                    DeliveryFromTime = data.DeliveryFromTime,
                    DeliveryPDate = data.DeliveryPDate,
                    DeliveryToTime = data.DeliveryToTime,
                    DeliveryTypeId = data.DeliveryTypeId,
                    DeviceIMEI = data.DeviceIMEI,
                    DiscountAmount = data.DiscountAmount,
                    DiscountAmount2 = data.DiscountAmount2,
                    TotalFinalAmount = data.FinalAmount,
                    ChargeFinalAmount = data.FinalChargeAmount,
                    DiscountFinalAmount = data.FinalDiscountAmount,
                    Discount2FinalAmount = data.FinalDiscountAmount2,
                    ShippingFinalCost = data.FinalFreightAmount,
                    FinalNetAmount = data.FinalNetAmount,
                    TaxFinalAmount = data.FinalTaxAmount,
                    ShippingCost = data.FreightAmount,
                    IsCancelled = data.IsCancelled,
                    NetAmount = data.NetAmount,
                    OrderDate = data.OrderDate,
                    OrderTime = data.OrderTime,
                    PaymentTypeId = data.PaymentTypeValueId,
                    PurchaseOrderStatusId = data.PurchaseOrderStatusValueId,
                    StoreId = data.StoreGuid,
                    TaxAmount = data.TaxAmount,

                    PurchaseOrderLineItems = (data.LineItems == null) ? null : PurchaseOrderLineItemProxy.ReverseConvert(data.LineItems),
                };
        }
    }
}
