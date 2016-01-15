namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class PurchaseOrder : BaseModel
    {        
        public Guid ActionSourceId { get; set; }
        public string DeviceIMEI { get; set; }
        public Nullable<DateTime> OrderDate { get; set; }
        public string OrderPDate { get; set; }
        public Nullable<TimeSpan> OrderTime { get; set; }
        public Guid PaymentTypeId { get; set; }
        public string DiscountCodeId { get; set; }
        public long AppOrderNo { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountAmount2 { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ChargeAmount { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal OtherAdd { get; set; }
        public decimal OtherSub { get; set; }
        [StringLength(500)]
        public string Comment { get; set; }
        public Guid DeliveryTypeId { get; set; }
        public Nullable<DateTime> DeliveryDate { get; set; }
        [StringLength(10)]
        public string DeliveryPDate { get; set; }
        public Nullable<TimeSpan> DeliveryFromTime { get; set; }
        public Nullable<TimeSpan> DeliveryToTime { get; set; }
        public Guid PurchaseOrderStatusId { get; set; }
        public decimal DiscountFinalAmount { get; set; }
        public decimal Discount2FinalAmount { get; set; }
        public decimal ChargeFinalAmount { get; set; }
        public decimal TaxFinalAmount { get; set; }
        public decimal ShippingFinalCost { get; set; }
        public decimal TotalFinalAmount { get; set; }
        public decimal OtherFinalAdd { get; set; }
        public decimal OtherFinalSub { get; set; }
        public decimal FinalNetAmount { get; set; }
        public bool IsCancelled { get; set; }
        public Guid? CancelReasonId { get; set; }
        [StringLength(100)]
        public string CancelDesc { get; set; }
        public Nullable<int> BackOfficeId { get; set; }
        [ForeignKey("Store")]
        public Guid StoreId { get; set; }
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerShipAddress")]
        public Guid CustomerShipAddressId { get; set; }
        public virtual CustomerShipAddress CustomerShipAddress { get; set; }
        public virtual Store Store { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<PurchaseOrderClearance> PurchaseOrderClearances { get; set; }
        public virtual ICollection<PurchaseOrderStatusHistory> PurchaseOrderHistories { get; set; }
        public virtual ICollection<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }
        public virtual ICollection<PurchaseOrderPayment> PurchaseOrderPayments { get; set; }
    }
}
