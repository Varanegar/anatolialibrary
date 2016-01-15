using System;
using System.Linq;
using System.Collections.Generic;

namespace Anatoli.ViewModels.Order
{
    public class PurchaseOrderStatusHistoryViewModel : BaseViewModel
    {
        public Guid StatusId { get; set; }
        public DateTime ActionDate { get; set; }
        public string Comment { get; set; }
        public Guid PurchaseOrderId { get; set; }
    }
}
