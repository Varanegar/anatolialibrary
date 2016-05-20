using System;
using ProtoBuf;

namespace Anatoli.ViewModels
{
    [ProtoContract]
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
        public Guid DataOwnerCenterId { get; set; }
        [ProtoMember(6)]
        public bool IsRemoved { get; set; }
        [ProtoMember(7)]
        public DateTime CreatedDate { get; set; }
        [ProtoMember(8)]
        public DateTime LastUpdate { get; set; }

    }
}
