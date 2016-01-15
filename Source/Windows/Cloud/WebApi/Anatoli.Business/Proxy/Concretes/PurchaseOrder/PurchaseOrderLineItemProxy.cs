using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.ViewModels.StoreModels;
using Anatoli.ViewModels.Order;

namespace Anatoli.Business.Proxy.Concretes
{
    public class PurchaseOrderLineItemProxy : AnatoliProxy<PurchaseOrderLineItem, PurchaseOrderLineItemViewModel>, IAnatoliProxy<PurchaseOrderLineItem, PurchaseOrderLineItemViewModel>
    {

        public override PurchaseOrderLineItemViewModel Convert(PurchaseOrderLineItem data)
        {
            return new PurchaseOrderLineItemViewModel()
                {
                    ID = data.Number_ID,
                    UniqueId = data.Id,

                    AllowReplace = data.AllowReplace,
                    ChargeAmount = data.ChargeAmount,
                    Comment = data.Comment,
                    DiscountAmount = data.Discount,
                    FinalChargeAmount = data.FinalChargeAmount,
                    FinalDiscountAmount = data.FinalDiscount,
                    FinalIsPrize = data.FinalIsPrize,
                    FinalNetAmount = data.FinalNetAmount,
                    FinalProductId = data.FinalProductId,
                    FinalQty = data.FinalQty,
                    FinalTaxAmount = data.FinalTaxAmount,
                    FinalUnitPrice = data.FinalUnitPrice,
                    IsPrize = data.IsPrize,
                    NetAmount = data.NetAmount,
                    ProductId = data.ProductId,
                    PurchaseOrderId = data.PurchaseOrderId,
                    Qty = data.Qty,
                    TaxAmount = data.TaxAmount,
                    UnitPrice = data.UnitPrice,
                };
        }

        public override PurchaseOrderLineItem ReverseConvert(PurchaseOrderLineItemViewModel data)
        {
            return new PurchaseOrderLineItem()
                {
                    Number_ID = data.ID,
                    Id = data.UniqueId,

                    AllowReplace = data.AllowReplace,
                    ChargeAmount = data.ChargeAmount,
                    Comment = data.Comment,
                    Discount = data.DiscountAmount,
                    FinalChargeAmount = data.FinalChargeAmount,
                    FinalDiscount = data.FinalDiscountAmount,
                    FinalIsPrize = data.FinalIsPrize,
                    FinalNetAmount = data.FinalNetAmount,
                    FinalProductId = data.FinalProductId,
                    FinalQty = data.FinalQty,
                    FinalTaxAmount = data.FinalTaxAmount,
                    FinalUnitPrice = data.FinalUnitPrice,
                    IsPrize = data.IsPrize,
                    NetAmount = data.NetAmount,
                    ProductId = data.ProductId,
                    PurchaseOrderId = data.PurchaseOrderId,
                    Qty = data.Qty,
                    TaxAmount = data.TaxAmount,
                    UnitPrice = data.UnitPrice,
                };
        }
    }
}
