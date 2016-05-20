using System;
using ProtoBuf;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.ViewModels
{
    [ProtoContract]
    [ProtoInclude(9, typeof(ProductViewModel))]
    [ProtoInclude(10, typeof(ProductTypeViewModel))]
    [ProtoInclude(11, typeof(SupplierViewModel))]
    [ProtoInclude(12, typeof(CharValueViewModel))]
    [ProtoInclude(13, typeof(ProductPictureViewModel))]
    public class BaseViewModel
    {
        [ProtoMember(1)]
        public int ID { get; set; }
        [ProtoMember(2)]
        public Guid UniqueId { get; set; }
        [ProtoMember(3)]
        public Guid ApplicationOwnerId { get; set; }
        [ProtoMember(4)]
        public Guid DataOwnerId { get; set; }
        [ProtoMember(5)]
        public Guid DataCenterOwnerId { get; set; }
        [ProtoMember(6)]
        public bool IsRemoved { get; set; }
        [ProtoMember(7)]
        public DateTime CreatedDate { get; set; }
        [ProtoMember(8)]
        public DateTime LastUpdate { get; set; }

    }
}
