using ProtoBuf;

namespace Anatoli.ViewModels.ProductModels
{
    [ProtoContract]
    public class CharValueViewModel : BaseViewModel
    {
        [ProtoMember(1)]
        public string CharValueText { get; set; }
        [ProtoMember(2)]
        public decimal? CharValueFromAmount { get; set; }
        [ProtoMember(3)]
        public decimal? CharValueToAmount { get; set; }
    }
}
