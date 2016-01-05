using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.ViewModels.StoreModels;
using Anatoli.ViewModels.Order;

namespace Anatoli.Business.Proxy.Concretes
{
    public class IncompletePurchaseOrderLineItemProxy : AnatoliProxy<IncompletePurchaseOrderLineItem, IncompletePurchaseOrderLineItemViewModel>, IAnatoliProxy<IncompletePurchaseOrderLineItem, IncompletePurchaseOrderLineItemViewModel>
    {

        public override IncompletePurchaseOrderLineItemViewModel Convert(IncompletePurchaseOrderLineItem data)
        {
            return new IncompletePurchaseOrderLineItemViewModel()
                {
                    ID = data.Number_ID,
                    UniqueId = data.Id,

                    ProductId = data.ProductId,
                    Qty = data.Qty,
                    IncompletePurchaseOrderId = data.IncompletePurchaseOrderId,
                };
        }

        public override IncompletePurchaseOrderLineItem ReverseConvert(IncompletePurchaseOrderLineItemViewModel data)
        {
            return new IncompletePurchaseOrderLineItem()
                {
                    Number_ID = data.ID,
                    Id = data.UniqueId,

                    ProductId = data.ProductId,
                    Qty = data.Qty,
                    IncompletePurchaseOrderId = data.IncompletePurchaseOrderId,
                };
        }
    }
}
