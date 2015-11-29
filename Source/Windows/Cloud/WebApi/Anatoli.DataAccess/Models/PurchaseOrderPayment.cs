namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class PurchaseOrderPayment : BaseModel
    {
        public int PurchaseOrderGiftCardId { get; set; }
        //public int PurchaseOrderId { get; set; }
        public decimal Amount { get; set; }
        public long PaymentTypeValueId { get; set; }
        public Nullable<int> GiftCardId { get; set; }
        public string BankAccountId { get; set; }
        public string PaymentTrackingNo { get; set; }
        public long PayTypeValueId { get; set; }
        public byte InAppPayment { get; set; }
        public DateTime PayDate { get; set; }
        public string PayPDate { get; set; }
        public TimeSpan PayTime { get; set; }
    
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}
