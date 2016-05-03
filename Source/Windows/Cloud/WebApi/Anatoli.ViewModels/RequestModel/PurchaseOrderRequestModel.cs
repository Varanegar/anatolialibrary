using System;
using Anatoli.ViewModels.User;
using System.Collections.Generic;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.Order;

namespace Anatoli.ViewModels
{
    public class PurchaseOrderRequestModel
    {
        public Guid customerId { get; set; }
        public string centerId { get; set; }
        public Guid poId { get; set; }
        public List<IncompletePurchaseOrderViewModel> incompletePurchaseOrderData { get; set; }
        public List<IncompletePurchaseOrderLineItemViewModel> incompletePurchaseOrderLineItemData { get; set; }
        public PurchaseOrderViewModel orderEntity { get; set; }
        public bool getAllOrderTypes { get; set; }
    }
}
