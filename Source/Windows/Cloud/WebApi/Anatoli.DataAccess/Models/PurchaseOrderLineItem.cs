namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class PurchaseOrderLineItem : BaseModel
    {
        //public int PurchaseOrderLineItemId { get; set; }
        //public int PurchaseOrderId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public int FinalProductId { get; set; }
        public Nullable<int> ProductBaseId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Qty { get; set; }
        public decimal Discount { get; set; }
        public decimal Add1 { get; set; }
        public decimal Add2 { get; set; }
        public decimal Amount { get; set; }
        public byte IsPrize { get; set; }
        public string Comment { get; set; }
        public Nullable<byte> AllowReplace { get; set; }
        public decimal Weight { get; set; }
        public decimal FinalUnitPrice { get; set; }
        public decimal FinalQty { get; set; }
        public decimal FinalDiscount { get; set; }
        public decimal FinalAdd1 { get; set; }
        public decimal FinalAdd2 { get; set; }
        public decimal FinalAmount { get; set; }
        public byte FinalIsPrize { get; set; }
    
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}
