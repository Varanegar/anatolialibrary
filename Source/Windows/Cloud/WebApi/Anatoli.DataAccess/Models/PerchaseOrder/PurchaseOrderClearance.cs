namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class PurchaseOrderClearance : AnatoliBaseModel
    {
        //public int PurchaseOrderClearanceId { get; set; }
        //public int PurchaseOrderId { get; set; }
        public decimal Amount { get; set; }
        public int DeliveryPersonId { get; set; }
        public int ClearanceStatusTypeId { get; set; }
    
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}
