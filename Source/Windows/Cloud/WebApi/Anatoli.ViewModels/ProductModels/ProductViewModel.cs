using System;
using ProtoBuf;
using System.Collections.Generic;
using Anatoli.ViewModels.StockModels;

namespace Anatoli.ViewModels.ProductModels
{
    [ProtoContract]
    public class ProductViewModel : BaseViewModel
    {
        [ProtoMember(9)]
        public string Barcode { get; set; }
        [ProtoMember(10)]
        public string ProductCode { get; set; }
        [ProtoMember(11)]
        public string ProductName { get; set; }
        [ProtoMember(12)]
        public string StoreProductName { get; set; }
        [ProtoMember(13)]
        public decimal? PackVolume { get; set; }
        [ProtoMember(14)]
        public decimal? PackWeight { get; set; }
        [ProtoMember(15)]
        public decimal QtyPerPack { get; set; }
        [ProtoMember(16)]
        public decimal RateValue { get; set; }
        [ProtoMember(17)]
        public string Desctription { get; set; }

        [ProtoMember(18)]
        public Guid PackUnitId { get; set; }
        [ProtoMember(19)]
        public Guid? ProductTypeId { get; set; }
        [ProtoMember(20)]
        public Guid TaxCategoryId { get; set; }

        [ProtoMember(21)]
        public List<SupplierViewModel> Suppliers { get; set; }
        [ProtoMember(22)]
        public Guid? MainProductGroupId { get; set; }
        [ProtoMember(23)]
        public Guid? MainSupplierId { get; set; }

        [ProtoMember(24)]
        public Guid? ProductGroupId { get; set; }
        [ProtoMember(25)]
        public Guid? ManufactureId { get; set; }
        [ProtoMember(26)]
        public double ProductRate { get; set; }

        //public bool IsRemoved { get; set; }
        [ProtoMember(27)]
        public List<CharValueViewModel> CharValues { get; set; }
        [ProtoMember(28)]
        public List<ProductPictureViewModel> ProductPictures { get; set; }

        [ProtoMember(29)]
        public ProductTypeViewModel ProductTypeInfo { get; set; }
        [ProtoMember(30)]
        public string MainSupplierName { get; set; }
        [ProtoMember(31)]
        public string ManufactureName { get; set; }
        [ProtoMember(32)]
        public bool IsActiveInOrder { get; set; }
    }
    
}
