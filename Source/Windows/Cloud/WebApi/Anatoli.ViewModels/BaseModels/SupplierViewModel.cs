using ProtoBuf;

namespace Anatoli.ViewModels.ProductModels
{
    [ProtoContract]
    public class SupplierViewModel : BaseViewModel
    {
        [ProtoMember(1)]
        public string SupplierName { get; set; }
    }
}
