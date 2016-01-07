using Anatoli.Framework.Model;
using Anatoli.Framework.AnatoliBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model.Product
{
    public class ProductModel : BaseDataModel
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string StoreProductName { get; set; }
        public string PackVolume { get; set; }
        public string PackWeight { get; set; }
        public string QtyPerPack { get; set; }
        public string RateValue { get; set; }
        public string SmallPicURL { get; set; }
        public string LargePicURL { get; set; }
        public string Desctription { get; set; }

        public string PackUnitId { get; set; }
        public string ProductTypeId { get; set; }
        public string TaxCategoryId { get; set; }

        //public List<SupplierViewModel> Suppliers { get; set; }
        public string MainProductGroupIdString { get; set; }
        public string ProductGroupIdString { get; set; }
        public string ManufactureIdString { get; set; }

        //public List<CharValueViewModel> CharValues { get; set; }
        //public List<ProductPictureViewModel> ProductPictures { get; set; }

        public int order_count { get; set; }
        public int cat_id { get; set; }
        public int brand_id { get; set; }
        public string product_name { get; set; }
        public int product_id { get; set; }
        public double price { get; set; }
        public int favorit { get; set; }
        public int count { get; set; }
        public string image { get; set; }
        public bool IsFavorit
        {
            get { return favorit == 1 ? true : false; }
        }
    }
}
