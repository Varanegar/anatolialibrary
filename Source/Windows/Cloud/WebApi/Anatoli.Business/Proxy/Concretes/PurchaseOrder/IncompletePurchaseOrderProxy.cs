using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.ViewModels.StoreModels;
using Anatoli.ViewModels.Order;

namespace Anatoli.Business.Proxy.Concretes
{
    public class IncompletePurchaseOrderProxy : AnatoliProxy<IncompletePurchaseOrder, IncompletePurchaseOrderViewModel>, IAnatoliProxy<IncompletePurchaseOrder, IncompletePurchaseOrderViewModel>
    {

        public IAnatoliProxy<IncompletePurchaseOrderLineItem, IncompletePurchaseOrderLineItemViewModel> IncompletePurchaseOrderLineItemProxy { get; set; }
        #region Ctors
        public IncompletePurchaseOrderProxy() :
            this(AnatoliProxy<IncompletePurchaseOrderLineItem, IncompletePurchaseOrderLineItemViewModel>.Create()
            )
        { }

        public IncompletePurchaseOrderProxy(IAnatoliProxy<IncompletePurchaseOrderLineItem, IncompletePurchaseOrderLineItemViewModel> incompletePurchaseOrderLineItemProxy
            )
        {
            IncompletePurchaseOrderLineItemProxy = incompletePurchaseOrderLineItemProxy;
        }
        #endregion
        public override IncompletePurchaseOrderViewModel Convert(IncompletePurchaseOrder data)
        {
            var result = new IncompletePurchaseOrderViewModel()
                {
                    ID = data.Number_ID,
                    UniqueId = data.Id,
                    PrivateOwnerId = data.PrivateLabelOwner_Id,

                    CityRegionId = data.CityRegionId,
                    DeliveryDate = data.DeliveryDate,
                    DeliveryFromTime = data.DeliveryFromTime,
                    DeliveryToTime = data.DeliveryToTime,
                    DeliveryTypeId = data.DeliveryTypeId,
                    OrderShipAddress = data.OrderShipAddress,
                    Phone = data.Phone,
                    StoreId = data.StoreId,
                    Transferee = data.Transferee,
                    PaymentTypeId = data.PaymentTypeId,
                    CustomerId = data.CustomerId,

                    
                };
            if (data.IncompletePurchaseOrderLineItems != null)
                result.LineItems = IncompletePurchaseOrderLineItemProxy.Convert(data.IncompletePurchaseOrderLineItems.ToList());
            return result; 
        }

        public override IncompletePurchaseOrder ReverseConvert(IncompletePurchaseOrderViewModel data)
        {
            return new IncompletePurchaseOrder()
                {
                    Number_ID = data.ID,
                    Id = data.UniqueId,

                    CityRegionId = data.CityRegionId,
                    DeliveryDate = data.DeliveryDate,
                    DeliveryFromTime = data.DeliveryFromTime,
                    DeliveryToTime = data.DeliveryToTime,
                    DeliveryTypeId = data.DeliveryTypeId,
                    OrderShipAddress = data.OrderShipAddress,
                    Phone = data.Phone,
                    StoreId = data.StoreId,
                    Transferee = data.Transferee,
                    PaymentTypeId = data.PaymentTypeId,
                    CustomerId = data.CustomerId,
                    //IncompletePurchaseOrderLineItems = (data.LineItems == null) ? null : IncompletePurchaseOrderLineItemProxy.ReverseConvert(data.LineItems)

                };
        }
    }
}
