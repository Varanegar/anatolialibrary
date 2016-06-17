using System;
using ProtoBuf;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.ProductModels;
using Anatoli.Common.ViewModel;

namespace Anatoli.ViewModels
{
    [ProtoContract]
    [ProtoInclude(9, typeof(ProductViewModel))]
    [ProtoInclude(10, typeof(ProductTypeViewModel))]
    [ProtoInclude(11, typeof(SupplierViewModel))]
    [ProtoInclude(12, typeof(CharValueViewModel))]
    [ProtoInclude(13, typeof(ProductPictureViewModel))]
    public class BaseViewModel: CBaseViewModel
    {
        [ProtoMember(1)]
        public override int ID { get; set; }
        [ProtoMember(2)]
        public override Guid UniqueId { get; set; }
        [ProtoMember(3)]
        public override Guid ApplicationOwnerId { get; set; }
        [ProtoMember(4)]
        public override Guid DataOwnerId { get; set; }
        [ProtoMember(5)]
        public override Guid DataOwnerCenterId { get; set; }
        [ProtoMember(6)]
        public override bool IsRemoved { get; set; }
        [ProtoMember(7)]
        public override DateTime CreatedDate { get; set; }
        [ProtoMember(8)]
        public override DateTime LastUpdate { get; set; }

    }
}
