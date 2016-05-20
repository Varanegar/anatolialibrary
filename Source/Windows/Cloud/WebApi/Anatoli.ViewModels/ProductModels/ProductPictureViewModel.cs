using ProtoBuf;

namespace Anatoli.ViewModels.ProductModels
{
    [ProtoContract]
    public class ProductPictureViewModel : BaseViewModel
    {
        [ProtoMember(1)]
        public string ProductPictureName { get; set; }
        [ProtoMember(2)]
        public bool IsDefault { get; set; }
    }
}
