namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product : BaseModel
    {
        [StringLength(20)]
        public string ProductCode { get; set; }
        [StringLength(20)]
        public string Barcode { get; set; }
        [StringLength(200)]
        public string ProductName { get; set; }
        [StringLength(200)]
        public string StoreProductName { get; set; }
        public Nullable<decimal> PackVolume { get; set; }
        public Nullable<decimal> PackWeight { get; set; }
        [DefaultValue(1)]
        public decimal QtyPerPack { get; set; }
        public Nullable<long> TaxCategoryValueId { get; set; }
        [StringLength(500)]
        public string Desctription { get; set; }
        [ForeignKey("ProductGroup")]
        public Nullable<Guid> ProductGroupId { get; set; }
        [ForeignKey("MainProductGroup")]
        public Nullable<Guid> MainProductGroupId { get; set; }
        [ForeignKey("Manufacture")]
        public Nullable<Guid> ManufactureId { get; set; }
        [ForeignKey("MainSupplier")]
        public Nullable<Guid> MainSupplierId { get; set; }
        [ForeignKey("ProductType")]
        public Nullable<Guid> ProductTypeId { get; set; }

        public virtual ProductType ProductType { get; set; }
        public virtual Supplier MainSupplier { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }
        public virtual MainProductGroup MainProductGroup { get; set; }
        public virtual ICollection<StockProduct> StockProducts { get; set; }
        public virtual ICollection<ProductComment> ProductComments { get; set; }
        public virtual ICollection<ProductPicture> ProductPictures { get; set; }
        public virtual ICollection<BasketItem> BasketItems { get; set; }
        public virtual ICollection<StockProductRequestRule> StockProductRequestRules { get; set; }
        public virtual ICollection<StockProductRequestProduct> StockProductRequestProducts { get; set; }
        public virtual ICollection<StockHistoryOnHand> StockHistoryOnHands { get; set; }
        public virtual ICollection<StockActiveOnHand> StockActiveOnHands { get; set; }
        public virtual ICollection<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }
        public virtual ICollection<ProductRate> ProductRates { get; set; }
        public virtual ICollection<StoreActivePriceList> StoreActivePriceLists { get; set; }
        public virtual ICollection<CharValue> CharValues { get; set; }
        public virtual ICollection<ProductTagValue> ProductTagValues { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<IncompletePurchaseOrderLineItem> IncompletePurchaseOrderLineItems { get; set; }
        public virtual Manufacture Manufacture { get; set; }
    }
}
