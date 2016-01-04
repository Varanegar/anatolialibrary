namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product : BaseModel
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string StoreProductName { get; set; }
        public int PackUnitValueId { get; set; }
        public int ProductTypeValueId { get; set; }
        public Nullable<decimal> PackVolume { get; set; }
        public Nullable<decimal> PackWeight { get; set; }
        public Nullable<long> TaxCategoryValueId { get; set; }
        public string Desctription { get; set; }
        [ForeignKey("ProductGroup")]
        public Nullable<Guid> ProductGroupId { get; set; }
        [ForeignKey("Manufacture")]
        public Nullable<Guid> ManufactureId { get; set; }
        [ForeignKey("MainSupplier")]
        public Nullable<Guid> MainSuppliereId { get; set; }
        [ForeignKey("ProductType")]
        public Nullable<Guid> ProductTypeId { get; set; }

        public virtual ProductType ProductType { get; set; }
        public virtual Supplier MainSupplier { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }
        public virtual ICollection<StockProduct> StockProducts { get; set; }
        public virtual ICollection<ProductComment> ProductComments { get; set; }
        public virtual ICollection<ProductPicture> ProductPictures { get; set; }
        public virtual ICollection<BasketItem> BasketItems { get; set; }
        public virtual ICollection<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }
        public virtual ICollection<ProductRate> ProductRates { get; set; }
        public virtual ICollection<StoreActivePriceList> StoreActivePriceLists { get; set; }
        public virtual ICollection<CharValue> CharValues { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual Manufacture Manufacture { get; set; }
    }
}
