using Aantoli.Framework.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aantoli.Common.Entity.Gateway.Product
{
    public class ProductEntity : BaseEntity
    {
        public int ProductCode { get; set; }
        public string ProductName { get; set; }
        public string StoreProductName { get; set; }
        public int PackUnitId { get; set; }
        public int ProductTypeId { get; set; }
        public decimal PackVolume { get; set; }
        public decimal PackWeight { get; set; }
        public int TaxCategoryId { get; set; }
        public int ProductGroupId { get; set; }
        public int ManufactureId { get; set; }
        public int RateValue { get; set; }
        public string SmallPicURL { get; set; }
        public string LargePicURL { get; set; }

        public ProductSupplierListEntity SupplierInfoList { get; set; }
    }

}
