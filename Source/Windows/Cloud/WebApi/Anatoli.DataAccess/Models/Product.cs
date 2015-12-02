namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;

    public class Product : BaseModel
    {
        public int MainAppProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string StoreProductName { get; set; }
        public int PackUnitValueId { get; set; }
        public int ProductTypeValueId { get; set; }
        public Nullable<decimal> PackVolume { get; set; }
        public Nullable<decimal> PackWeight { get; set; }
        public Nullable<long> TaxCategoryValueId { get; set; }
        public Nullable<int> MainSupplierId { get; set; }
        //public int ProductBaseId { get; set; }
        //public Nullable<Guid> ProductGroupId { get; set; }

        public virtual ProductBase ProductBase { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }
        //public virtual ICollection<ProductBaseProductMap> ProductBaseProductMaps { get; set; }
        public virtual ICollection<ProductComment> ProductComments { get; set; }
        public virtual ICollection<ProductPicture> ProductPictures { get; set; }
        public virtual ICollection<ProductRate> ProductRates { get; set; }
        public virtual ICollection<StoreActivePriceList> StoreActivePriceLists { get; set; }
        public virtual ICollection<CharValue> CharValues { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }

    }
}
