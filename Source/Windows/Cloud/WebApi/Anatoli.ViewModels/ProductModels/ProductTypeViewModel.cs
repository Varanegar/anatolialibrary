using System;
using ProtoBuf;

namespace Anatoli.ViewModels.StockModels
{
    [ProtoContract]
    public class ProductTypeViewModel : BaseViewModel
    {
        public ProductTypeViewModel()
        {
            ProductTypeName = string.Empty;
        }

        public static readonly Guid NormalRequestProducts = Guid.Parse("8E85E5B0-9242-47A1-99A1-7B90566C36D4");
        public static readonly Guid RefrigRequestProducts = Guid.Parse("6FC2FD34-4CBC-4EB1-BD7E-1BD751E4F2A2");
        public static readonly Guid FreezeRequestProducts = Guid.Parse("72E59112-6054-4140-8E33-947228616393");

        [ProtoMember(1)]
        public string ProductTypeName { get; set; }

    }
}
