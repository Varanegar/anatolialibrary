using System;
using System.Linq;
using System.Collections.Generic;
using Aantoli.Framework.Entity.Base;

namespace Aantoli.Common.Entity.Gateway.Product
{
    public class ProductEntity : BaseEntity
    {
        public int ProductCode { get; set; }
        public string ProductName { get; set; }
        public string StoreProductName { get; set; }
        public Guid PackUnitId { get; set; }
        public Guid ProductTypeId { get; set; }
        public decimal PackVolume { get; set; }
        public decimal PackWeight { get; set; }
        public Guid TaxCategoryId { get; set; }
        public Guid ProductGroupId { get; set; }
        public Guid ManufactureId { get; set; }
        public decimal RateValue { get; set; }
        public string SmallPicURL { get; set; }
        public string LargePicURL { get; set; }
        public string Desctription { get; set; }
        public ProductSupplierListEntity SupplierInfoList { get; set; }
    }
}