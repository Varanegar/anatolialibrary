namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class PurchaseOrderLineItem : BaseModel
    {
        public decimal UnitPrice { get; set; }
        public decimal Qty { get; set; }
        public decimal Discount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ChargeAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal NetAmount { get; set; }
        public bool IsPrize { get; set; }
        [StringLength(500)]
        public string Comment { get; set; }
        public bool AllowReplace { get; set; }
        public decimal Weight { get; set; }
        public decimal FinalUnitPrice { get; set; }
        public decimal FinalQty { get; set; }
        public decimal FinalDiscount { get; set; }
        public decimal FinalTaxAmount { get; set; }
        public decimal FinalChargeAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public decimal FinalNetAmount { get; set; }
        public bool FinalIsPrize { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        [ForeignKey("FinalProduct")]
        public Nullable<Guid> FinalProductId { get; set; }
        [ForeignKey("PurchaseOrder")]
        public Guid PurchaseOrderId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Product FinalProduct { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}
