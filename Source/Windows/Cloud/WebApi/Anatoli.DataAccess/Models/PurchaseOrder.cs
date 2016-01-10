namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public class PurchaseOrder : BaseModel
    {        
        public long ActionSourceValueId { get; set; }
        public string DeviceIMEI { get; set; }
        public Nullable<DateTime> OrderDate { get; set; }
        public string OrderPDate { get; set; }
        public Nullable<TimeSpan> OrderTime { get; set; }
        public long PaymentTypeValueId { get; set; }
        public string DiscountCodeId { get; set; }
        public long AppOrderNo { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Add1Amount { get; set; }
        public decimal Add2Amount { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal OtherAdd { get; set; }
        public Nullable<decimal> OtherSub { get; set; }
        [StringLength(500)]
        public string Comment { get; set; }
        public long DeliveryTypeValueId { get; set; }
        public Nullable<DateTime> DeliveryDate { get; set; }
        [StringLength(10)]
        public string DeliveryPDate { get; set; }
        public Nullable<TimeSpan> DeliveryFromTime { get; set; }
        public Nullable<TimeSpan> DeliveryToTime { get; set; }
        public long PurchaseOrderStatusValueId { get; set; }
        public decimal DiscountFinalAmount { get; set; }
        public decimal Add1FinalAmount { get; set; }
        public decimal Add2FinalAmount { get; set; }
        public decimal ShippingFinalCost { get; set; }
        public decimal TotalFinalAmount { get; set; }
        public decimal OtherFinalAdd { get; set; }
        public decimal OtherFinalSub { get; set; }
        public Nullable<byte> IsCancelled { get; set; }
        public Nullable<long> CancelReasonValueId { get; set; }
        [StringLength(100)]
        public string CancelDesc { get; set; }
        public Nullable<int> BackOfficeId { get; set; }

        public virtual CustomerShipAddress CustomerShipAddress { get; set; }
        public virtual Store Store { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Basket Basket { get; set; }
        public virtual ICollection<PurchaseOrderClearance> PurchaseOrderClearances { get; set; }
        public virtual ICollection<PurchaseOrderHistory> PurchaseOrderHistories { get; set; }
        public virtual ICollection<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }
        public virtual ICollection<PurchaseOrderPayment> PurchaseOrderPayments { get; set; }
    }
}
